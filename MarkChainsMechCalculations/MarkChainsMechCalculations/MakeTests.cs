using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkChainsMechCalculations
{
    // Класс для создания тестов
    static public class MakeTests
    {
        // Тест для случая сопряжения Поршень-Цилиндр
        static public void TestPistonCylinderLiner(ref int[] masCalc, ref double maxd, ref double[] masSurf, ref int time, ref double[] masModel)
        {
            masCalc = new int[5];
            masCalc[0] = 1;
            masCalc[1] = 2;
            masCalc[2] = 0;
            masCalc[3] = 160;
            masCalc[4] = 450;
            maxd = 20;
            masSurf = new double[28];
            masSurf[0] = CalcFunctions.mum(10.0);
            masSurf[1] = CalcFunctions.mum(5.0);
            masSurf[2] = CalcFunctions.mum(1.7);
            masSurf[3] = CalcFunctions.mum(0.6);
            masSurf[4] = 0.28;
            masSurf[5] = 0.34;
            masSurf[6]=CalcFunctions.GPa(172.0);
            masSurf[7]=CalcFunctions.GPa(102.0);
            masSurf[8] = 0.03;
            masSurf[9] = 0.02;
            masSurf[10] = CalcFunctions.mum(1000.0);
            masSurf[11] = CalcFunctions.mum(100.0);
            masSurf[12] = CalcFunctions.mum(100.0);
            masSurf[13] = CalcFunctions.mum(30.0);
            masSurf[14] = 0.2;
            masSurf[15] = 0.2;
            masSurf[16] = 0.0;
            masSurf[17] = 0.0;
            masSurf[18] = 0.0;
            masSurf[19] = 0.0;
            masSurf[20] = 0.0;
            masSurf[21] = 0.0;
            masSurf[22] = 0.0;
            masSurf[23] = 0.0;
            masSurf[24] = 0.0;
            masSurf[25] = 0.0;
            masSurf[26] = CalcFunctions.MPa(30.0);
            masSurf[27] = 0.043;
            time = 8000;
            masModel = new double[3];
            masModel[0] = 0.7 * Math.Pow(10.0, -8.0);
            masModel[1] = 0.7 * Math.Pow(10.0, -8.0);
            masModel[2] = 1.0;
        }
        // Тест для случая моделирования из диссертации Тигетова
        static public void TestModelingDissertation(ref int[] masCalc, ref double maxd, ref double[] masSurf, ref int time, ref double[] masModel)
        {
            masCalc = new int[5];
            masCalc[0] = 2;
            masCalc[1] = 2;
            masCalc[2] = 0;
            masCalc[3] = 113;
            masCalc[4] = 450;
            maxd = 20;
            masSurf = new double[28];
            masSurf[0] = CalcFunctions.mum(5.0);
            masSurf[1] = CalcFunctions.mum(5.0);
            masSurf[2] = CalcFunctions.mum(1.7);
            masSurf[3] = CalcFunctions.mum(1.2);
            masSurf[4] = 0.27;
            masSurf[5] = 0.25;
            masSurf[6] = CalcFunctions.GPa(190.0);
            masSurf[7] = CalcFunctions.GPa(200.0);
            masSurf[8] = 0.03;
            masSurf[9] = 0.02;
            masSurf[10] = CalcFunctions.mum(350.0);
            masSurf[11] = CalcFunctions.mum(400.0);
            masSurf[12] = CalcFunctions.mum(40.0);
            masSurf[13] = CalcFunctions.mum(40.0);
            masSurf[14] = 0.2;
            masSurf[15] = 0.2;
            masSurf[16] = 0.0;
            masSurf[17] = 0.0;
            masSurf[18] = 0.0;
            masSurf[19] = 0.0;
            masSurf[20] = 0.0;
            masSurf[21] = 0.0;
            masSurf[22] = 0.0;
            masSurf[23] = 0.0;
            masSurf[24] = 0.0;
            masSurf[25] = 0.0;
            masSurf[26] = CalcFunctions.MPa(1.0);
            masSurf[27] = 0.04;
            time = 3200;
            masModel = new double[3];
            masModel[0] = 4.8 * Math.Pow(10.0, -8.0);
            masModel[1] = 96.0 * Math.Pow(10.0, -8.0);
            masModel[2] = 2.0;
        }
        // Тест для случая из статьи "Трения и смазка 2014 год"
        static public void TestGeneralModel(ref int[] masCalc, ref double maxd, ref double[] masSurf, ref int time, ref double[] masModel)
        {
            masCalc = new int[5];
            masCalc[0] = 2;
            masCalc[1] = 2;
            masCalc[2] = 130;
            masCalc[3] = 280;
            masCalc[4] = 600;
            maxd = 4.2;
            masSurf = new double[28];
            masSurf[0] = CalcFunctions.mum(2.0);
            masSurf[1] = CalcFunctions.mum(3.0);
            masSurf[2] = CalcFunctions.mum(0.4);
            masSurf[3] = CalcFunctions.mum(0.4);
            masSurf[4] = 0.0;
            masSurf[5] = 0.0;
            masSurf[6] = CalcFunctions.GPa(0.0);
            masSurf[7] = CalcFunctions.GPa(0.0);
            masSurf[8] = 0.0;
            masSurf[9] = 0.0;
            masSurf[10] = CalcFunctions.mum(0.0);
            masSurf[11] = CalcFunctions.mum(0.0);
            masSurf[12] = CalcFunctions.mum(50.0);
            masSurf[13] = CalcFunctions.mum(50.0);
            masSurf[14] = 0.57;
            masSurf[15] = 0.57;
            masSurf[16] = 0.1;
            masSurf[17] = 0.1;
            masSurf[18] = 0.3;
            masSurf[19] = 0.5;
            masSurf[20] = 1.0;
            masSurf[21] = 1.0;
            masSurf[22] = 10.0;
            masSurf[23] = 10.0;
            masSurf[24] = 139.0;
            masSurf[25] = 139.0;
            masSurf[26] = CalcFunctions.MPa(0.0);
            masSurf[27] = 0.0;
            time = 8000;
            masModel = new double[3];
            masModel[0] = 1.0 * Math.Pow(10.0, -3.0);
            masModel[1] = 1.0 * Math.Pow(10.0, -3.0);
            masModel[2] = 3.0;
        }
    }
}
