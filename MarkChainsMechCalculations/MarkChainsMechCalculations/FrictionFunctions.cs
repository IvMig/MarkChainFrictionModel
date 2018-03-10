using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkChainsMechCalculations
{
    // Класс формул для расчета характеристик трения
    public static class FrictionFunctions
    {
        // Вычислить площадь контакта
        public static double ar(double delta, double Rm)
        {
            return Math.PI * delta * Rm;
        }
        // Вычислить нормальное напряжение
        public static double sigma(double delta, double Rm, double theta)
        {
            double val = 0.0;
            if (delta > double.Epsilon)
                val = 4.0 / 3.0 / Math.PI / theta * Math.Sqrt(delta / Rm);
            return val;
        }
        // Вычислить тангенциальное напряжение
        public static double tau(double taumol, double betamol, double sigma)
        {
            return taumol + betamol * sigma;
        }
        // Вычислить молекулярную силу трения
        public static double ffrmol(double tau, double ar)
        {
            return tau * ar;
        }
        // Вычислить деформационную силу трения при упругой деформации элементов
        public static double ffrdef(double delta, double thetaxi, double thetaeta, double alphaxi, double alphaeta)
        {
            return 0.25 * (alphaxi / thetaxi + alphaeta / thetaeta) * delta * delta;
        }
        // Вычислить полную силу трения
        public static double ffr(double ffrmol, double ffrdef)
        {
            return ffrmol + ffrdef;
        }
        // Вычислить коэффициент трения
        public static double kfr(double Ftr, double F)
        {
            double val = 0.0;
            if (F > double.Epsilon)
                val = Ftr / F;
            return val;
        }
        // Вычислить средний линейный износ
        public static double I(double[] p0, double[] p)
        {
            double Mp0 = CalcFunctions.MathExpectation(p0);
            double Mp = CalcFunctions.MathExpectation(p);
            double val = Math.Abs(Mp0 - Mp);
            return val;
        }
        // Вычислить нагрузку
        public static double F(DistributionData DData)
        {
            double M = CalcFunctions.MathExpectation(DData.pdelta);
            double val = M;
            return M;
        }
        // Вычислить расстояние сдвига
        public static double deltah(double F0, double F, DistributionData DData)
        {
            double Sd = DData.Sdelta;
            double pd0 = DData.pdelta[0];
            double val = (Math.Sqrt(2 * pd0 * (F0 - F) + Sd * Sd) - Sd) / pd0;
            //double val = (F0 - F) / (Sd);
            return val;
        }
        // Вычислить параметры трения
        public static double[] FrictionParams(CalcData CData, DistributionData DData0, DistributionData DData, SurfaceData SData)
        {
            int Pp = DData.Pp[2];
            double[] mas = new double[8];
            double h = CData.h;
            double Rm = SData.Rm;
            double theta = SData.theta;
            double taumol = SData.taumol;
            double betamol = SData.betamol;
            double alphaxi = SData.alphaxi; double alphaeta = SData.alphaeta;
            double thetaxi = SData.thetaxi; double thetaeta = SData.thetaeta;
            for(int i = 0; i < Pp; i++)
            {
                double z = Convert.ToDouble(i) * CalcFunctions.mum(h);
                double arval = ar(z, Rm);
                double sigmaval = sigma(z, Rm, theta);
                mas[0] += arval * DData.pdelta[i];
                mas[1] += arval * sigmaval * DData.pdelta[i];
                
                double tauval = tau(taumol, betamol, sigmaval);
                double ffrmolval = ffrmol(tauval, arval);
                double ffrdefval = ffrdef(z, thetaxi, thetaeta, alphaxi, alphaeta);
                double ffrval = ffr(ffrmolval, ffrdefval);
                mas[2] += ffrval * DData.pdelta[i];
            }
            if(mas[1] > double.Epsilon)
                mas[3] = mas[2] / mas[1];
            mas[4] = h * I(DData0.p, DData.p);
            mas[5] = h * I(DData0.q, DData.q);
            return mas;
        }
    }
}
