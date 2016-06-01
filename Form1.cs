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
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            buttonRun.Enabled = false;
        }

        private void buttonRun_Click(object sender,EventArgs e)
        {
            DateTime DT = DateTime.Now;

            prb = new PRB( textBox_mdbPath.Text );

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
            checkBox1.Enabled = false;
            prb.OpenData();
            checkBox1.Enabled = true;

            TimeSpan TS = DateTime.Now - DT;

            MessageBox.Show( "Расчет занял: " + TS.Hours.ToString() + ":" + TS.Minutes.ToString() + ":" + TS.Seconds.ToString() + " (ЧЧ:ММ:СС)\r\nОбращений к БД: " + prb.DBcount.ToString() );
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
    }
}
