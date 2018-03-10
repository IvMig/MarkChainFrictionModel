using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkChainsMechCalculations
{
    // Класс данных трения
    public class FrictionData
    {
        // Средняя площадь контакта в мкм^2
        public double ar;
        // Нормальная сила контакта
        public double F;
        // Полная сила трения в 10^-3 Н
        public double ffr;
        // Коэффициент трения
        public double kfr;
        // Средний линейный износ в мкм для нижней поверхности
        public double Ixi;
        // Средний линейный износ в мкм для верхней поверхности
        public double Ieta;
        // Конструктор по умолчанию
        public FrictionData()
        {
            F = 0.0;
            ar = 0.0;
            ffr = 0.0;
            kfr = 0.0;
            Ixi = 0.0;
            Ieta = 0.0;
        }
        // Конструктор с параметрами
        public FrictionData(double[] mas)
        {
            ar = mas[0];
            F = mas[1];
            ffr = mas[2];
            kfr = mas[3];
            Ixi = mas[4];
            Ieta = mas[5];
        }
    }
}
