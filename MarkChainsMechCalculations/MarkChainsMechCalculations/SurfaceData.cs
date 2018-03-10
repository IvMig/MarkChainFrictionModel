using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkChainsMechCalculations
{
    // Класс данных о поверхностях
    public class SurfaceData
    {
        // Высота неровностей поверхностей в м
        public double mxi;      public double meta;
        // Среднее квадратичное отклонение профилей в м
        public double sigmaxi;  public double sigmaeta;
        // Коэффициенты Пуассона (безразмерны)
        public double nuxi;     public double nueta;
        // Модули Юнга в ГПа
        public double Exi;      public double Eeta;
        // Комплексные физические постоянные в 1/Па
        public double thetaxi;  public double thetaeta;
        public double theta;
        // Коэффициенты гистерезисных потерь (безразмерны)
        public double alphaxi;  public double alphaeta;
        // Средние радиусы кривизны вершин выступов в м
        public double Rmxi;     public double Rmeta;
        // Приведенный радиус в мкм
        public double Rm;
        // Среднее расстояние между выступами в м
        public double deltaSxi; public double deltaSeta;
        // Средний размер частицы разрушения в мкм
        public double wxi;      public double weta;
        // Cреднеквадратичное отклонение размера частиц разрушения в мкм
        public double Rwxi; public double Rweta;
        // Порог упругости в мкм
        public double DeltaDef;
        // Коэффициент различия материалов
        public double Pm;
        // Коэффициент растяжения/сжатия распределения размера частицы
        public double Scalewxi; public double Scaleweta;
        // Сдвиг распределения размера частицы
        public int dwxi; public int dweta;
        // Число элементов в дискретном распределении размера частицы
        public int Nwxi; public int Nweta;
        // Параметры сдвиговой прочности молекулярной связи 1 - в Па, 2 - безразмерна
        public double taumol;   public double betamol;
        //  Параметры бета распределения
        public double axi;      public double bxi;
        public double aeta;     public double beta;

        // Конструктор по умолчанию
        public SurfaceData()
        {
            mxi = 0.0;      meta = 0.0;
            sigmaxi = 0.0;  sigmaeta = 0.0;
            nuxi = 0.0;     nueta = 0.0;
            Exi = 0.0;      Eeta = 0.0;
            thetaxi = 0.0;  thetaeta = 0.0;
            theta = 0.0;
            alphaxi = 0.0;  alphaeta = 0.0;
            Rmxi = 0.0;     Rmeta = 0.0;
            Rm = 0.0;
            deltaSxi = 0.0; deltaSeta = 0.0;
            wxi = 0.0;      weta = 0.0;
            Rwxi=0.0;       Rweta=0.0;
            DeltaDef=0.0; 
            Pm = 0.0;
            Scalewxi = 0.0; Scaleweta = 0.0;
            dwxi = 0;       dweta = 0;
            Nwxi = 0;       Nweta = 0;
            taumol = 0.0;   betamol = 0.0;
            axi=0.0;        bxi=0.0;
            aeta=0.0;       beta=0.0;
        }
        // Конструктор с параметрами
        public SurfaceData(double[] mas)
        {
            mxi = mas[0];       meta = mas[1];
            sigmaxi = mas[2];   sigmaeta = mas[3];
            nuxi = mas[4];      nueta = mas[5];
            Exi = mas[6];       Eeta = mas[7];
            thetaxi = (1 - nuxi * nuxi) / Exi; thetaeta = (1 - nueta * nueta) / Eeta;
            theta = thetaxi + thetaeta;
            alphaxi = mas[8];   alphaeta = mas[9];
            Rmxi = mas[10];     Rmeta = mas[11];
            Rm = 1 / (1 / Rmxi + 1 / Rmeta);
            deltaSxi = mas[12]; deltaSeta = mas[13];
            wxi = mas[14];      weta = mas[15];
            Rwxi = mas[16];     Rweta = mas[17];
            DeltaDef = mas[18]; 
            Pm = mas[19];
            Scalewxi = mas[20]; Scaleweta = mas[21];
            dwxi = Convert.ToInt32(mas[22]); dweta = Convert.ToInt32(mas[23]);
            Nwxi = Convert.ToInt32(mas[24]); Nweta = Convert.ToInt32(mas[25]);
            taumol = mas[26];   betamol = mas[27];
            axi = ((mxi * mxi) / (sigmaxi * sigmaxi) - 1.0) / 2;      bxi = axi;
            aeta = ((meta * meta) / (sigmaeta * sigmaeta) - 1.0) / 2; beta = aeta;
        }
    }
}
