using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkChainsMechCalculations
{
    // Класс данных для распределений
    public class DistributionData
    {
        // Распределения поверхностей
        public double[] p; public double[] q;
        // Cредние высоты поверхностей в мкм
        public double Mxi; public double Meta;
        // Шероховатости поверхностей в мкм
        public double Rpxi; public double Rqeta;
        // Распределение контактного пересечения
        public double[] pdelta;
        // Положительная доля контактного пересечения
        public double Sdelta;
        // Границы пересечения распределений поверхностей
        public int[] Pp;
        // Хвост распределения p
        public double[] Tp;
        // Функция распределения q
        public double[] Fq;
        // Конструктор по умолчанию
        public DistributionData()
        {
            p = null; q = null;
            Mxi = 0.0; Meta = 0.0;
            Rpxi = 0.0; Rqeta = 0.0;
            pdelta = null;
            Sdelta = 0.0;
            Pp = null;
            Tp = null;
            Fq = null;
        }
        // Конструктор с параметрами
        public DistributionData(int n)
        {
            p = new double[n]; q = new double[n];
            Mxi = 0.0; Meta = 0.0;
            Rpxi = 0.0; Rqeta = 0.0;
            pdelta = null;
            Sdelta = 0.0;
            Pp = null;
            Tp = new double[n];
            Fq = new double[n];
        }
        // Конструктор с параметрами
        public DistributionData(double[] newp, double[] newq)
        {
            int N = newp.Length;
            if(N == newq.Length)
            {
                p=new double[N]; q=new double[N];
                for(int i=0; i<N; i++)
                {
                    p[i] = newp[i]; q[i] = newq[i]; 
                }
                Mxi = CalcFunctions.MathExpectation(p);
                Meta = CalcFunctions.MathExpectation(q);
                Rpxi = CalcFunctions.Roughness(p, Mxi);
                Rqeta = CalcFunctions.Roughness(q, Meta);
                SetPp();
                Setpdelta();
                Tp = CalcFunctions.GetTailDistr(p);
                Fq = CalcFunctions.GetFuncDistr(q);
                Sdelta = CalcFunctions.PosPart(pdelta);
                CalcFunctions.Normp(ref pdelta, Sdelta);
            }
        }
        // Конструктор с параметрами
        public DistributionData(CalcData CData, SurfaceData SData)
        {
            SetDistributions(CData, SData);
            double h = CData.h;
            Mxi = CalcFunctions.MathExpectation(p);
            Meta = CalcFunctions.MathExpectation(q);
            Rpxi = CalcFunctions.Roughness(p, Mxi, h);
            Rqeta = CalcFunctions.Roughness(q, Meta, h);
            Mxi *= h;
            Meta *= h;
            SetPp();
            Setpdelta();
            Tp = CalcFunctions.GetTailDistr(p);
            Fq = CalcFunctions.GetFuncDistr(q);
            Sdelta = CalcFunctions.PosPart(pdelta);
        }
        // Конструктор с параметрами
        public DistributionData(double axi, double bxi, double aeta, double beta, double Scalexi, double Scaleeta, int dxi, int deta, int N, double h)
        {
            p = CalcFunctions.GetBetaDistr(axi, bxi, Scalexi, dxi, N);
            q = CalcFunctions.GetBetaDistr(aeta, beta, Scaleeta, deta, N);
            Mxi = CalcFunctions.MathExpectation(p);
            Meta = CalcFunctions.MathExpectation(q);
            Rpxi = CalcFunctions.Roughness(p, Mxi, h);
            Rqeta = CalcFunctions.Roughness(q, Meta, h);
            Mxi *= h;
            Meta *= h;
            SetPp();
            Setpdelta();
            Tp = CalcFunctions.GetTailDistr(p);
            Fq = CalcFunctions.GetFuncDistr(q);
            Sdelta = CalcFunctions.PosPart(pdelta);
        }
        // Задать распределения
        private void SetDistributions(CalcData CData, SurfaceData SData)
        {
            double axi = SData.axi;         double bxi = SData.bxi;
            double aeta = SData.aeta;       double beta = SData.beta;
            double Scalexi = CData.Scalexi; double Scaleeta = CData.Scaleeta;
            int dxi = CData.dxi;            int deta = CData.deta;
            int N = CData.N;
            p = CalcFunctions.GetBetaDistr(axi, bxi, Scalexi, dxi, N);
            q = CalcFunctions.GetBetaDistr(aeta, beta, Scaleeta, deta, N);
        }
        // Вычислить параметр нагрузки 0 - низ, 1 - верх, 2 - разность
        public void SetPp()
        {
            Pp = new int[3];
            int N = p.Length;
            Pp[0] = 0;
            Pp[1] = N - 1;
            if (N == q.Length)
            {
                bool fl = false;
                for (int i = 0; i < N; i++)
                {
                    if (p[i] > double.Epsilon && q[i] > double.Epsilon && !fl)
                    { fl = !fl; Pp[0] = i; }
                    if (p[i] < double.Epsilon && q[i] < double.Epsilon && fl)
                    { Pp[1] = i - 1; break; }
                }

                Pp[2] = Pp[1] - Pp[0];
            }
        }
        // Вычислить распределение контактного пересечения
        public void Setpdelta()
        {
            pdelta = new double[Pp[2]];
            int N = p.Length;
            if (N != q.Length)
                return;
            for (int i = 0; i < Pp[2]; i++)
            {
                pdelta[i] = 0.0;
                for (int j = Pp[0]; j < Pp[1]; j++)
                    pdelta[i] += q[j] * p[Math.Min(j + i, N - 1)];
            }
        }
        // Инициализировать данные в случае известных распределений p,q,pdelta
        public void InitFrompq()
        {
            Mxi = CalcFunctions.MathExpectation(p);
            Meta = CalcFunctions.MathExpectation(q);
            Rpxi = CalcFunctions.Roughness(p, Mxi);
            Rqeta = CalcFunctions.Roughness(q, Meta);
            Tp = CalcFunctions.GetTailDistr(p);
            Fq = CalcFunctions.GetFuncDistr(q);
        }
        // Инициализировать данные в случае известных распределений p,q,pdelta, h - шаг дискретизации
        public void InitFrompq(double h)
        {
            Mxi = CalcFunctions.MathExpectation(p);
            Meta = CalcFunctions.MathExpectation(q);
            Rpxi = CalcFunctions.Roughness(p, Mxi, h);
            Rqeta = CalcFunctions.Roughness(q, Meta, h);
            Mxi *= h;
            Meta *= h;
            Tp = CalcFunctions.GetTailDistr(p);
            Fq = CalcFunctions.GetFuncDistr(q);
        }
    }
}
