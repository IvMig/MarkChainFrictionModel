using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkChainsMechCalculations
{
    // Класс данных для расчетов
    public class CalcData
    {
        // Коэффициенты сжатия
        public int Scalexi; public int Scaleeta;
        // Параметры сдвига
        public int dxi; public int deta;
        // Число уровней высот
        public int N;
        // Максимальный уровень высот в мкм
        public double maxd;
        // Шаг дискретизации высот в мкм
        public double h;

        // Конструктор по умолчанию
        public CalcData()
        {
            Scalexi = 0; Scaleeta = 0;
            dxi = 0; deta = 0;
            N = 0;
            maxd = 0.0;
            h = 0.0;
        }
        // Констркутор с параметрами
        public CalcData(int[] mas, double d)
        {
            Scalexi = mas[0]; Scaleeta = mas[1];
            dxi = mas[2]; deta = mas[3];
            N = mas[4];
            maxd = d;
            h = maxd / Convert.ToDouble(N);
        }
    }
}
