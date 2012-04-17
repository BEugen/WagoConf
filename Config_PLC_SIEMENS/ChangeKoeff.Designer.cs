namespace Config_PLC_SIEMENS
{
    partial class ChangeKoeff
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
            this.set_frm_b_ok = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.set_frm_l_koeff_open = new System.Windows.Forms.Label();
            this.set_frm_l_koeff_close = new System.Windows.Forms.Label();
            this.inp_koeff_open = new Config_PLC_SIEMENS.CustomControl.DigitTextBox();
            this.inp_koeff_close = new Config_PLC_SIEMENS.CustomControl.DigitTextBox();
            this.SuspendLayout();
            // 
            // set_frm_b_ok
            // 
            this.set_frm_b_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.set_frm_b_ok.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.set_frm_b_ok.Location = new System.Drawing.Point(38, 122);
            this.set_frm_b_ok.Margin = new System.Windows.Forms.Padding(2);
            this.set_frm_b_ok.Name = "set_frm_b_ok";
            this.set_frm_b_ok.Size = new System.Drawing.Size(105, 32);
            this.set_frm_b_ok.TabIndex = 5;
            this.set_frm_b_ok.Text = "Применить";
            // 
            // cancel
            // 
            this.cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cancel.Location = new System.Drawing.Point(204, 122);
            this.cancel.Margin = new System.Windows.Forms.Padding(2);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(105, 32);
            this.cancel.TabIndex = 6;
            this.cancel.Text = "Отмена";
            // 
            // set_frm_l_koeff_open
            // 
            this.set_frm_l_koeff_open.AutoSize = true;
            this.set_frm_l_koeff_open.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.set_frm_l_koeff_open.Location = new System.Drawing.Point(34, 34);
            this.set_frm_l_koeff_open.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.set_frm_l_koeff_open.Name = "set_frm_l_koeff_open";
            this.set_frm_l_koeff_open.Size = new System.Drawing.Size(156, 20);
            this.set_frm_l_koeff_open.TabIndex = 7;
            this.set_frm_l_koeff_open.Text = "Коэффициент открытия";
            // 
            // set_frm_l_koeff_close
            // 
            this.set_frm_l_koeff_close.AutoSize = true;
            this.set_frm_l_koeff_close.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.set_frm_l_koeff_close.Location = new System.Drawing.Point(34, 76);
            this.set_frm_l_koeff_close.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.set_frm_l_koeff_close.Name = "set_frm_l_koeff_close";
            this.set_frm_l_koeff_close.Size = new System.Drawing.Size(156, 20);
            this.set_frm_l_koeff_close.TabIndex = 8;
            this.set_frm_l_koeff_close.Text = "Коэффициент закрытия";
            // 
            // inp_koeff_open
            // 
            this.inp_koeff_open.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.inp_koeff_open.Location = new System.Drawing.Point(207, 31);
            this.inp_koeff_open.Name = "inp_koeff_open";
            this.inp_koeff_open.Size = new System.Drawing.Size(100, 26);
            this.inp_koeff_open.TabIndex = 9;
            this.inp_koeff_open.Text = "0";
            this.inp_koeff_open.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.inp_koeff_open.Value = 0D;
            this.inp_koeff_open.KeyUp += new System.Windows.Forms.KeyEventHandler(this.InpKoeffOpenKeyUp);
            // 
            // inp_koeff_close
            // 
            this.inp_koeff_close.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.inp_koeff_close.Location = new System.Drawing.Point(207, 73);
            this.inp_koeff_close.Name = "inp_koeff_close";
            this.inp_koeff_close.Size = new System.Drawing.Size(100, 26);
            this.inp_koeff_close.TabIndex = 10;
            this.inp_koeff_close.Text = "0";
            this.inp_koeff_close.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.inp_koeff_close.Value = 0D;
            this.inp_koeff_close.KeyUp += new System.Windows.Forms.KeyEventHandler(this.InpKoeffCloseKeyUp);
            // 
            // ChangeKoeff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 176);
            this.Controls.Add(this.inp_koeff_close);
            this.Controls.Add(this.inp_koeff_open);
            this.Controls.Add(this.set_frm_l_koeff_close);
            this.Controls.Add(this.set_frm_l_koeff_open);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.set_frm_b_ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangeKoeff";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Введите коэффициент";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button set_frm_b_ok;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Label set_frm_l_koeff_open;
        private System.Windows.Forms.Label set_frm_l_koeff_close;
        private CustomControl.DigitTextBox inp_koeff_open;
        private CustomControl.DigitTextBox inp_koeff_close;
    }
}