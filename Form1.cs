using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace East_CSharp
{
    public partial class Form1 : Form
    {
        PRB prb;
        DateTime DT;

        public Form1()
        {
            InitializeComponent();

            textBox8.Text = TtoP(Convert.ToInt32(textBox1.Text)).ToString("F3");
            textBox9.Text = TtoP(Convert.ToInt32(textBox2.Text)).ToString("F3");
            textBox10.Text = TtoP(Convert.ToInt32(textBox3.Text)).ToString("F3");
            textBox11.Text = TtoP(Convert.ToInt32(textBox4.Text)).ToString("F3");
            textBox12.Text = TtoP(Convert.ToInt32(textBox5.Text)).ToString("F3");
            textBox13.Text = TtoP(Convert.ToInt32(textBox6.Text)).ToString("F3");
            textBox14.Text = TtoP(Convert.ToInt32(textBox7.Text)).ToString("F3");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            buttonRun.Enabled = false;
        }

        private void buttonRun_Click(object sender,EventArgs e)
        {
            StartThread();
        }


        private void StartThread()
        {
            DT = DateTime.Now;

            prb = new PRB(textBox_mdbPath.Text);

            prb.m_ch_make_katalog = false;
            //prb.m_e_iter = Convert.ToInt32( textBox_iter.Text );
            prb.m_e_tmax = 5000;
            prb.m_main_base_ok = true;
            prb.flag_9999 = 0;
            prb.i_gst1 = 0;
            prb.i_gst2 = 0;
            prb.i_gst3 = 1;
            prb.i_rz = 0;
            prb.i_az = 1;
            prb.i_bz = 1;
            prb.i_cz = 1;

            prb.probabilityOfExceedance[0] = Convert.ToDouble(textBox8.Text);
            prb.probabilityOfExceedance[1] = Convert.ToDouble(textBox9.Text);
            prb.probabilityOfExceedance[2] = Convert.ToDouble(textBox10.Text);
            prb.probabilityOfExceedance[3] = Convert.ToDouble(textBox11.Text);
            prb.probabilityOfExceedance[4] = Convert.ToDouble(textBox12.Text);
            prb.probabilityOfExceedance[5] = Convert.ToDouble(textBox13.Text);
            prb.probabilityOfExceedance[6] = Convert.ToDouble(textBox14.Text);

            prb.periodsOfRepeating[0] = Convert.ToInt32(textBox1.Text);
            prb.periodsOfRepeating[1] = Convert.ToInt32(textBox2.Text);
            prb.periodsOfRepeating[2] = Convert.ToInt32(textBox3.Text);
            prb.periodsOfRepeating[3] = Convert.ToInt32(textBox4.Text);
            prb.periodsOfRepeating[4] = Convert.ToInt32(textBox5.Text);
            prb.periodsOfRepeating[5] = Convert.ToInt32(textBox6.Text);
            prb.periodsOfRepeating[6] = Convert.ToInt32(textBox7.Text);


            //если выбран быстрый счет
            if (checkBox1.Checked)
                prb.fastCalc = 1;
            else
                prb.fastCalc = 0;

            //если выбрано сохранение каталога
            if (checkBoxSaveKatalog.Checked)
                prb.LCAT = 1;
            else
                prb.LCAT = 0;

            //если выбрана деагрегация
            if (checkBoxDeagreg.Checked)
                prb.DEAG = 1;
            else
                prb.DEAG = 0;

            if (radioButton1.Checked)
                prb.typeOfGrunt = 0;
            if (radioButton2.Checked)
                prb.typeOfGrunt = 1;
            if (radioButton3.Checked)
                prb.typeOfGrunt = 2;

            checkBox1.Enabled = false;
            checkBoxSaveKatalog.Enabled = false;
            checkBoxDeagreg.Enabled = false;
            buttonRun.Enabled = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            radioButton3.Enabled = false;

            //запуск расчета

            // Start the asynchronous operation.
            backgroundWorker1.RunWorkerAsync(prb);


            
        }

        private void buttonOpen_Click(object sender,EventArgs e)
        {
            OpenFileDialog OD = new OpenFileDialog();
            if(OD.ShowDialog() == DialogResult.OK)
            {
                textBox_mdbPath.Text = OD.FileName;
            }

            
        }

        private void textBox_mdbPath_TextChanged(object sender,EventArgs e)
        {
            if(textBox_mdbPath.Text.Length > 4)
                buttonRun.Enabled = true;
            else
                buttonRun.Enabled = false;
        }

        private void Form1_FormClosing(object sender,FormClosingEventArgs e)
        {
            if(prb != null)
                prb.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBoxSaveKatalog.Checked = false;
                checkBoxSaveKatalog.Enabled = false;
            }
            else
            {
                checkBoxSaveKatalog.Checked = true;
                checkBoxSaveKatalog.Enabled = true;
            }


        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;

            label3.Text = (string)e.UserState;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            checkBox1.Enabled = true;
            checkBoxSaveKatalog.Enabled = true;
            checkBoxDeagreg.Enabled = true;
            buttonRun.Enabled = true;
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            radioButton3.Enabled = true;

            TimeSpan TS = DateTime.Now - DT;

            MessageBox.Show("Расчет занял: " + TS.Hours.ToString() + ":" + TS.Minutes.ToString() + ":" + TS.Seconds.ToString() + " (ЧЧ:ММ:СС)\r\nОбращений к БД: " + prb.DBcount.ToString());

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            System.ComponentModel.BackgroundWorker worker;
            worker = (System.ComponentModel.BackgroundWorker)sender;

            PRB prb = (PRB)e.Argument;
            prb.OpenData(worker, e);
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private double TtoP(int T)
        {
            return (1 - Math.Pow(Math.E, (-50.0 / Convert.ToDouble(T))))*100;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            textBox8.Text = TtoP(Convert.ToInt32(textBox1.Text)).ToString("F3");
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            textBox9.Text = TtoP(Convert.ToInt32(textBox2.Text)).ToString("F3");
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            textBox10.Text = TtoP(Convert.ToInt32(textBox3.Text)).ToString("F3");
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            textBox11.Text = TtoP(Convert.ToInt32(textBox4.Text)).ToString("F3");
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            textBox12.Text = TtoP(Convert.ToInt32(textBox5.Text)).ToString("F3");
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            textBox13.Text = TtoP(Convert.ToInt32(textBox6.Text)).ToString("F3");
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            textBox14.Text = TtoP(Convert.ToInt32(textBox7.Text)).ToString("F3");
        }
    }
}
