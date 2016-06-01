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
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Выбери MDB файл";
            // 
            // textBox_mdbPath
            // 
            this.textBox_mdbPath.Location = new System.Drawing.Point(15, 25);
            this.textBox_mdbPath.Name = "textBox_mdbPath";
            this.textBox_mdbPath.Size = new System.Drawing.Size(561, 20);
            this.textBox_mdbPath.TabIndex = 1;
            this.textBox_mdbPath.TextChanged += new System.EventHandler(this.textBox_mdbPath_TextChanged);
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(582, 22);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(35, 23);
            this.buttonOpen.TabIndex = 2;
            this.buttonOpen.Text = "...";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // buttonRun
            // 
            this.buttonRun.Location = new System.Drawing.Point(15, 51);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(133, 50);
            this.buttonRun.TabIndex = 3;
            this.buttonRun.Text = "ПУСК";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(212, 69);
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
            this.checkBoxSaveKatalog.Checked = true;
            this.checkBoxSaveKatalog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSaveKatalog.Location = new System.Drawing.Point(316, 69);
            this.checkBoxSaveKatalog.Name = "checkBoxSaveKatalog";
            this.checkBoxSaveKatalog.Size = new System.Drawing.Size(122, 17);
            this.checkBoxSaveKatalog.TabIndex = 5;
            this.checkBoxSaveKatalog.Text = "Сохранять каталог";
            this.checkBoxSaveKatalog.UseVisualStyleBackColor = true;
            // 
            // checkBoxDeagreg
            // 
            this.checkBoxDeagreg.AutoSize = true;
            this.checkBoxDeagreg.Location = new System.Drawing.Point(445, 69);
            this.checkBoxDeagreg.Name = "checkBoxDeagreg";
            this.checkBoxDeagreg.Size = new System.Drawing.Size(93, 17);
            this.checkBoxDeagreg.TabIndex = 6;
            this.checkBoxDeagreg.Text = "Деагрегация";
            this.checkBoxDeagreg.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 113);
            this.Controls.Add(this.checkBoxDeagreg);
            this.Controls.Add(this.checkBoxSaveKatalog);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.textBox_mdbPath);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "East 2016";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
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
    }
}

