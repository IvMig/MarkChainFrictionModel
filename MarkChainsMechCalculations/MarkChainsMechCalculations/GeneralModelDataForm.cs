using System;
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
    public partial class GeneralModelDataForm : Form
    {
        // ссылка на главную форму для получения данных
        MainForm form1;
        public GeneralModelDataForm(MainForm f)
        {
            form1 = f;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            form1.flagInit = -1;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            form1.masCalc = new int[5];
            form1.masModel = new double[3];
            form1.masSurf = new double[28];
            form1.masCalc[0] = Convert.ToInt32(ScalexitextBox.Text);
            form1.masCalc[1] = Convert.ToInt32(ScaleetatextBox.Text);
            form1.masCalc[2] = Convert.ToInt32(ShiftxitextBox.Text);
            form1.masCalc[3] = Convert.ToInt32(ShiftetatextBox.Text);
            form1.masCalc[4] = Convert.ToInt32(SizetextBox.Text);
            form1.maxd = Convert.ToDouble(maxdtextBox.Text);
            form1.time = Convert.ToInt32(timetextBox.Text);
            form1.masModel[0] = Convert.ToDouble(PwxitextBox.Text);
            form1.masModel[1] = Convert.ToDouble(PwetatextBox.Text);
            form1.masModel[2] = -1;
            if (radioButtonxi.Checked) form1.masModel[2] = 1;
            if (radioButtoneta.Checked) form1.masModel[2] = 2;
            if (radioButtonxieta.Checked) form1.masModel[2] = 3;
            form1.masSurf[0] = CalcFunctions.mum(Convert.ToDouble(mxitextBox.Text));
            form1.masSurf[1] = CalcFunctions.mum(Convert.ToDouble(metatextBox.Text));
            form1.masSurf[2] = CalcFunctions.mum(Convert.ToDouble(sigmaxitextBox.Text));
            form1.masSurf[3] = CalcFunctions.mum(Convert.ToDouble(sigmaetatextBox.Text));
            form1.masSurf[12] = CalcFunctions.mum(Convert.ToDouble(deltastextBox.Text));
            form1.masSurf[13] = form1.masSurf[12];
            form1.masSurf[14] = Convert.ToDouble(wxitextBox.Text);
            form1.masSurf[15] = Convert.ToDouble(wetatextBox.Text);
            form1.masSurf[16] = Convert.ToDouble(RwxitextBox.Text);
            form1.masSurf[17] = Convert.ToDouble(RwetatextBox.Text);
            form1.masSurf[18] = Convert.ToDouble(DeltaDeftextBox.Text);
            form1.masSurf[19] = Convert.ToDouble(PmtextBox.Text);
            form1.masSurf[20] = Convert.ToDouble(ScalewxitextBox.Text);
            form1.masSurf[21] = Convert.ToDouble(ScalewetatextBox.Text);
            form1.masSurf[22] = Convert.ToDouble(ShiftwxitextBox.Text);
            form1.masSurf[23] = Convert.ToDouble(ShiftwetatextBox.Text);
            form1.masSurf[24] = Convert.ToDouble(NwxitextBox.Text);
            form1.masSurf[25] = Convert.ToDouble(NwetatextBox.Text);
            form1.flagInit = 1;
            Close();
        }
    }
}
