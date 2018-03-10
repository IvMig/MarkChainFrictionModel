using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarkChainsMechCalculations
{
    // Класс функций, расчитывающих эволюцию распределений
    public static class CalcSolution
    {
        // Решить задачу
        public static void FindSolution(ref ModelData Model)
        {
            if (Model.mode == 0) FindSolutionDestroySchemeAny(ref Model);
            if (Model.mode == 1) FindSolutionGeneralScheme(ref Model);
        }
        // Проследить эволюцию распределений в случае усталостного разрушения
        private static void FindSolutionDestroySchemeAny(ref ModelData Model)
        {
            int nt = Model.nt;
            int N = Model.DData[0].p.Length;
            for (int i = 0; i < nt; i++)
            {
                // Расчет параметров Пуассона
                double[] axi = CalcFunctions.FindParamPo(Model.netaPwxi, Model.DData[i].Fq);
                double[] aeta = CalcFunctions.FindParamPo(Model.nxiPweta, Model.DData[i].Tp);
                double[][] Poxi = CalcFunctions.Po(axi);
                double[][] Poeta = CalcFunctions.Po(aeta);
                // Расчет приращений распределений
                double[] deltap = MarkovChainSchemes.DestroySchemexiAnyTime(Model.DData[i], Poxi, Model.kxi);
                double[] deltaq = MarkovChainSchemes.DestroySchemeetaAnyTime(Model.DData[i], Poeta, Model.keta);
                Model.DData[i + 1] = new DistributionData(N);
                // Вычисление распределений
                for (int j = 0; j < N; j++)
                {
                    Model.DData[i + 1].p[j] = Model.DData[i].p[j] + deltap[j];
                    Model.DData[i + 1].q[j] = Model.DData[i].q[j] + deltaq[j];
                }
                // Вычисление контактного пересечения
                Model.DData[i + 1].SetPp();
                Model.DData[i + 1].Setpdelta();
                Model.DData[i + 1].Sdelta = CalcFunctions.PosPart(Model.DData[i + 1].pdelta);
                // Вычисление силы
                double F = FrictionFunctions.F(Model.DData[i + 1]);
                // Вычисление расстояния сближения
                double deltah = FrictionFunctions.deltah(Model.F0, F, Model.DData[i + 1]);
                // Сдвиг
                if (Model.ShiftMode == 1) MarkovChainSchemes.ShiftRight(ref Model.DData[i + 1].p, deltah, Model.CData.h);
                if (Model.ShiftMode == 2) MarkovChainSchemes.ShiftLeft(ref Model.DData[i + 1].q, deltah, Model.CData.h);
                if (Model.ShiftMode == 3)
                {
                    MarkovChainSchemes.ShiftRight(ref Model.DData[i + 1].p, deltah / 2, Model.CData.h);
                    MarkovChainSchemes.ShiftLeft(ref Model.DData[i + 1].q, deltah / 2, Model.CData.h);
                }
                // Вычисление Хвоста и функции соответствующих распределений
                Model.DData[i + 1].InitFrompq(Model.CData.h);
                // Вычисление других характеристик
                double[] FrMas = FrictionFunctions.FrictionParams(Model.CData, Model.DData[0], Model.DData[i + 1], Model.SData);
                Model.FData[i + 1] = new FrictionData(FrMas);
            }
        }
        // Проследить эволюцию распределений в случае общей модели
        private static void FindSolutionGeneralScheme(ref ModelData Model)
        {
            int nt = Model.nt;
            int N = Model.DData[0].p.Length;
            double awxi = (Model.SData.wxi * Model.SData.wxi / Model.SData.Rwxi / Model.SData.Rwxi - 1) / 2;
            double bwxi = awxi;
            double aweta = (Model.SData.weta * Model.SData.weta / Model.SData.Rweta / Model.SData.Rweta - 1) / 2;
            double bweta = aweta;
            double[] fwxi = CalcFunctions.GetBetaDistr(awxi, bwxi, Model.SData.Scalewxi, Model.SData.dwxi, Model.SData.Nwxi);
            double[] Fwxi = CalcFunctions.GetFuncDistr(fwxi);
            double[] fweta = CalcFunctions.GetBetaDistr(aweta, bweta, Model.SData.Scaleweta, Model.SData.dweta, Model.SData.Nweta);
            double[] Fweta= CalcFunctions.GetFuncDistr(fweta);
            double eps = Math.Pow(10, -15);
            for (int i = 0; i < nt; i++)
            {
                // Расчет приращений распределений при пластической деформации
                double[] deltaDefp = MarkovChainSchemes.PlasticDefGeneralSchemexi(Model.CData, Model.SData, Model.DData[i]);
                double[] deltaDefq = MarkovChainSchemes.PlasticDefGeneralSchemeeta(Model.CData, Model.SData, Model.DData[i]);
                // Расчет приращений распределений при усталостном разрушении
                double[] deltaDestp = MarkovChainSchemes.DestroyGeneralSchemexi(Model.DData[i], fwxi, Fwxi, Model.Pwxi);
                double[] deltaDestq = MarkovChainSchemes.DestroyGeneralSchemeeta(Model.DData[i], fweta, Fweta, Model.Pweta);
                Model.DData[i + 1] = new DistributionData(N);
                // Вычисление распределений
                for (int j = 0; j < N; j++)
                    Model.DData[i + 1].p[j] = Model.DData[i].p[j] + deltaDefp[j] + deltaDestp[j];
                for (int j = 0; j < N; j++)
                    Model.DData[i + 1].q[j] = Model.DData[i].q[j] + deltaDefq[j] + deltaDestq[j];
                // Вычисление контактного пересечения
                Model.DData[i + 1].SetPp();
                Model.DData[i + 1].Setpdelta();
                Model.DData[i + 1].Sdelta = CalcFunctions.PosPart(Model.DData[i + 1].pdelta);
                // Вычисление силы
                double F = FrictionFunctions.F(Model.DData[i + 1]);
                // Вычисление расстояния сближения
                double deltah = Math.Abs(Model.F0 - F) / Model.DData[i + 1].Sdelta;
                // Сдвиг
                if (Model.ShiftMode == 1) MarkovChainSchemes.ShiftRight(ref Model.DData[i + 1].p, deltah, Model.CData.h);
                if (Model.ShiftMode == 2) MarkovChainSchemes.ShiftLeft(ref Model.DData[i + 1].q, deltah, Model.CData.h);
                if (Model.ShiftMode == 3)
                {
                    MarkovChainSchemes.ShiftRight(ref Model.DData[i + 1].p, deltah / 2, Model.CData.h);
                    MarkovChainSchemes.ShiftLeft(ref Model.DData[i + 1].q, deltah / 2, Model.CData.h);
                }
                // Вычисление Хвоста и функции соответствующих распределений
                Model.DData[i + 1].InitFrompq(Model.CData.h);
                Model.FData[i + 1] = new FrictionData();
            }
        }
    }
}
