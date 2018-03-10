using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarkChainsMechCalculations
{
    // Класс функций для графического отображения характеристик трения
    public static class ShowFunctions
    {
        // Отступления от границ PictureBox
        static private float FSpace = 15;
        // Нарисовать координатную сетку
        public static void DrawOXY(Graphics g, PictureBox P)
        {
            float Width = P.Width;
            float Height = P.Height;
            Pen p=new Pen(Color.Black);
            // OX
            g.DrawLine(p, FSpace, FSpace, FSpace, Height - FSpace * 2);
            g.DrawLine(p, FSpace, FSpace, FSpace / 2, FSpace * 2);
            g.DrawLine(p, FSpace, FSpace, FSpace * 3 / 2, FSpace * 2);
            // Верхняя линия
            g.DrawLine(p, FSpace / 3 * 2, FSpace * 2, Width - FSpace * 3, FSpace * 2);
            // OY
            g.DrawLine(p, FSpace, Height - FSpace * 2, Width - FSpace * 2, Height - FSpace * 2);
            g.DrawLine(p, Width - FSpace * 2, Height - FSpace * 2, Width - FSpace * 3, Height - FSpace * 3 / 2);
            g.DrawLine(p, Width - FSpace * 2, Height - FSpace * 2, Width - FSpace * 3, Height - FSpace * 5 / 2);
            // Правая линия 
            g.DrawLine(p, Width - FSpace * 3, FSpace * 2, Width - FSpace * 3, Height - FSpace * 3 / 2);
        }
        // Нарисовать график
        public static void DrawPlot(Graphics g, PictureBox P, double[] mas, Color C, double Max, float Pwidth)
        {
            int N = mas.Length;
            float Width = P.Width;
            float Height = P.Height;
            // Начало координат
            float NullX = FSpace;
            float NullY = Height - FSpace * 2;
            // Коэффициент сжатия по оси Y ( чтобы график "вмещался")
            double k = Convert.ToDouble((Height - FSpace * 4) / Max);
            // Шаг по оси ОХ
            float DeltaX = (Width - FSpace * 4) / N;
            // Отрисовка
            double[] ValueL1 = new double[2];
            Pen p = new Pen(C,Pwidth);
            for (int i = 1; i < N; i++)
            {
                ValueL1[0] = mas[i - 1];
                ValueL1[1] = mas[i];
                // Масштабирование
                double[] YL1 = new double[2];
                YL1[0] = k * ValueL1[0];
                YL1[1] = k * ValueL1[1];
                int x0 = (int)(NullX + (i - 1) * DeltaX);
                int x1 = (int)(NullX + i * DeltaX);
                int y0 = (int)(NullY - YL1[0]);
                int y1 = (int)(NullY - YL1[1]);
                g.DrawLine(p, x0, y0, x1, y1);
            }
        }
        // Нарисовать распределения
        public static void DrawPlotDistr(Graphics g, PictureBox P, ModelData Model, int index)
        {
            double Max = Model.DData[0].p.Max();
            double buf = Model.DData[0].q.Max();
            if (Max < buf) Max = buf;
            buf = Model.DData[index].p.Max();
            if (Max < buf) Max = buf;
            buf = Model.DData[index].q.Max();
            if (Max < buf) Max = buf;
            DrawPlot(g, P, Model.DData[0].p, Color.Red, Max,2);
            DrawPlot(g, P, Model.DData[0].q, Color.Blue, Max,2);
            DrawPlot(g, P, Model.DData[index].p, Color.Red, Max,1);
            DrawPlot(g, P, Model.DData[index].q, Color.Blue, Max,1);
            DrawScalesOXY(g, P, Model.CData.N * Model.CData.h, Max, 10, 10, "h, мкм", "");
        }
        // Нарисовать функцию и хвост распределений
        public static void DrawPlotFuncTailDistr(Graphics g, PictureBox P, ModelData Model, int index)
        {
            double Max = 1.0;
            DrawPlot(g, P, Model.DData[index].Fq, Color.Blue, Max, 2);
            DrawPlot(g, P, Model.DData[index].Tp, Color.Red, Max, 2);
            DrawScalesOXY(g, P, Model.CData.N * Model.CData.h, 1.0, 10, 10, "h, мкм", "");
        }
        // Нарисовать зависимость фрикционных параметров от времени
        public static void DrawFricFunc(Graphics g, PictureBox P, ModelData Model, string FricID)
        {
            string ScaleName = "";
            double L = Transformm(Model.deltaL * Model.nt, ref ScaleName);
            if (string.Compare(FricID, "Шероховатость") == 0)
            {
                double[] masp = new double[Model.nt + 1];
                double[] masq = new double[Model.nt + 1];
                for (int i = 0; i <= Model.nt; i++)
                {
                    masp[i] = Model.DData[i].Rpxi;
                    masq[i] = Model.DData[i].Rqeta;
                }
                double Max=masp[0];
                if (masq[0] > Max) Max = masq[0];
                DrawPlot(g, P, masp, Color.Red, Max, 1);
                DrawPlot(g, P, masq, Color.Blue, Max, 1);
                DrawScalesOXY(g, P, L, Max, 10, 10, ScaleName, "R, мкм");
            }
            if(string.Compare(FricID,"Линейный износ")==0)
            {
                double[] masIp = new double[Model.nt + 1];
                double[] masIq = new double[Model.nt + 1];
                for (int i = 0; i <= Model.nt; i++)
                {
                    masIp[i] = Model.FData[i].Ixi;
                    masIq[i] = Model.FData[i].Ieta;
                }
                double Max = masIp[Model.nt - 1];
                if (masIq[Model.nt - 1] > Max) Max = masIq[Model.nt];
                DrawPlot(g, P, masIp, Color.Red, Max, 1);
                DrawPlot(g, P, masIq, Color.Blue, Max, 1);
                DrawScalesOXY(g, P, L, Max, 10, 10, ScaleName, "I, мкм");
            }
            if(string.Compare(FricID,"Коэффициент трения")==0)
            {
                double[] masKtr = new double[Model.nt + 1];
                for (int i = 0; i <= Model.nt; i++)
                    masKtr[i] = Model.FData[i].kfr;
                double Max = masKtr.Max();
                DrawPlot(g, P, masKtr, Color.Black, Max, 1);
                DrawScalesOXY(g, P, L, Max, 10, 10, ScaleName, "k");
            }
        }
        // Нарисовать шкалу по осям OXY
        public static void DrawScalesOXY(Graphics g, PictureBox P, double MaxX, double MaxY, int countX, int countY, string Xname, string Yname)
        {
            float Width = P.Width;
            float Height = P.Height;
            Pen p = new Pen(Color.Black);
            float LenX = (Width - FSpace * 4) / countX;
            float LenY = (Height - FSpace * 4) / countY;
            double PriceX = MaxX / countX;
            double PriceY = MaxY / countY;
            Font font = new Font("Arial", 10);
            g.DrawString(Yname, font, Brushes.Black, 0, 0);
            g.DrawString(Xname, font, Brushes.Black, Width - FSpace * 4, Height - FSpace * 7 / 4);
            for (int i = 0; i < countX; i++)
            {
                g.DrawLine(p, FSpace + (i + 1) * LenX, FSpace * 4 / 3, FSpace + (i + 1) * LenX, FSpace * 8 / 3);
                g.DrawString(((i + 1) * PriceX).ToString("0.0"), font, Brushes.Black, (i + 1) * LenX - 5, FSpace * 2 / 3);
            }
            for (int i = 0; i < countY; i++)
            {
                g.DrawLine(p, Width - FSpace * 11 / 3, Height - FSpace * 2 - (i + 1) * LenY, Width - FSpace * 7 / 3, Height - FSpace * 2 - (i + 1) * LenY);
                g.DrawString(((i + 1) * PriceY).ToString("0.0000"), font, Brushes.Black, Width - FSpace * 9 / 3, Height - FSpace * 2 - (i + 1) * LenY);
            }

        }
        // Преобразовать м в другую единицу. Нужна для корректного отображения шкалы
        public static double Transformm(double Max,ref string ScaleName)
        {
            double val = Max;
            if (val > 1000) { val /= 1000; ScaleName = "L, км"; return val; }
            if (val > 100) { val /= 100; ScaleName = "L, гм"; return val; }
            if (val > 10) { val /= 10; ScaleName = "L, дам"; return val; }
            if (val > 0.1) { val *= 10; ScaleName = "L, дм"; return val; }
            if (val > 0.01) { val *= 100; ScaleName = "L, cм"; return val; }
            if (val > 0.001) { val *= 1000; ScaleName = "L, мм"; return val; }
            if (val > 0.000001) { val *= 1000000; ScaleName = "L, мкм"; return val; }
            return val;
        }
    }
}
