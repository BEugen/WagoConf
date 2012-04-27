namespace testActiveX
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.p6 = new System.Windows.Forms.TextBox();
            this.accept = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.p5 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.p4 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.p3 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.p2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.p1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.address = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmd = new System.Windows.Forms.TextBox();
            this.configPLC_S71 = new RtpWagoConf.ConfigPlcWago();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.p6);
            this.groupBox1.Controls.Add(this.accept);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.p5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.p4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.p3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.p2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.p1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.address);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmd);
            this.groupBox1.Location = new System.Drawing.Point(1039, 32);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(183, 610);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры команды";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(41, 546);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(104, 29);
            this.button5.TabIndex = 21;
            this.button5.Text = "Single";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(41, 511);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(104, 29);
            this.button4.TabIndex = 20;
            this.button4.Text = "Group";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(41, 436);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(104, 54);
            this.button3.TabIndex = 19;
            this.button3.Text = "9999";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(41, 377);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 45);
            this.button2.TabIndex = 18;
            this.button2.Text = "ShiberSelect";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(41, 291);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 43);
            this.button1.TabIndex = 17;
            this.button1.Text = "Лог";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 200);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "P6";
            // 
            // p6
            // 
            this.p6.Location = new System.Drawing.Point(86, 197);
            this.p6.Margin = new System.Windows.Forms.Padding(2);
            this.p6.Name = "p6";
            this.p6.Size = new System.Drawing.Size(76, 20);
            this.p6.TabIndex = 15;
            // 
            // accept
            // 
            this.accept.Location = new System.Drawing.Point(41, 222);
            this.accept.Margin = new System.Windows.Forms.Padding(2);
            this.accept.Name = "accept";
            this.accept.Size = new System.Drawing.Size(104, 29);
            this.accept.TabIndex = 14;
            this.accept.Text = "Выполненно";
            this.accept.UseVisualStyleBackColor = true;
            this.accept.Click += new System.EventHandler(this.accept_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 177);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "P5";
            // 
            // p5
            // 
            this.p5.Location = new System.Drawing.Point(86, 175);
            this.p5.Margin = new System.Windows.Forms.Padding(2);
            this.p5.Name = "p5";
            this.p5.Size = new System.Drawing.Size(76, 20);
            this.p5.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 154);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "P4";
            // 
            // p4
            // 
            this.p4.Location = new System.Drawing.Point(86, 152);
            this.p4.Margin = new System.Windows.Forms.Padding(2);
            this.p4.Name = "p4";
            this.p4.Size = new System.Drawing.Size(76, 20);
            this.p4.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 132);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "P3";
            // 
            // p3
            // 
            this.p3.Location = new System.Drawing.Point(86, 129);
            this.p3.Margin = new System.Windows.Forms.Padding(2);
            this.p3.Name = "p3";
            this.p3.Size = new System.Drawing.Size(76, 20);
            this.p3.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 109);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "P2";
            // 
            // p2
            // 
            this.p2.Location = new System.Drawing.Point(86, 106);
            this.p2.Margin = new System.Windows.Forms.Padding(2);
            this.p2.Name = "p2";
            this.p2.Size = new System.Drawing.Size(76, 20);
            this.p2.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 86);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "P1";
            // 
            // p1
            // 
            this.p1.Location = new System.Drawing.Point(86, 84);
            this.p1.Margin = new System.Windows.Forms.Padding(2);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(76, 20);
            this.p1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 63);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Адрес";
            // 
            // address
            // 
            this.address.Location = new System.Drawing.Point(86, 61);
            this.address.Margin = new System.Windows.Forms.Padding(2);
            this.address.Name = "address";
            this.address.Size = new System.Drawing.Size(76, 20);
            this.address.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 41);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Команда";
            // 
            // cmd
            // 
            this.cmd.Location = new System.Drawing.Point(86, 38);
            this.cmd.Margin = new System.Windows.Forms.Padding(2);
            this.cmd.Name = "cmd";
            this.cmd.Size = new System.Drawing.Size(76, 20);
            this.cmd.TabIndex = 0;
            // 
            // configPLC_S71
            // 
            this.configPLC_S71.Accept = 0;
            this.configPLC_S71.Command = 0;
            this.configPLC_S71.CurrentAccessLevel = 999;
            this.configPLC_S71.GroupSetup = 0;
            this.configPLC_S71.Location = new System.Drawing.Point(9, 9);
            this.configPLC_S71.Margin = new System.Windows.Forms.Padding(2);
            this.configPLC_S71.MinAccessLevelToConfigPlc = 9999;
            this.configPLC_S71.Name = "configPLC_S71";
            this.configPLC_S71.P1 = 0;
            this.configPLC_S71.P2 = 0;
            this.configPLC_S71.P3 = 0;
            this.configPLC_S71.P4 = 0;
            this.configPLC_S71.P5 = 0;
            this.configPLC_S71.P6 = 0;
            this.configPLC_S71.ShiberSelect = -1;
            this.configPLC_S71.SingleSetup = 1;
            this.configPLC_S71.Size = new System.Drawing.Size(1024, 633);
            this.configPLC_S71.TabIndex = 0;
            this.configPLC_S71.Load += new System.EventHandler(this.configPLC_S71_Load);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1233, 691);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.configPLC_S71);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private RtpWagoConf.ConfigPlcWago configPLC_S71;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button accept;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox p5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox p4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox p3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox p2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox p1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox address;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox cmd;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox p6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}

