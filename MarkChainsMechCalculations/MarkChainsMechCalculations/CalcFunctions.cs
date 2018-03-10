using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkChainsMechCalculations
{
    // Класс для других функций
    public static class CalcFunctions
    {
        // Вычислить матожидание
        public static double MathExpectation(double[] p)
        {
            int N = p.Length;
            double val=0.0;
            for (int i = 0; i < N; i++)
                val += i * p[i];
            return val;
        }
        // Вычислить матожидание в мкм
        public static double MathExpectation(double[] p, double h)
        {
            int N = p.Length;
            double val = 0.0;
            for (int i = 0; i < N; i++)
                val += i * p[i];
            return val*h;
        }
        // Вычислить шероховатость
        public static double Roughness(double[] p, double M)
        {
            int N = p.Length;
            double val = 0.0;
            for (int i = 0; i < N; i++)
                val += (i - M) * (i - M) * p[i];
            val = Math.Sqrt(val);
            return val;
        }
        // Вычислить шероховатость в мкм
        public static double Roughness(double[] p, double M, double h)
        {
            int N = p.Length;
            double val = 0.0;
            for (int i = 0; i < N; i++)
                val += (i - M) * (i - M) * p[i];
            val = Math.Sqrt(val);
            return val * h;
        }
        // Вычислить положительную долю контактного пересечения
        public static double PosPart(double[] p)
        {
            int N = p.Length;
            double val = 0.0;
            for(int i = 0; i < N; i++)
                val += p[i];
            return val;
        }
        // Нормировать распределение контактного пересечения
        public static void Normp(ref double[] p)
        {
            double S = PosPart(p);
            int N = p.Length;
            for (int i = 0; i < N; i++)
                p[i] = p[i] / S;
        }
        // Нормировать распределение контактного пересечения
        public static void Normp(ref double[] p, double S)
        {
            int N = p.Length;
            for (int i = 0; i < N; i++)
                p[i] = p[i] / S;
        }
        // Вычислить вероятность по закону Пуассона с параметром а
        public static double Po(int i, double a)
        {
            int frac = 1;
            for (int j = 2; j <= i; j++)
                frac *= j;
            double val = Math.Pow(a, frac) * Math.Exp(-a) / Convert.ToDouble(frac);
            return val;
        }
        // Вычислить вероятности по закону Пуассона для вектора параметров
        public static double[][] Po(double[] a)
        {
            int N = a.Length;
            double[][] val = new double[N][];
            for (int i = 0; i < N; i++)
            {
                val[i] = new double[3];
                for (int j = 1; j <= 3; j++)
                    val[i][j - 1] = Po(j, a[i]);
            }
            return val;
        }
        // Преобразовать в мкм
        public static double mum( double val)
        {
            return val * 0.000001;
        }
        // Преобразовать в ГПа
        public static double GPa(double val)
        {
            return val * 1000000000;
        }
        // Преобразовать в МПа
        public static double MPa(double val)
        {
            return val * 1000000;
        }
        // Сделать сдвиг массива
        public static void ShiftMas(ref double[] p, int d)
        {
            int N = p.Length;
            if (Math.Abs(d) <= N)
                if (d >= 0)
                    ShiftMasRight(ref p, d);
                else
                    ShiftMasLeft(ref p, d);
        }
        // Сделать сдвиг массива вправо
        private static void ShiftMasRight(ref double[] p, int d)
        {
            int N = p.Length;
            for (int i = d; i < N; i++)
                p[N - 1 - i + d] = p[N - 1 - i];
            for (int i = 0; i < d; i++)
                p[i] = 0.0;
        }
        // Сделать сдвиг массива влево
        private static void ShiftMasLeft(ref double[] p, int d)
        {
            int N = p.Length;
            for (int i = 0; i < N - d; i++)
                p[i] = p[i + d];
            for (int i = N - d; i < N; i++)
                p[i] = 0.0;
        }
        // Вычислить параметры закона Пуассона
        public static double[] FindParamPo(double nPw,double[] Fp)
        {
            int N = Fp.Length;
            double[] val = new double[N];
            for (int i = 0; i < N; i++)
                val[i] = nPw * Fp[i];
            return val;
        }
        // Задать смещенное, сжатое бета-распределение
        public static double[] GetBetaDistr(double a, double b, double Scale, int d, int N)
        {
            double Nd = Convert.ToDouble(N);
            double[] mas = new double[N];
            int NDistr = Convert.ToInt32(Nd / Convert.ToDouble(Scale));
            for (int i = 0; i < N; i++)
                if (i < NDistr)
                {
                    double x = Convert.ToDouble(i) / Convert.ToDouble(NDistr);
                    mas[i] = Math.Pow(x, a - 1) * Math.Pow(1 - x, b - 1);
                }
                else
                    mas[i] = 0.0;
            double S = CalcFunctions.PosPart(mas);
            Normp(ref mas, S);
            ShiftMas(ref mas, d);
            return mas;
        }
        // Задать функцию распределения
        public static double[] GetFuncDistr(double[] p)
        {
            int N = p.Length;
            double[] Fq = new double[N];
            Fq[0] = 0;
            for (int i = 1; i < N; i++)
                Fq[i] = Fq[i - 1] + p[i];
            return Fq;
        }
        // Задать хвост распределения
        public static double[] GetTailDistr(double[] p)
        {
            int N = p.Length;
            double[] Tp = new double[N];
            Tp[0] = 1.0;
            for (int i = 1; i < N; i++)
                Tp[i] = Tp[i - 1] - p[i];
            return Tp;
        }
        // Вычислить путь трения
        public static double GetL(ModelData Model, int TimeIndex)
        {
            double L = 0.0;
            if (Model.mode == 0) L = Convert.ToInt32(Model.deltaL) * TimeIndex;
            if (Model.mode == 1) L = Model.SData.deltaSxi * TimeIndex;
            return L;
        }
    }
}
