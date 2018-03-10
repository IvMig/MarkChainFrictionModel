using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarkChainsMechCalculations
{
    public partial class MainForm : Form
    {
        // Модель, используемая в расчетах
        public ModelData Model;
        // Массив для инициализации расчетных данных
        public int[] masCalc; public double maxd;
        // Массив для инициализации данных о поверхностях
        public double[] masSurf;
        // Число шагов
        public int time;
        // Массив данных для инициализации данных
        public double[] masModel;
        // Режим отрисовки 0- не рисовать графики
        // 1 - рисовать графики начальных распределений, функций распределения и т.д. 
        // 2 - рисовать графики всех характеристик
        private int DrawMode;
        // -1 - Не инициализировать
        public int flagInit;
        // Индекс текущего шага
        private int TimeIndex;
        // Индекс отображаемого фрикционного параметра
        private int FricIndex;
        // Число фрикционных параметров
        private int FricCount = 3;
        // Строка-идентификатор для построения графиков фрикционных параметров
        private string[] FricID;
        // Коэффициент ускорения для быстрой перемотки распределений
        private int k = 1;
        // Имя директории, куда будут сохраняться все данные о расчетах
        string GeneralDirectory = "C:\\Users\\elextom\\OneDrive\\Documents\\Научная работа\\Бакалаврская работа\\Программа\\Расчетные данные\\";
        // "C:\\Users\\elextom\\OneDrive\\Documents\\Научная работа\\Бакалаврская работа\\Программа\\Расчетные данные\\";
        // "C:\\Users\\ivanm\\OneDrive\\Documents\\Научная работа\\Бакалаврская работа\\Программа\\Расчетные данные\\";
        public MainForm()
        {
            flagInit = -1;
            InitializeComponent();
            // Изменение Form1
            Text = "Главное окно";
            TopMost = false;
            Bounds = Screen.PrimaryScreen.WorkingArea;
            // Данные, необходимые для расстановки элементов
            int HClient;
            int WClient;
            int HMenuStrip;
            int HStatusStrip;
            // Место между объектами
            int FSpace = 10;
            HClient = ClientSize.Height;
            WClient = ClientSize.Width;
            HMenuStrip = MainMenuStrip.Height;
            HStatusStrip = statusStrip.Height;
            // Вычисление размеров PictureBox
            int WPictureBox = (WClient - 4 * FSpace) / 3;
            int HPictureBox = (HClient - HMenuStrip - HStatusStrip - 2 * FricParamlabel.Height - FSpace * 2 - 2 * FricParamPrevbutton.Height) / 2;
            // Изменение PictureBox'ов
            FricParampictureBox.BorderStyle = BorderStyle.Fixed3D;
            FricParampictureBox.Location = new Point(FSpace, HMenuStrip + FricParamlabel.Height + FSpace);
            FricParampictureBox.Size = new System.Drawing.Size(WPictureBox, HPictureBox);
            FricParampictureBox.BackColor = Color.White;
            Controls.Add(FricParampictureBox);
            DistrEvolutionpictureBox.BorderStyle = BorderStyle.Fixed3D;
            DistrEvolutionpictureBox.Location = new Point(FSpace * 2 + WPictureBox, HMenuStrip + FricParamlabel.Height + FSpace);
            DistrEvolutionpictureBox.Size = new System.Drawing.Size(WPictureBox, HPictureBox);
            DistrEvolutionpictureBox.BackColor = Color.White;
            Controls.Add(DistrEvolutionpictureBox);
            ContactCrossingpictureBox.BorderStyle = BorderStyle.Fixed3D;
            ContactCrossingpictureBox.Location = new Point(FSpace * 3 + WPictureBox * 2, HMenuStrip + FricParamlabel.Height + FSpace);
            ContactCrossingpictureBox.Size = new System.Drawing.Size(WPictureBox, HPictureBox);
            ContactCrossingpictureBox.BackColor = Color.White;
            Controls.Add(ContactCrossingpictureBox);
            FqTppictureBox.BorderStyle = BorderStyle.Fixed3D;
            FqTppictureBox.Location = new Point(FSpace * 2 + WPictureBox, HMenuStrip + FricParamlabel.Height + FSpace * 2 + HPictureBox + FricParamPrevbutton.Height);
            FqTppictureBox.Size = new System.Drawing.Size(WPictureBox, HPictureBox);
            FqTppictureBox.BackColor = Color.White;
            Controls.Add(FqTppictureBox);
            // Изменение label'ов
            FricParamlabel.Location = new Point(FSpace, HMenuStrip + FSpace);
            FricParamlabel.Text = "Фрикционные характеристики";
            DistrEvolutionlabel.Location = new Point(FSpace * 2 + WPictureBox, HMenuStrip + FSpace);
            DistrEvolutionlabel.Text = "Эволюция распределений";
            ContactCrossinglabel.Location = new Point(FSpace * 3 + WPictureBox * 2, HMenuStrip + FSpace);
            ContactCrossinglabel.Text = "Контактное пересечение";
            FqTplabel.Location = new Point(FSpace * 2 + WPictureBox, HMenuStrip + FSpace * 2 + HPictureBox + FricParamPrevbutton.Height);
            FqTplabel.Text = "Функция и хвост распределений";
            SpeedScrolllabel.Location = new Point(FSpace * 3 + WPictureBox * 2, HMenuStrip + FSpace * 2 + HPictureBox + FricParamPrevbutton.Height);
            SpeedScrolllabel.Text = "Скорость прокрутки";
            // Изменение button'ов
            FricParamPrevbutton.Location = new Point(FSpace, HMenuStrip + FSpace + HPictureBox + FricParamlabel.Height);
            FricParamNextbutton.Location = new Point(FSpace * 3 + FricParamPrevbutton.Width + FricParamtextBox.Width, HMenuStrip + FSpace + HPictureBox + FricParamlabel.Height);
            FricLenghPrevbutton.Location = new Point(FSpace * 2 + WPictureBox, HMenuStrip + FSpace * 3 + HPictureBox * 2 + FricParamlabel.Height * 2);
            FricLenghNextbutton.Location = new Point(FSpace * 4 + WPictureBox + FricParamtextBox.Width + FricParamPrevbutton.Width, HMenuStrip + FSpace * 3 + HPictureBox * 2 + FricParamlabel.Height * 2);
            SpeedScroll1button.Location = new Point(FSpace * 3 + WPictureBox * 2, HMenuStrip + FSpace * 3 + HPictureBox + FricParamPrevbutton.Height + SpeedScrolllabel.Height);
            SpeedScroll10button.Location = new Point(FSpace * 4 + WPictureBox * 2 + SpeedScroll1button.Width, HMenuStrip + FSpace * 3 + HPictureBox + FricParamPrevbutton.Height + SpeedScrolllabel.Height);
            SpeedScroll50button.Location = new Point(FSpace * 5 + WPictureBox * 2 + SpeedScroll1button.Width * 2, HMenuStrip + FSpace * 3 + HPictureBox + FricParamPrevbutton.Height + SpeedScrolllabel.Height);
            SpeedScroll100button.Location = new Point(FSpace * 3 + WPictureBox * 2, HMenuStrip + FSpace * 3 + HPictureBox + FricParamPrevbutton.Height + SpeedScrolllabel.Height + SpeedScroll1button.Height);
            SpeedScroll500button.Location = new Point(FSpace * 4 + WPictureBox * 2 + SpeedScroll1button.Width, HMenuStrip + FSpace * 3 + HPictureBox + FricParamPrevbutton.Height + SpeedScrolllabel.Height + SpeedScroll1button.Height);
            SpeedScroll1000button.Location = new Point(FSpace * 5 + WPictureBox * 2 + SpeedScroll1button.Width * 2, HMenuStrip + FSpace * 3 + HPictureBox + FricParamPrevbutton.Height + SpeedScrolllabel.Height + SpeedScroll1button.Height);
            // Изменение textBox'ов
            FricParamtextBox.Location = new Point(FSpace * 2 + FricParamPrevbutton.Width, HMenuStrip + FSpace + HPictureBox + FricParamlabel.Height);
            FricLenghtextBox.Location = new Point(FSpace * 3 + FricParamPrevbutton.Width + WPictureBox, HMenuStrip + FSpace * 3 + HPictureBox * 2 + FricParamlabel.Height * 2);
            toolStripStatusLabel.Text = "Программа запущена";
            DrawMode = 0;

            FricID=new string[FricCount];
            FricID[0] = "Шероховатость";
            FricID[1] = "Линейный износ";
            FricID[2] = "Коэффициент трения";
        }
        // Файл
        private void задатьОбщуюДиректориюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                GeneralDirectory = folderBrowserDialog.SelectedPath + "\\";
                MessageBox.Show("Общая директория задана", "Уведомление");
            }
        }
        private void сохранитьВсеДанныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            string DataSaveDirectory = GeneralDirectory + dt.ToString("d_MM_yyyy__HH_mm_ss");
            Directory.CreateDirectory(DataSaveDirectory);
            WorkModelDataFile.SaveModelData(DataSaveDirectory, Model);
            toolStripStatusLabel.Text = "Данные сохранены";
        }
        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string DataLoadDirectory = folderBrowserDialog.SelectedPath + "\\";
                WorkModelDataFile.LoadModelData(DataLoadDirectory, ref Model);
                toolStripStatusLabel.Text = "Данные загружены";
                TimeIndex = 0;
                FricIndex = 0;
                if (Model.mode == 1) FricCount = 1;
                if (Model.mode == 0) FricCount = 3;
                FricParamtextBox.Text = FricID[FricIndex];
                int L = Convert.ToInt32(Model.deltaL) * TimeIndex;
                FricLenghtextBox.Text = "Путь трения: " + L.ToString() + " м";
                DrawMode = 2;
                time = Model.nt;
                Reflesh();
            }
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        // Тесты
        private void поршеньцилиндрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MakeTests.TestPistonCylinderLiner(ref masCalc, ref maxd, ref masSurf, ref time, ref masModel);
            Model = new ModelData(masCalc, maxd, masSurf, time, masModel);
            Model.mode = 0;
            FricCount = 3;
            toolStripStatusLabel.Text = "Тестовый пример сопряжения поршень-цилиндр установлен";
            DrawMode = 1;
            Reflesh();
        }
        private void примерИзМоделированияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MakeTests.TestModelingDissertation(ref masCalc, ref maxd, ref masSurf, ref time, ref masModel);
            Model = new ModelData(masCalc, maxd, masSurf, time, masModel);
            Model.mode = 0;
            FricCount = 3;
            toolStripStatusLabel.Text = "Тестовый пример из диссертации Тигетова с моделированием установлен";
            DrawMode = 1;
            Reflesh();
        }
        private void примерИзЖурналаТрениеИСмазкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MakeTests.TestGeneralModel(ref masCalc, ref maxd, ref masSurf, ref time, ref masModel);
            Model = new ModelData(masCalc, maxd, masSurf, time, masModel);
            Model.mode = 1;
            FricCount = 1;
            Model.SData.axi = 2.7; Model.SData.bxi = 2.0;
            Model.SData.aeta = 2.0; Model.SData.beta = 2.7;
            Model.deltaL = Model.SData.deltaSxi;
            Model.DData[0] = new DistributionData(2.7, 2.0, 2.0, 2.7, 2, 2, 130, 280, 600, Model.CData.h);
            Model.F0 = FrictionFunctions.F(Model.DData[0]);
            toolStripStatusLabel.Text = "Тестовый пример из журнала \"Трение и смазка 2014 год\" установлен";
            DrawMode = 1;
            Reflesh();
        }
        // Работа
        private void схемаОбщегоВидаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GeneralModelDataForm form2 = new GeneralModelDataForm(this);
            form2.ShowDialog(this);
            if (flagInit == -1) return;
            Model = new ModelData(masCalc, maxd, masSurf, time, masModel);
            Model.mode = 1;
            FricCount = 1;
            DrawMode = 1;
            Reflesh();
        }
        private void схемаУсталостногоРазрушенияЗаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DestroyAnyDataForm form3 = new DestroyAnyDataForm(this);
            form3.ShowDialog(this);
            if (flagInit == -1) return;
            Model = new ModelData(masCalc, maxd, masSurf, time, masModel);
            Model.mode = 0;
            FricCount = 3;
            DrawMode = 1;
            Reflesh();
        }
        private void начатьРасчетыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CalcSolution.FindSolution(ref Model);
            DrawMode = 2;
            TimeIndex = 0;
            FricIndex = 0;
            // Отрисовки
            Reflesh();
            FricParamtextBox.Text = FricID[FricIndex];
            int L = Convert.ToInt32(Model.deltaL) * TimeIndex;
            FricLenghtextBox.Text = "Путь трения: " + L.ToString() + " м";
            toolStripStatusLabel.Text = "Расчеты выполнены";
        }
        // О программе
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа разработана студентом группы А-14-12 Мигалем Иваном. Предназначена для расчетов распределений высот шероховатых поверхностей с использованием марковской модели.", "О программе");
        }
        // Отображение графиков
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            ShowFunctions.DrawOXY(g,FricParampictureBox);
            if (DrawMode == 2) ShowFunctions.DrawFricFunc(g, FricParampictureBox, Model, FricID[FricIndex]);
        }
        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            ShowFunctions.DrawOXY(g, DistrEvolutionpictureBox);
            if (DrawMode == 1) ShowFunctions.DrawPlotDistr(g, DistrEvolutionpictureBox, Model, 0);
            if (DrawMode == 2) ShowFunctions.DrawPlotDistr(g, DistrEvolutionpictureBox, Model, TimeIndex);
        }
        private void pictureBox3_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            ShowFunctions.DrawOXY(g, ContactCrossingpictureBox);
            if (DrawMode == 1)
            {
                double MaxY = Model.DData[0].pdelta.Max();
                double MaxX = Model.DData[0].Pp[2] * Model.CData.h;
                ShowFunctions.DrawPlot(g, ContactCrossingpictureBox, Model.DData[0].pdelta, Color.Black, MaxY, 1);
                ShowFunctions.DrawScalesOXY(g, ContactCrossingpictureBox, MaxX, MaxY, 10, 10, "h, мкм", "");
            }
            if (DrawMode == 2)
            {
                double MaxY = Model.DData[TimeIndex].pdelta.Max();
                double MaxX = Model.DData[0].Pp[2] * Model.CData.h;
                ShowFunctions.DrawPlot(g, ContactCrossingpictureBox, Model.DData[TimeIndex].pdelta, Color.Black, Model.DData[TimeIndex].pdelta.Max(), 1);
                ShowFunctions.DrawScalesOXY(g, ContactCrossingpictureBox, MaxX, MaxY, 10, 10, "h, мкм", "");
            }
        }
        private void pictureBox4_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            ShowFunctions.DrawOXY(g, FqTppictureBox);
            if (DrawMode == 1) ShowFunctions.DrawPlotFuncTailDistr(g, FqTppictureBox, Model, 0);
            if (DrawMode == 2) ShowFunctions.DrawPlotFuncTailDistr(g, FqTppictureBox, Model, TimeIndex);
        }
        // Обновить изображения графиков, связанных с распределением
        private void RefleshDistr()
        {
            DistrEvolutionpictureBox.Refresh();
            ContactCrossingpictureBox.Refresh();
            FqTppictureBox.Refresh();
        }
        // Обновить изображения
        private void Reflesh()
        {
            FricParampictureBox.Refresh();
            RefleshDistr();
        }
        // Обработка событий нажатия на кнопки
        private void button1_Click(object sender, EventArgs e)
        {
            FricIndex--;
            if (FricIndex < 0) FricIndex = FricCount - 1;
            FricParampictureBox.Refresh();
            FricParamtextBox.Text = FricID[FricIndex];

        }
        private void button2_Click(object sender, EventArgs e)
        {
            FricIndex++;
            if (FricIndex > FricCount-1) FricIndex = 0;
            FricParampictureBox.Refresh();
            FricParamtextBox.Text = FricID[FricIndex];
        }
        private void button3_Click(object sender, EventArgs e)
        {
            TimeIndex -= k;
            if (TimeIndex < 0) TimeIndex = time;
            RefleshDistr();
            double L = CalcFunctions.GetL(Model, TimeIndex);
            FricLenghtextBox.Text = "Путь трения: " + L.ToString() + " м";
        }
        private void button4_Click(object sender, EventArgs e)
        {
            TimeIndex += k;
            if (TimeIndex > time) TimeIndex = 0;
            RefleshDistr();
            double L = CalcFunctions.GetL(Model,TimeIndex);
            FricLenghtextBox.Text = "Путь трения: " + L.ToString() + " м";
        }
        private void button5_Click(object sender, EventArgs e)
        {
            k = 1;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            k = 10;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            k = 50;
        }
        private void button8_Click(object sender, EventArgs e)
        {
            k = 100;
        }
        private void button9_Click(object sender, EventArgs e)
        {
            k = 500;
        }
        private void button10_Click(object sender, EventArgs e)
        {
            k = 1000;
        }
    }
}
