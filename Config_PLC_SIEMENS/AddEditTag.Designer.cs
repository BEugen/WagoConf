namespace Config_PLC_SIEMENS
{
    partial class AddEditTag
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
            this.added_b_cancel = new System.Windows.Forms.Button();
            this.added_b_ok = new System.Windows.Forms.Button();
            this.added_l_timeFiltr = new System.Windows.Forms.Label();
            this.added_inp_timeFiltr = new Config_PLC_SIEMENS.CustomControl.DigitTextBox();
            this.added_enInv = new System.Windows.Forms.CheckBox();
            this.added_ch_enFiltr = new System.Windows.Forms.CheckBox();
            this.added_l_maxEU = new System.Windows.Forms.Label();
            this.added_inp_maxEU = new Config_PLC_SIEMENS.CustomControl.DigitTextBox();
            this.added_l_minEU = new System.Windows.Forms.Label();
            this.added_inp_minEU = new Config_PLC_SIEMENS.CustomControl.DigitTextBox();
            this.added_l_maxRAW = new System.Windows.Forms.Label();
            this.added_inp_rawMAX = new Config_PLC_SIEMENS.CustomControl.DigitTextBox();
            this.added_l_minRAW = new System.Windows.Forms.Label();
            this.added_inp_rawMIN = new Config_PLC_SIEMENS.CustomControl.DigitTextBox();
            this.added_l_namePLC = new System.Windows.Forms.Label();
            this.added_inp_namePLC = new System.Windows.Forms.TextBox();
            this.added_l_nameScada = new System.Windows.Forms.Label();
            this.added_inp_nameScada = new System.Windows.Forms.TextBox();
            this.added_l_descr = new System.Windows.Forms.Label();
            this.added_inp_descr = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // added_b_cancel
            // 
            this.added_b_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.added_b_cancel.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.added_b_cancel.Location = new System.Drawing.Point(623, 250);
            this.added_b_cancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.added_b_cancel.Name = "added_b_cancel";
            this.added_b_cancel.Size = new System.Drawing.Size(140, 40);
            this.added_b_cancel.TabIndex = 11;
            this.added_b_cancel.Text = "Отменить";
            this.added_b_cancel.UseVisualStyleBackColor = true;
            // 
            // added_b_ok
            // 
            this.added_b_ok.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.added_b_ok.Location = new System.Drawing.Point(456, 250);
            this.added_b_ok.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.added_b_ok.Name = "added_b_ok";
            this.added_b_ok.Size = new System.Drawing.Size(140, 40);
            this.added_b_ok.TabIndex = 10;
            this.added_b_ok.Text = "Принять";
            this.added_b_ok.UseVisualStyleBackColor = true;
            this.added_b_ok.Click += new System.EventHandler(this.AddedBOkClick);
            // 
            // added_l_timeFiltr
            // 
            this.added_l_timeFiltr.AutoSize = true;
            this.added_l_timeFiltr.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.added_l_timeFiltr.Location = new System.Drawing.Point(27, 218);
            this.added_l_timeFiltr.Name = "added_l_timeFiltr";
            this.added_l_timeFiltr.Size = new System.Drawing.Size(158, 24);
            this.added_l_timeFiltr.TabIndex = 25;
            this.added_l_timeFiltr.Text = "Время фильтрации";
            this.added_l_timeFiltr.Visible = false;
            // 
            // added_inp_timeFiltr
            // 
            this.added_inp_timeFiltr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.added_inp_timeFiltr.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.added_inp_timeFiltr.Location = new System.Drawing.Point(251, 217);
            this.added_inp_timeFiltr.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.added_inp_timeFiltr.Name = "added_inp_timeFiltr";
            this.added_inp_timeFiltr.Size = new System.Drawing.Size(119, 30);
            this.added_inp_timeFiltr.TabIndex = 5;
            this.added_inp_timeFiltr.Text = "0";
            this.added_inp_timeFiltr.Value = 0D;
            this.added_inp_timeFiltr.Visible = false;
            this.added_inp_timeFiltr.TextChanged += new System.EventHandler(this.added_inp_rawMIN_TextChanged);
            // 
            // added_enInv
            // 
            this.added_enInv.AutoSize = true;
            this.added_enInv.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.added_enInv.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.added_enInv.Location = new System.Drawing.Point(375, 185);
            this.added_enInv.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.added_enInv.Name = "added_enInv";
            this.added_enInv.Size = new System.Drawing.Size(186, 28);
            this.added_enInv.TabIndex = 9;
            this.added_enInv.Text = "Включить инверсию";
            this.added_enInv.UseVisualStyleBackColor = true;
            // 
            // added_ch_enFiltr
            // 
            this.added_ch_enFiltr.AutoSize = true;
            this.added_ch_enFiltr.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.added_ch_enFiltr.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.added_ch_enFiltr.Location = new System.Drawing.Point(27, 186);
            this.added_ch_enFiltr.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.added_ch_enFiltr.Name = "added_ch_enFiltr";
            this.added_ch_enFiltr.Size = new System.Drawing.Size(209, 28);
            this.added_ch_enFiltr.TabIndex = 4;
            this.added_ch_enFiltr.Text = "Включить фильтрацию";
            this.added_ch_enFiltr.UseVisualStyleBackColor = true;
            this.added_ch_enFiltr.CheckedChanged += new System.EventHandler(this.AddedChEnFiltrCheckedChanged);
            // 
            // added_l_maxEU
            // 
            this.added_l_maxEU.AutoSize = true;
            this.added_l_maxEU.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.added_l_maxEU.Location = new System.Drawing.Point(375, 150);
            this.added_l_maxEU.Name = "added_l_maxEU";
            this.added_l_maxEU.Size = new System.Drawing.Size(212, 24);
            this.added_l_maxEU.TabIndex = 21;
            this.added_l_maxEU.Text = "Макс. значение в инж. ед.";
            // 
            // added_inp_maxEU
            // 
            this.added_inp_maxEU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.added_inp_maxEU.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.added_inp_maxEU.Location = new System.Drawing.Point(627, 146);
            this.added_inp_maxEU.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.added_inp_maxEU.Name = "added_inp_maxEU";
            this.added_inp_maxEU.Size = new System.Drawing.Size(143, 30);
            this.added_inp_maxEU.TabIndex = 8;
            this.added_inp_maxEU.Text = "0";
            this.added_inp_maxEU.Value = 0D;
            this.added_inp_maxEU.TextChanged += new System.EventHandler(this.added_inp_rawMIN_TextChanged);
            // 
            // added_l_minEU
            // 
            this.added_l_minEU.AutoSize = true;
            this.added_l_minEU.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.added_l_minEU.Location = new System.Drawing.Point(375, 110);
            this.added_l_minEU.Name = "added_l_minEU";
            this.added_l_minEU.Size = new System.Drawing.Size(206, 24);
            this.added_l_minEU.TabIndex = 19;
            this.added_l_minEU.Text = "Мин. значение в инж. ед.";
            // 
            // added_inp_minEU
            // 
            this.added_inp_minEU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.added_inp_minEU.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.added_inp_minEU.Location = new System.Drawing.Point(627, 107);
            this.added_inp_minEU.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.added_inp_minEU.Name = "added_inp_minEU";
            this.added_inp_minEU.Size = new System.Drawing.Size(143, 30);
            this.added_inp_minEU.TabIndex = 7;
            this.added_inp_minEU.Text = "0";
            this.added_inp_minEU.Value = 0D;
            this.added_inp_minEU.TextChanged += new System.EventHandler(this.added_inp_rawMIN_TextChanged);
            // 
            // added_l_maxRAW
            // 
            this.added_l_maxRAW.AutoSize = true;
            this.added_l_maxRAW.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.added_l_maxRAW.Location = new System.Drawing.Point(27, 150);
            this.added_l_maxRAW.Name = "added_l_maxRAW";
            this.added_l_maxRAW.Size = new System.Drawing.Size(184, 24);
            this.added_l_maxRAW.TabIndex = 17;
            this.added_l_maxRAW.Text = "Мак. значение в кодах";
            // 
            // added_inp_rawMAX
            // 
            this.added_inp_rawMAX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.added_inp_rawMAX.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.added_inp_rawMAX.Location = new System.Drawing.Point(251, 146);
            this.added_inp_rawMAX.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.added_inp_rawMAX.Name = "added_inp_rawMAX";
            this.added_inp_rawMAX.Size = new System.Drawing.Size(119, 30);
            this.added_inp_rawMAX.TabIndex = 3;
            this.added_inp_rawMAX.Text = "0";
            this.added_inp_rawMAX.Value = 0D;
            this.added_inp_rawMAX.TextChanged += new System.EventHandler(this.added_inp_rawMIN_TextChanged);
            // 
            // added_l_minRAW
            // 
            this.added_l_minRAW.AutoSize = true;
            this.added_l_minRAW.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.added_l_minRAW.Location = new System.Drawing.Point(27, 110);
            this.added_l_minRAW.Name = "added_l_minRAW";
            this.added_l_minRAW.Size = new System.Drawing.Size(186, 24);
            this.added_l_minRAW.TabIndex = 15;
            this.added_l_minRAW.Text = "Мин. значение в кодах";
            // 
            // added_inp_rawMIN
            // 
            this.added_inp_rawMIN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.added_inp_rawMIN.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.added_inp_rawMIN.Location = new System.Drawing.Point(251, 107);
            this.added_inp_rawMIN.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.added_inp_rawMIN.Name = "added_inp_rawMIN";
            this.added_inp_rawMIN.Size = new System.Drawing.Size(119, 30);
            this.added_inp_rawMIN.TabIndex = 2;
            this.added_inp_rawMIN.Text = "0";
            this.added_inp_rawMIN.Value = 0D;
            this.added_inp_rawMIN.TextChanged += new System.EventHandler(this.added_inp_rawMIN_TextChanged);
            // 
            // added_l_namePLC
            // 
            this.added_l_namePLC.AutoSize = true;
            this.added_l_namePLC.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.added_l_namePLC.Location = new System.Drawing.Point(465, 34);
            this.added_l_namePLC.Name = "added_l_namePLC";
            this.added_l_namePLC.Size = new System.Drawing.Size(79, 24);
            this.added_l_namePLC.TabIndex = 31;
            this.added_l_namePLC.Text = "Имя PLC";
            // 
            // added_inp_namePLC
            // 
            this.added_inp_namePLC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.added_inp_namePLC.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.added_inp_namePLC.Location = new System.Drawing.Point(567, 32);
            this.added_inp_namePLC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.added_inp_namePLC.Name = "added_inp_namePLC";
            this.added_inp_namePLC.Size = new System.Drawing.Size(203, 30);
            this.added_inp_namePLC.TabIndex = 6;
            this.added_inp_namePLC.TextChanged += new System.EventHandler(this.added_inp_rawMIN_TextChanged);
            // 
            // added_l_nameScada
            // 
            this.added_l_nameScada.AutoSize = true;
            this.added_l_nameScada.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.added_l_nameScada.Location = new System.Drawing.Point(27, 34);
            this.added_l_nameScada.Name = "added_l_nameScada";
            this.added_l_nameScada.Size = new System.Drawing.Size(104, 24);
            this.added_l_nameScada.TabIndex = 29;
            this.added_l_nameScada.Text = "Имя SCADA";
            // 
            // added_inp_nameScada
            // 
            this.added_inp_nameScada.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.added_inp_nameScada.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.added_inp_nameScada.Location = new System.Drawing.Point(251, 32);
            this.added_inp_nameScada.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.added_inp_nameScada.Name = "added_inp_nameScada";
            this.added_inp_nameScada.Size = new System.Drawing.Size(209, 30);
            this.added_inp_nameScada.TabIndex = 1;
            this.added_inp_nameScada.TextChanged += new System.EventHandler(this.added_inp_rawMIN_TextChanged);
            // 
            // added_l_descr
            // 
            this.added_l_descr.AutoSize = true;
            this.added_l_descr.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.added_l_descr.Location = new System.Drawing.Point(27, 73);
            this.added_l_descr.Name = "added_l_descr";
            this.added_l_descr.Size = new System.Drawing.Size(182, 24);
            this.added_l_descr.TabIndex = 32;
            this.added_l_descr.Text = "Описание переменной";
            // 
            // added_inp_descr
            // 
            this.added_inp_descr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.added_inp_descr.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.added_inp_descr.Location = new System.Drawing.Point(251, 70);
            this.added_inp_descr.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.added_inp_descr.Name = "added_inp_descr";
            this.added_inp_descr.Size = new System.Drawing.Size(519, 30);
            this.added_inp_descr.TabIndex = 33;
            // 
            // AddEditTag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.added_b_cancel;
            this.ClientSize = new System.Drawing.Size(792, 313);
            this.Controls.Add(this.added_inp_descr);
            this.Controls.Add(this.added_l_descr);
            this.Controls.Add(this.added_l_namePLC);
            this.Controls.Add(this.added_inp_namePLC);
            this.Controls.Add(this.added_l_nameScada);
            this.Controls.Add(this.added_inp_nameScada);
            this.Controls.Add(this.added_b_cancel);
            this.Controls.Add(this.added_b_ok);
            this.Controls.Add(this.added_l_timeFiltr);
            this.Controls.Add(this.added_inp_timeFiltr);
            this.Controls.Add(this.added_enInv);
            this.Controls.Add(this.added_ch_enFiltr);
            this.Controls.Add(this.added_l_maxEU);
            this.Controls.Add(this.added_inp_maxEU);
            this.Controls.Add(this.added_l_minEU);
            this.Controls.Add(this.added_inp_minEU);
            this.Controls.Add(this.added_l_maxRAW);
            this.Controls.Add(this.added_inp_rawMAX);
            this.Controls.Add(this.added_l_minRAW);
            this.Controls.Add(this.added_inp_rawMIN);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddEditTag";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добавить переменную";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.AddEditTagLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button added_b_cancel;
        private System.Windows.Forms.Button added_b_ok;
        private System.Windows.Forms.Label added_l_timeFiltr;
        private CustomControl.DigitTextBox added_inp_timeFiltr;
        private System.Windows.Forms.CheckBox added_enInv;
        private System.Windows.Forms.CheckBox added_ch_enFiltr;
        private System.Windows.Forms.Label added_l_maxEU;
        private CustomControl.DigitTextBox added_inp_maxEU;
        private System.Windows.Forms.Label added_l_minEU;
        private CustomControl.DigitTextBox added_inp_minEU;
        private System.Windows.Forms.Label added_l_maxRAW;
        private CustomControl.DigitTextBox added_inp_rawMAX;
        private System.Windows.Forms.Label added_l_minRAW;
        private CustomControl.DigitTextBox added_inp_rawMIN;
        private System.Windows.Forms.Label added_l_namePLC;
        private System.Windows.Forms.TextBox added_inp_namePLC;
        private System.Windows.Forms.Label added_l_nameScada;
        private System.Windows.Forms.TextBox added_inp_nameScada;
        private System.Windows.Forms.Label added_l_descr;
        private System.Windows.Forms.TextBox added_inp_descr;
    }
}