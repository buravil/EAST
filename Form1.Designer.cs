namespace East_CSharp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if( disposing && (components != null) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_mdbPath = new System.Windows.Forms.TextBox();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.buttonRun = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBoxSaveKatalog = new System.Windows.Forms.CheckBox();
            this.checkBoxDeagreg = new System.Windows.Forms.CheckBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.textBox_net = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button_net = new System.Windows.Forms.Button();
            this.textBox_lat = new System.Windows.Forms.TextBox();
            this.textBox_lon = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Выбери базу данных, MDB";
            // 
            // textBox_mdbPath
            // 
            this.textBox_mdbPath.Location = new System.Drawing.Point(15, 50);
            this.textBox_mdbPath.Name = "textBox_mdbPath";
            this.textBox_mdbPath.Size = new System.Drawing.Size(561, 20);
            this.textBox_mdbPath.TabIndex = 1;
            this.textBox_mdbPath.TextChanged += new System.EventHandler(this.textBox_mdbPath_TextChanged);
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(582, 47);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(35, 23);
            this.buttonOpen.TabIndex = 2;
            this.buttonOpen.Text = "...";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // buttonRun
            // 
            this.buttonRun.Location = new System.Drawing.Point(484, 81);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(133, 50);
            this.buttonRun.TabIndex = 3;
            this.buttonRun.Text = "НАЧАТЬ РАСЧЕТ";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(690, 196);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(97, 17);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Быстрый счет";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBoxSaveKatalog
            // 
            this.checkBoxSaveKatalog.AutoSize = true;
            this.checkBoxSaveKatalog.Location = new System.Drawing.Point(15, 76);
            this.checkBoxSaveKatalog.Name = "checkBoxSaveKatalog";
            this.checkBoxSaveKatalog.Size = new System.Drawing.Size(122, 17);
            this.checkBoxSaveKatalog.TabIndex = 5;
            this.checkBoxSaveKatalog.Text = "Сохранять каталог";
            this.checkBoxSaveKatalog.UseVisualStyleBackColor = true;
            // 
            // checkBoxDeagreg
            // 
            this.checkBoxDeagreg.AutoSize = true;
            this.checkBoxDeagreg.Location = new System.Drawing.Point(15, 99);
            this.checkBoxDeagreg.Name = "checkBoxDeagreg";
            this.checkBoxDeagreg.Size = new System.Drawing.Size(93, 17);
            this.checkBoxDeagreg.TabIndex = 6;
            this.checkBoxDeagreg.Text = "Деагрегация";
            this.checkBoxDeagreg.UseVisualStyleBackColor = true;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 309);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(602, 19);
            this.progressBar1.TabIndex = 7;
            this.progressBar1.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 291);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 9;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(78, 120);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(28, 17);
            this.radioButton1.TabIndex = 10;
            this.radioButton1.Text = "I";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(122, 120);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(31, 17);
            this.radioButton2.TabIndex = 11;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "II";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(163, 120);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(34, 17);
            this.radioButton3.TabIndex = 12;
            this.radioButton3.Text = "III";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Тип грунта:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(211, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Период повторяемости сотрясений, лет";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(266, 140);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(43, 20);
            this.textBox1.TabIndex = 15;
            this.textBox1.Text = "100";
            this.textBox1.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(316, 140);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(43, 20);
            this.textBox2.TabIndex = 16;
            this.textBox2.Text = "500";
            this.textBox2.Leave += new System.EventHandler(this.textBox2_Leave);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(365, 140);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(43, 20);
            this.textBox3.TabIndex = 17;
            this.textBox3.Text = "1000";
            this.textBox3.Leave += new System.EventHandler(this.textBox3_Leave);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(414, 140);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(43, 20);
            this.textBox4.TabIndex = 18;
            this.textBox4.Text = "2000";
            this.textBox4.Leave += new System.EventHandler(this.textBox4_Leave);
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(463, 140);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(43, 20);
            this.textBox5.TabIndex = 19;
            this.textBox5.Text = "3000";
            this.textBox5.Leave += new System.EventHandler(this.textBox5_Leave);
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(512, 140);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(43, 20);
            this.textBox6.TabIndex = 20;
            this.textBox6.Text = "5000";
            this.textBox6.Leave += new System.EventHandler(this.textBox6_Leave);
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(561, 140);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(43, 20);
            this.textBox7.TabIndex = 21;
            this.textBox7.Text = "10000";
            this.textBox7.TextChanged += new System.EventHandler(this.textBox7_TextChanged);
            this.textBox7.Leave += new System.EventHandler(this.textBox7_Leave);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(12, 171);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(248, 31);
            this.label5.TabIndex = 22;
            this.label5.Text = "Вероятность превышения в течение 50 лет, %";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // textBox8
            // 
            this.textBox8.Enabled = false;
            this.textBox8.Location = new System.Drawing.Point(266, 167);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(43, 20);
            this.textBox8.TabIndex = 29;
            // 
            // textBox9
            // 
            this.textBox9.Enabled = false;
            this.textBox9.Location = new System.Drawing.Point(316, 167);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(43, 20);
            this.textBox9.TabIndex = 28;
            // 
            // textBox10
            // 
            this.textBox10.Enabled = false;
            this.textBox10.Location = new System.Drawing.Point(365, 167);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(43, 20);
            this.textBox10.TabIndex = 27;
            // 
            // textBox11
            // 
            this.textBox11.Enabled = false;
            this.textBox11.Location = new System.Drawing.Point(414, 167);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(43, 20);
            this.textBox11.TabIndex = 26;
            // 
            // textBox12
            // 
            this.textBox12.Enabled = false;
            this.textBox12.Location = new System.Drawing.Point(463, 167);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(43, 20);
            this.textBox12.TabIndex = 25;
            // 
            // textBox13
            // 
            this.textBox13.Enabled = false;
            this.textBox13.Location = new System.Drawing.Point(512, 167);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(43, 20);
            this.textBox13.TabIndex = 24;
            // 
            // textBox14
            // 
            this.textBox14.Enabled = false;
            this.textBox14.Location = new System.Drawing.Point(561, 167);
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(43, 20);
            this.textBox14.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(445, 293);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 13);
            this.label6.TabIndex = 30;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Checked = true;
            this.radioButton4.Location = new System.Drawing.Point(14, 32);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(110, 17);
            this.radioButton4.TabIndex = 32;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "Для одной точки";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(14, 63);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(100, 17);
            this.radioButton5.TabIndex = 33;
            this.radioButton5.Text = "Выбрать сетку";
            this.radioButton5.UseVisualStyleBackColor = true;
            this.radioButton5.CheckedChanged += new System.EventHandler(this.radioButton5_CheckedChanged);
            // 
            // textBox_net
            // 
            this.textBox_net.Enabled = false;
            this.textBox_net.Location = new System.Drawing.Point(198, 60);
            this.textBox_net.Name = "textBox_net";
            this.textBox_net.Size = new System.Drawing.Size(370, 20);
            this.textBox_net.TabIndex = 34;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(127, 13);
            this.label7.TabIndex = 35;
            this.label7.Text = "Выбор расчетной сетки";
            // 
            // button_net
            // 
            this.button_net.Enabled = false;
            this.button_net.Location = new System.Drawing.Point(120, 60);
            this.button_net.Name = "button_net";
            this.button_net.Size = new System.Drawing.Size(67, 23);
            this.button_net.TabIndex = 36;
            this.button_net.Text = "Выбрать";
            this.button_net.UseVisualStyleBackColor = true;
            this.button_net.Click += new System.EventHandler(this.button_net_Click);
            // 
            // textBox_lat
            // 
            this.textBox_lat.Location = new System.Drawing.Point(196, 31);
            this.textBox_lat.Name = "textBox_lat";
            this.textBox_lat.Size = new System.Drawing.Size(95, 20);
            this.textBox_lat.TabIndex = 37;
            this.textBox_lat.TextChanged += new System.EventHandler(this.textBox_lat_TextChanged);
            this.textBox_lat.Leave += new System.EventHandler(this.textBox_lat_Leave);
            // 
            // textBox_lon
            // 
            this.textBox_lon.Location = new System.Drawing.Point(346, 31);
            this.textBox_lon.Name = "textBox_lon";
            this.textBox_lon.Size = new System.Drawing.Size(92, 20);
            this.textBox_lon.TabIndex = 38;
            this.textBox_lon.TextChanged += new System.EventHandler(this.textBox_lon_TextChanged);
            this.textBox_lon.Leave += new System.EventHandler(this.textBox_lon_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(159, 34);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(22, 13);
            this.label8.TabIndex = 39;
            this.label8.Text = "Lat";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(305, 34);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(25, 13);
            this.label9.TabIndex = 40;
            this.label9.Text = "Lon";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Файлы сетки (*.geg)|*.geg";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.radioButton4);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.radioButton5);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.textBox_net);
            this.panel1.Controls.Add(this.textBox_lon);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.textBox_lat);
            this.panel1.Controls.Add(this.button_net);
            this.panel1.Location = new System.Drawing.Point(15, 194);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(589, 92);
            this.panel1.TabIndex = 41;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(161, 77);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(216, 13);
            this.label10.TabIndex = 42;
            this.label10.Text = "Длительность модельного каталога, лет";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(161, 103);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(215, 13);
            this.label11.TabIndex = 43;
            this.label11.Text = "Число реализаций модельного каталога";
            // 
            // textBox15
            // 
            this.textBox15.Location = new System.Drawing.Point(387, 100);
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new System.Drawing.Size(70, 20);
            this.textBox15.TabIndex = 44;
            this.textBox15.Text = "200";
            // 
            // textBox16
            // 
            this.textBox16.Enabled = false;
            this.textBox16.Location = new System.Drawing.Point(388, 74);
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new System.Drawing.Size(69, 20);
            this.textBox16.TabIndex = 45;
            this.textBox16.Text = "5000";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(629, 24);
            this.menuStrip1.TabIndex = 46;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.оПрограммеToolStripMenuItem});
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 334);
            this.Controls.Add(this.textBox16);
            this.Controls.Add(this.textBox15);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.textBox9);
            this.Controls.Add(this.textBox10);
            this.Controls.Add(this.textBox11);
            this.Controls.Add(this.textBox12);
            this.Controls.Add(this.textBox13);
            this.Controls.Add(this.textBox14);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.checkBoxDeagreg);
            this.Controls.Add(this.checkBoxSaveKatalog);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.textBox_mdbPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "East v3.1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_mdbPath;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBoxSaveKatalog;
        private System.Windows.Forms.CheckBox checkBoxDeagreg;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.TextBox textBox_net;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button_net;
        private System.Windows.Forms.TextBox textBox_lat;
        private System.Windows.Forms.TextBox textBox_lon;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox15;
        private System.Windows.Forms.TextBox textBox16;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
    }
}

