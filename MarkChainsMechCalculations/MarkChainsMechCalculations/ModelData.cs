using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkChainsMechCalculations
{
    // Класс данных модели
    public class ModelData
    {
        // Какая модель 0 - для усталостного разрушения
        // 1 - для общего вида взаимодействия
        public int mode;
        // Режим постоянной нагрузки 
        // 1- сдвиг нижней поверхности
        // 2- сдвиг верхней поверхности
        // 3- сдвиг обеих поверхностей
        // остальное - сдвига нет
        public int ShiftMode;
        // Число временных шагов
        public int nt;
        // Число шагов, равных частице разрушения
        public int kxi; public int keta;
        // Количество выступов поверхностей за путь трения
        public int nxi; public int neta;
        // Вероятность разрушения при контакте
        public double Pwxi; public double Pweta;
        // Путь трения в м
        public double deltaL;
        // Произведение вероятности разрушения на количество выступов
        public double netaPwxi; public double nxiPweta;
        // Значение постоянной нагрузки
        public double F0;
        // Данные для расчетов
        public CalcData CData;
        // Данные поверхностей
        public SurfaceData SData;
        // Массивы данных трения и распределений
        public DistributionData[] DData;
        public FrictionData[] FData;
                
        // Конструктор по умолчанию
        public ModelData()
        {
            mode = -1;
            CData = new CalcData();
            SData = new SurfaceData();
            nt = 0;
            Pwxi = 0.0; Pweta = 0.0;
            deltaL = 0.0;
            nxi = 0; neta = 0;
            netaPwxi = 0.0; nxiPweta = 0.0;
            FData = null; DData = null;
            ShiftMode = -1;
        }
        // Конструктор с параметрами
        public ModelData(int[] masCalc, double maxd, double[] masSurf, int time, double[] masModel)
        {
            mode = -1;
            CData = new CalcData(masCalc, maxd);
            SData = new SurfaceData(masSurf);
            nt = time;
            kxi = Convert.ToInt32(Math.Floor(SData.wxi / CData.h));
            keta = Convert.ToInt32(Math.Floor(SData.weta / CData.h));
            Pwxi = masModel[0]; Pweta = masModel[1];
            ShiftMode = Convert.ToInt32(masModel[2]);
            deltaL = Math.Ceiling(0.01 * Math.Max(SData.deltaSeta / Pwxi, SData.deltaSxi / Pweta));
            nxi = Convert.ToInt32(deltaL / SData.deltaSxi); neta = Convert.ToInt32(deltaL / SData.deltaSeta);
            netaPwxi = Convert.ToDouble(neta) * Pwxi; nxiPweta = Convert.ToDouble(nxi)*Pweta;
            FData = new FrictionData[nt + 1]; DData = new DistributionData[nt + 1];
            DData[0] = new DistributionData(CData, SData);
            FData[0] = new FrictionData(FrictionFunctions.FrictionParams(CData,DData[0],DData[0],SData));
            F0 = FrictionFunctions.F(DData[0]);
        }
    }
}
