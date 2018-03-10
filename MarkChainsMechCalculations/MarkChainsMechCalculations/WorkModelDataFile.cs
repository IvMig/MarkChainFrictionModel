using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkChainsMechCalculations
{
    // класс для сохранения/загрузки ModelData в файлы
    public static class WorkModelDataFile
    {
        // Сохранить простые параметры ModelData
        public static void SaveModelDataSimpleData(BinaryWriter f_out, ModelData Model)
        {
            if (f_out == null) return;
            f_out.Write(Model.mode);
            f_out.Write(Model.ShiftMode);
            f_out.Write(Model.nt);
            f_out.Write(Model.kxi); f_out.Write(Model.keta);
            f_out.Write(Model.nxi); f_out.Write(Model.neta);
            f_out.Write(Model.Pwxi); f_out.Write(Model.Pweta);
            f_out.Write(Model.deltaL);
            f_out.Write(Model.netaPwxi); f_out.Write(Model.nxiPweta);
            f_out.Write(Model.F0);
        }
        // Загрузить простые параметры ModelData
        public static void LoadModelDataSimpleData(BinaryReader f_in, ref ModelData Model)
        {
            if (f_in == null) return;
            Model.mode = f_in.ReadInt32();
            Model.ShiftMode = f_in.ReadInt32();
            Model.nt = f_in.ReadInt32();
            Model.kxi = f_in.ReadInt32(); Model.keta = f_in.ReadInt32();
            Model.nxi = f_in.ReadInt32(); Model.neta=f_in.ReadInt32();
            Model.Pwxi = f_in.ReadDouble(); Model.Pweta = f_in.ReadDouble();
            Model.deltaL = f_in.ReadDouble();
            Model.netaPwxi = f_in.ReadDouble(); Model.nxiPweta = f_in.ReadDouble();
            Model.F0 = f_in.ReadDouble();
        }
        // Сохранить CalcData
        public static void SaveCalcData(BinaryWriter f_out, CalcData CData)
        {
            if (f_out == null) return;
            f_out.Write(CData.Scalexi); f_out.Write(CData.Scaleeta);
            f_out.Write(CData.dxi); f_out.Write(CData.deta);
            f_out.Write(CData.N);
            f_out.Write(CData.maxd);
            f_out.Write(CData.h);
        }
        // Загрузить CalcData
        public static void LoadCalcData(BinaryReader f_in, ref CalcData CData)
        {
            if (f_in == null) return;
            CData.Scalexi = f_in.ReadInt32(); CData.Scaleeta = f_in.ReadInt32();
            CData.dxi = f_in.ReadInt32(); CData.deta = f_in.ReadInt32();
            CData.N = f_in.ReadInt32();
            CData.maxd = f_in.ReadDouble();
            CData.h = f_in.ReadDouble();
        }
        // Сохранить SurfaceData
        public static void SaveSurfaceData(BinaryWriter f_out, SurfaceData SData)
        {
            if (f_out == null) return;
            f_out.Write(SData.mxi); f_out.Write(SData.meta);
            f_out.Write(SData.sigmaxi); f_out.Write(SData.sigmaeta);
            f_out.Write(SData.nuxi); f_out.Write(SData.nueta);
            f_out.Write(SData.Exi); f_out.Write(SData.Eeta);
            f_out.Write(SData.thetaxi); f_out.Write(SData.thetaeta);
            f_out.Write(SData.theta);
            f_out.Write(SData.alphaxi); f_out.Write(SData.alphaeta);
            f_out.Write(SData.Rmxi); f_out.Write(SData.Rmeta);
            f_out.Write(SData.Rm);
            f_out.Write(SData.deltaSxi); f_out.Write(SData.deltaSeta);
            f_out.Write(SData.wxi); f_out.Write(SData.weta);
            f_out.Write(SData.Rwxi); f_out.Write(SData.Rweta);
            f_out.Write(SData.DeltaDef);
            f_out.Write(SData.Pm);
            f_out.Write(SData.Scalewxi); f_out.Write(SData.Scaleweta);
            f_out.Write(SData.dwxi); f_out.Write(SData.dweta);
            f_out.Write(SData.Nwxi); f_out.Write(SData.Nweta);
            f_out.Write(SData.taumol); f_out.Write(SData.betamol);
            f_out.Write(SData.axi); f_out.Write(SData.bxi);
            f_out.Write(SData.aeta); f_out.Write(SData.beta);
        }
        // Загрузить SurfaceData
        public static void LoadSurfaceData(BinaryReader f_in, ref SurfaceData SData)
        {
            if (f_in == null) return;
            SData.mxi = f_in.ReadDouble(); SData.meta = f_in.ReadDouble();
            SData.sigmaxi = f_in.ReadDouble(); SData.sigmaeta = f_in.ReadDouble();
            SData.nuxi = f_in.ReadDouble(); SData.nueta = f_in.ReadDouble();
            SData.Exi = f_in.ReadDouble(); SData.Eeta = f_in.ReadDouble();
            SData.thetaxi = f_in.ReadDouble(); SData.thetaeta = f_in.ReadDouble();
            SData.theta = f_in.ReadDouble();
            SData.alphaxi = f_in.ReadDouble(); SData.alphaeta = f_in.ReadDouble();
            SData.Rmxi = f_in.ReadDouble(); SData.Rmeta = f_in.ReadDouble();
            SData.Rm = f_in.ReadDouble();
            SData.deltaSxi = f_in.ReadDouble(); SData.deltaSeta = f_in.ReadDouble();
            SData.wxi = f_in.ReadDouble(); SData.weta = f_in.ReadDouble();
            SData.Rwxi = f_in.ReadDouble(); SData.Rweta = f_in.ReadDouble();
            SData.DeltaDef = f_in.ReadDouble();
            SData.Pm = f_in.ReadDouble();
            SData.Scalewxi = f_in.ReadDouble(); SData.Scaleweta = f_in.ReadDouble();
            SData.dwxi = f_in.ReadInt32(); SData.dweta = f_in.ReadInt32(); ;
            SData.Nwxi = f_in.ReadInt32(); ; SData.Nweta = f_in.ReadInt32();
            SData.taumol = f_in.ReadDouble(); SData.betamol = f_in.ReadDouble();
            SData.axi = f_in.ReadDouble(); SData.bxi = f_in.ReadDouble();
            SData.aeta = f_in.ReadDouble(); SData.beta = f_in.ReadDouble();

        }
        // Сохранить double[]
        public static void SaveArray(BinaryWriter f_out, double[] mas)
        {
            if (f_out == null) return;
            int N = mas.Length;
            for (int i = 0; i < N; i++)
                f_out.Write(mas[i]);
        }
        // Загрузить double[]
        public static void LoadArray(BinaryReader f_in, ref double[] mas, int N)
        {
            if (f_in == null) return;
            mas = new double[N];
            for (int i = 0; i < N; i++)
                mas[i] = f_in.ReadDouble();
        }
        // Сохранить int[]
        public static void SaveArray(BinaryWriter f_out, int[] mas)
        {
            if (f_out == null) return;
            int N = mas.Length;
            for (int i = 0; i < N; i++)
                f_out.Write(mas[i]);
        }
        // Загрузить int[]
        public static void LoadArray(BinaryReader f_in, ref int[] mas, int N)
        {
            if (f_in == null) return;
            mas = new int[N];
            for (int i = 0; i < N; i++)
                mas[i] = f_in.ReadInt32();
        }
        // Сохранить DistributionData
        public static void SaveDistributionData(BinaryWriter f_out, DistributionData DData)
        {
            if (f_out == null) return;
            SaveArray(f_out, DData.p); SaveArray(f_out, DData.q);
            f_out.Write(DData.Mxi); f_out.Write(DData.Meta);
            f_out.Write(DData.Rpxi); f_out.Write(DData.Rqeta);
            SaveArray(f_out, DData.Pp);
            SaveArray(f_out, DData.pdelta);
            f_out.Write(DData.Sdelta);
            SaveArray(f_out, DData.Tp);
            SaveArray(f_out, DData.Fq);
        }
        // Загрузить DistributionData
        public static void LoadDistributionData(BinaryReader f_in, ref DistributionData DData, int N)
        {
            if (f_in == null) return;
            LoadArray(f_in, ref DData.p, N); LoadArray(f_in, ref DData.q, N);
            DData.Mxi = f_in.ReadDouble(); DData.Meta = f_in.ReadDouble();
            DData.Rpxi = f_in.ReadDouble(); DData.Rqeta = f_in.ReadDouble();
            LoadArray(f_in, ref DData.Pp, 3);
            LoadArray(f_in, ref DData.pdelta, DData.Pp[2]);
            DData.Sdelta = f_in.ReadDouble();
            LoadArray(f_in, ref DData.Tp, N);
            LoadArray(f_in, ref DData.Fq, N);
        }
        // Сохранить DistributionData[]
        public static void SaveArray(BinaryWriter f_out, DistributionData[] DData)
        {
            if (f_out == null) return;
            int N = DData.Length;
            for (int i = 0; i < N; i++)
                SaveDistributionData(f_out, DData[i]);
        }
        // Загрузить DistributionData[]
        public static void LoadArray(BinaryReader f_in, ref ModelData Model)
        {
            if (f_in == null) return;
            Model.DData = new DistributionData[Model.nt + 1];
            for (int i = 0; i < Model.nt + 1; i++)
            {
                Model.DData[i] = new DistributionData();
                LoadDistributionData(f_in, ref Model.DData[i], Model.CData.N);
            }
        }
        // Сохранить FritionData
        public static void SaveFrictionData(BinaryWriter f_out, FrictionData FData)
        {
            if (f_out == null) return;
            f_out.Write(FData.ar);
            f_out.Write(FData.F);
            f_out.Write(FData.ffr);
            f_out.Write(FData.kfr);
            f_out.Write(FData.Ixi); f_out.Write(FData.Ieta);
        }
        // Загрузить FrictionData
        public static void LoadFrictionData(BinaryReader f_in, ref FrictionData FData)
        {
            if (f_in == null) return;
            FData.ar = f_in.ReadDouble();
            FData.F = f_in.ReadDouble();
            FData.ffr = f_in.ReadDouble();
            FData.kfr = f_in.ReadDouble();
            FData.Ixi = f_in.ReadDouble(); FData.Ieta = f_in.ReadDouble();
        }
        // Сохранить FrictionData[]
        public static void SaveArray(BinaryWriter f_out, FrictionData[] FData)
        {
            if (f_out == null) return;
            int N = FData.Length;
            for (int i = 0; i < N; i++)
                SaveFrictionData(f_out, FData[i]);
        }
        // Загрузить FrictionData[]
        public static void LoadArray(BinaryReader f_in, ref FrictionData[] FData, int N)
        {
            if (f_in == null) return;
            FData = new FrictionData[N];
            for (int i = 0; i < N; i++)
            {
                FData[i] = new FrictionData();
                LoadFrictionData(f_in, ref FData[i]);
            }
        }
        // Сохранить ModelData
        public static void SaveModelData(string DataSaveDirectory, ModelData Model)
        {
            // Создание файла для простых параметров ModelData
            string FileName = DataSaveDirectory + "\\ModelData.bin";
            BinaryWriter f_out = new BinaryWriter(new FileStream(FileName, FileMode.Create));
            WorkModelDataFile.SaveModelDataSimpleData(f_out, Model);
            f_out.Close();
            // Создание файла для CalcData
            FileName = DataSaveDirectory + "\\CalcData.bin";
            f_out = new BinaryWriter(new FileStream(FileName, FileMode.Create));
            WorkModelDataFile.SaveCalcData(f_out, Model.CData);
            f_out.Close();
            // Создание файла для SData
            FileName = DataSaveDirectory + "\\SurfaceData.bin";
            f_out = new BinaryWriter(new FileStream(FileName, FileMode.Create));
            WorkModelDataFile.SaveSurfaceData(f_out, Model.SData);
            f_out.Close();
            // Создание файла для DData[]
            FileName = DataSaveDirectory + "\\DistributionData.bin";
            f_out = new BinaryWriter(new FileStream(FileName, FileMode.Create));
            WorkModelDataFile.SaveArray(f_out, Model.DData);
            f_out.Close();
            // Создание файла для FData[]
            FileName = DataSaveDirectory + "\\FrictionData.bin";
            f_out = new BinaryWriter(new FileStream(FileName, FileMode.Create));
            WorkModelDataFile.SaveArray(f_out, Model.FData);
            f_out.Close();
        }
        // Загрузить ModelData
        public static void LoadModelData(string DataLoadDirectory, ref ModelData Model)
        {
            Model = new ModelData();
            // Загрузить простые данные для ModelData
            string FileName = DataLoadDirectory + "\\ModelData.bin";
            BinaryReader f_in = new BinaryReader(new FileStream(FileName, FileMode.Open));
            LoadModelDataSimpleData(f_in, ref Model);
            f_in.Close();
            // Загрузить CalcData
            FileName = DataLoadDirectory + "\\CalcData.bin";
            f_in = new BinaryReader(new FileStream(FileName, FileMode.Open));
            Model.CData = new CalcData();
            LoadCalcData(f_in, ref Model.CData);
            f_in.Close();
            // Загрузить SData
            FileName = DataLoadDirectory + "\\SurfaceData.bin";
            f_in = new BinaryReader(new FileStream(FileName, FileMode.Open));
            Model.SData = new SurfaceData();
            LoadSurfaceData(f_in, ref Model.SData);
            f_in.Close();
            // Загрузить DData[]
            FileName = DataLoadDirectory + "\\DistributionData.bin";
            f_in = new BinaryReader(new FileStream(FileName, FileMode.Open));
            LoadArray(f_in, ref Model);
            f_in.Close();
            // Загрузить FData[]
            FileName = DataLoadDirectory + "\\FrictionData.bin";
            f_in = new BinaryReader(new FileStream(FileName, FileMode.Open));
            LoadArray(f_in, ref Model.FData, Model.nt + 1);
            f_in.Close();
        }
    }
}
