using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace East_CSharp
{
    public partial class Form1 : Form
    {
        PRB prb;
        DateTime DT;
        int flagOfDeag = 0;

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
            string pattern = "^-?\\d*(\\.\\d+)?$";
            Regex regex = new Regex(pattern);

           
            if (radioButton4.Checked && regex.IsMatch(textBox_lon.Text) && regex.IsMatch(textBox_lat.Text) && textBox_lon.Text != "" && textBox_lat.Text != "")
            {               
                StartThread();
            }
            else if (radioButton5.Checked && textBox_net.Text != "")
            {
                StartThread();
            }
            else
            {
                MessageBox.Show("Не корректно заданы координаты точки либо либо файл с сеткой");
            }
            
        }


        private void StartThread()
        {
            int typeOfGrunt=1;

            if (radioButton1.Checked)
                typeOfGrunt = 0;
            if (radioButton2.Checked)
                typeOfGrunt = 1;
            if (radioButton3.Checked)
                typeOfGrunt = 2;

            prb = new PRB(textBox_mdbPath.Text);

            DT = DateTime.Now;

            if (radioButton4.Checked == true)
            {
                prb.Lat = Convert.ToDouble(textBox_lat.Text);
                prb.Lon = Convert.ToDouble(textBox_lon.Text);
                prb.NetIsFile = false;
            }
            if (radioButton5.Checked == true)
            {
                prb.NetIsFile = true;
                prb.NetFilePath = openFileDialog1.FileName;
            }



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
            prb.ncycl = Convert.ToInt32(textBox15.Text);

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

            if (flagOfDeag == 0)
            {
                label6.Text = "Расчет сейсмического эффекта";
                //если выбрана деагрегация
                if (checkBoxDeagreg.Checked)
                    prb.DEAG = 2;
                else
                    prb.DEAG = 0;
            }
            else
            {
                label6.Text = "Деагрегация";
                prb.DEAG = 1;
            }

            
            

            checkBox1.Enabled = false;
            checkBoxSaveKatalog.Enabled = false;
            checkBoxDeagreg.Enabled = false;
            buttonRun.Enabled = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            radioButton3.Enabled = false;

            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;


            textBox15.Enabled = false;
            radioButton4.Enabled = false;
            radioButton5.Enabled = false;
            textBox_lat.Enabled = false;
            textBox_lon.Enabled = false;
            textBox_net.Enabled = false;
            button_net.Enabled = false;
            textBox_mdbPath.Enabled = false;
            buttonOpen.Enabled = false;


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

            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            textBox6.Enabled = true;
            textBox7.Enabled = true;

            textBox15.Enabled = true;
            radioButton4.Enabled = true;
            radioButton5.Enabled = true;
            textBox_lat.Enabled = true;
            textBox_lon.Enabled = true;
            textBox_net.Enabled = true;
            button_net.Enabled = true;
            textBox_mdbPath.Enabled = true;
            buttonOpen.Enabled = true;

            label6.Text = "";
            if (prb.DEAG == 2)
            {
                prb = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                flagOfDeag = 1;
                StartThread();
            }
            else
            {

                
                TimeSpan TS = DateTime.Now - DT;

                MessageBox.Show("Расчет занял: " + TS.Hours.ToString() + ":" + TS.Minutes.ToString() + ":" + TS.Seconds.ToString() + " (ЧЧ:ММ:СС)\r\nОбращений к БД: " + prb.DBcount.ToString());
            }
           

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

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            button_net.Enabled = true;
            textBox_net.Enabled = true;
            textBox_lat.Enabled = false;
            textBox_lon.Enabled = false;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            button_net.Enabled = false;
            textBox_net.Enabled = false;
            textBox_lat.Enabled = true;
            textBox_lon.Enabled = true;
        }

        private void button_net_Click(object sender, EventArgs e)
        {
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox_net.Text = openFileDialog1.FileName;
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void textBox_lat_Leave(object sender, EventArgs e)
        {
 
        }

        private void textBox_lat_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_lon_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_lon_Leave(object sender, EventArgs e)
        {

        }

        private void checkBoxSaveDuration_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 AboutBox = new AboutBox1();
            AboutBox.Show();
        }
    }
}
