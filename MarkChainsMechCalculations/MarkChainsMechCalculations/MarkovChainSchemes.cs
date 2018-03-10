using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkChainsMechCalculations
{
    // Класс схем однородных цепей Маркова
    public static class MarkovChainSchemes
    {
        // Схема усталостного разрушения за произвольное время
        public static double[] DestroySchemexiAnyTime(DistributionData DData, double[][] Poxi, int k)
        {
            int N = DData.p.Length;
            if (k > N || k < 0) return null;
            double[] deltap = new double[N];
            for (int y = 0; y < k - 1; y++)
                for (int i = 0; i < 3; i++)
                    deltap[y] += DData.p[y + (i + 1) * k] * Poxi[y + (i + 1) * k][i];
            for (int y = k; y < 2 * k - 1; y++)
            {
                for (int i = 0; i < 3; i++)
                    deltap[y] += DData.p[y + (i + 1) * k] * Poxi[y + (i + 1) * k][i];
                deltap[y] -= DData.p[y] * Poxi[y][0];
            }
            for (int y = 2 * k; y < 3 * k - 1; y++)
            {
                for (int i = 0; i < 3; i++)
                    deltap[y] += DData.p[y + (i + 1) * k] * Poxi[y + (i + 1) * k][i];
                for (int i = 0; i < 2; i++)
                    deltap[y] -= DData.p[y] * Poxi[y][i];
            }
            for (int y = 3 * k; y < N - 3 * k; y++)
            {
                for (int i = 0; i < 3; i++)
                    deltap[y] += DData.p[y + (i + 1) * k] * Poxi[y + (i + 1) * k][i];
                for (int i = 0; i < 3; i++)
                    deltap[y] -= DData.p[y] * Poxi[y][i];
            }
            for (int y = N - 3 * k + 1; y < N - 2 * k; y++)
            {
                for (int i = 0; i < 2; i++)
                    deltap[y] += DData.p[y + (i + 1) * k] * Poxi[y + (i + 1) * k][i];
                for (int i = 0; i < 3; i++)
                    deltap[y] -= DData.p[y] * Poxi[y][i];
            }
            for (int y = N - 2 * k + 1; y < N - k; y++)
            {
                    deltap[y] += DData.p[y + k] * Poxi[y + k][0];
                for (int i = 0; i < 3; i++)
                    deltap[y] -= DData.p[y] * Poxi[y][i];
            }
            for (int y = N - k + 1; y < N; y++)
                for (int i = 0; i < 3; i++)
                    deltap[y] -= DData.p[y] * Poxi[y][i];

            return deltap;
        }
        public static double[] DestroySchemeetaAnyTime(DistributionData DData, double[][] Poeta, int k)
        {
            int N = DData.p.Length;
            if (k > N || k < 0) return null;
            double[] deltaq = new double[N];
            for (int y = 0; y < k - 1; y++)
                for (int i = 0; i < 3; i++)
                    deltaq[y] -= DData.q[y] * Poeta[y][i];
            for (int y = k; y < 2 * k - 1; y++)
            {
                for (int i = 0; i < 3; i++)
                    deltaq[y] -= DData.q[y] * Poeta[y][i];
                deltaq[y] += DData.q[y - k] * Poeta[y - k][0];
            }
            for (int y = 2 * k; y < 3 * k - 1; y++)
            {
                for (int i = 0; i < 3; i++)
                    deltaq[y] -= DData.q[y] * Poeta[y][i];
                for (int i = 0; i < 2; i++)
                    deltaq[y] += DData.q[y - (i + 1) * k] * Poeta[y - (i + 1) * k][i];
            }
            for (int y = 3 * k; y < N - 3 * k; y++)
            {
                for (int i = 0; i < 3; i++)
                    deltaq[y] -= DData.q[y] * Poeta[y][i];
                for (int i = 0; i < 3; i++)
                    deltaq[y] += DData.q[y - (i + 1) * k] * Poeta[y - (i + 1) * k][i];
            }
            for (int y = N - 3 * k + 1; y < N - 2 * k; y++)
            {
                for (int i = 0; i < 2; i++)
                    deltaq[y] -= DData.q[y] * Poeta[y][i];
                for (int i = 0; i < 3; i++)
                    deltaq[y] += DData.q[y - (i + 1) * k] * Poeta[y - (i + 1) * k][i];
            }
            for (int y = N - 2 * k + 1; y < N - k; y++)
            {
                deltaq[y] -= DData.q[y] * Poeta[y][0];
                for (int i = 0; i < 3; i++)
                    deltaq[y] += DData.q[y - (i + 1) * k] * Poeta[y - (i + 1) * k][i];

            }
            for (int y = N - k + 1; y < N; y++)
                for (int i = 0; i < 3; i++)
                    deltaq[y] += DData.q[y - (i + 1) * k] * Poeta[y - (i + 1) * k][i];
            return deltaq;
        }
        // Схема усталостного разрушения
        public static double[] DestroyGeneralSchemexi(DistributionData DData, double[] fwxi, double[] Fwxi, double Pwxi)
        {
            int N = DData.p.Length;
            int Nwxi = fwxi.Length;
            if (Nwxi != Fwxi.Length || Nwxi > N || Fwxi.Length > N) return null;
            double[] deltap = new double[N];
            for (int i = 0; i < N; i++)
            {
                deltap[i] = -DData.p[i] * DData.Fq[i] * Fwxi[Convert.ToInt32(Math.Min(i, Nwxi - 1))];
                for (int j = i + 1; j < Convert.ToInt32(Math.Min(i + Nwxi, N)); j++)
                    deltap[i] += DData.p[j] * DData.Fq[j] * fwxi[j - i];
                deltap[i] *= Pwxi;
            }
            return deltap;
        }
        public static double[] DestroyGeneralSchemeeta(DistributionData DData, double[] fweta, double[] Fweta, double Pweta)
        {
            int N = DData.q.Length;
            int Nweta = fweta.Length;
            if (Nweta != Fweta.Length || Nweta > N || Fweta.Length > N) return null;
            double[] deltaq = new double[N];
            for (int i = 0; i < N; i++)
            {
                deltaq[i] = -DData.q[i] * DData.Tp[i] * Fweta[Convert.ToInt32(Math.Min(Nweta - 1, N - 1 - i))];
                for (int j = Convert.ToInt32(Math.Max(0, i - Nweta)); j < i; j++)
                    deltaq[i] += DData.q[j] * DData.Tp[j] * fweta[i - j - 1];
                deltaq[i] *= Pweta;
            }
            return deltaq;
        }
        // Схема пластической деформации
        public static double[] PlasticDefGeneralSchemexi(CalcData CData, SurfaceData SData, DistributionData DData)
        {
            int N = DData.p.Length;
            int k = Convert.ToInt32(Math.Floor(SData.DeltaDef / CData.h));
            if (k > N) return null;
            double[] deltap = new double[N];
            for (int i = k; i < N; i++)
                deltap[i] = SData.Pm * ((-DData.Fq[i - k] + DData.q[i - k]) * DData.p[i] + DData.q[i - k] * DData.Tp[i]);
            return deltap;
        }
        public static double[] PlasticDefGeneralSchemeeta(CalcData CData, SurfaceData SData, DistributionData DData)
        {
            int N = DData.q.Length;
            int k = Convert.ToInt32(Math.Floor(SData.DeltaDef / CData.h));
            double Qm = 1 - SData.Pm;
            if (k > N) return null;
            double[] deltaq = new double[N];
            for (int i = 0; i < N - k; i++)
                deltaq[i] = Qm * (-DData.Tp[i + k] * DData.q[i] + (DData.Fq[i] - DData.q[i]) * DData.p[i + k]);
            return deltaq;
        }
        // Учет постоянной нагрузки. Сдвиг влево
        public static void ShiftLeft(ref double[] q, double deltah, double h)
        {
            int N = q.Length;
            double[] qn = new double[N];
            if(deltah < h && deltah > double.Epsilon)
            { 
                for (int i = 0; i < N - 1; i++)
                    qn[i] = (1 - deltah / h) * q[i] + (deltah / h) * q[i];
                qn[N-1] = 0;
            }
            else
            {
                if (deltah < -double.Epsilon) return;
                int nh = Convert.ToInt32(Math.Truncate(deltah / h));
                if (nh > N) return;
                for (int i = 0; i < N - nh; i++)
                    qn[i] = q[i + nh];
                for (int i = N - nh; i < N; i++)
                    qn[i] = 0;
                deltah -= Convert.ToDouble(nh) * h;
                if(deltah>=double.Epsilon)
                    for (int i = 0; i < N - 1; i++)
                        qn[i] = (1 - (deltah / h)) * q[i] + (deltah / h) * q[i + 1];
            }
            for (int i = 0; i < N; i++)
                q[i] = qn[i];
        }
        // Учет постоянной нагрузки. Сдвиг вправо
        public static void ShiftRight(ref double[] p, double deltah, double h)
        {
            int N = p.Length;
            double[] pn = new double[N];
            if (double.IsNaN(deltah)==true) return;
            if (deltah < h && deltah > double.Epsilon)
            {
                for (int i = 1; i < N; i++)
                    pn[i] = (1 - deltah / h) * p[i] + (deltah / h) * p[i - 1];
                pn[N - 1] = 0;
            }
            else
            {
                if (deltah < -double.Epsilon) return;
                int nh = Convert.ToInt32(Math.Truncate(deltah / h));
                if (nh > N) return;
                for (int i = N-1; i >= nh; i--)
                    pn[i] = p[i - nh];
                for (int i = 0; i < nh; i++)
                    pn[i] = 0;
                deltah -= Convert.ToDouble(nh) * h;
                if (deltah >= double.Epsilon)
                    for (int i = 1; i < N; i++)
                        pn[i] = (1 - deltah / h) * p[i] + (deltah / h) * p[i - 1];
                pn[N-1] = 0;
            }
            for (int i = 0; i < N; i++)
                p[i] = pn[i];
        }
    }
}
