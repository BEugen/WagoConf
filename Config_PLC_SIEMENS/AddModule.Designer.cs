namespace RtpWagoConf
{
    partial class Set_form_module_add
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
            this.set_frm_l_type_modul = new System.Windows.Forms.Label();
            this.set_frm_ddl_type_modul = new System.Windows.Forms.ComboBox();
            this.set_frm_l_channel_count = new System.Windows.Forms.Label();
            this.set_frm_n_channel_count = new System.Windows.Forms.NumericUpDown();
            this.set_frm_b_ok = new System.Windows.Forms.Button();
            this.set_frm_b_cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.set_frm_n_channel_count)).BeginInit();
            this.SuspendLayout();
            // 
            // set_frm_l_type_modul
            // 
            this.set_frm_l_type_modul.AutoSize = true;
            this.set_frm_l_type_modul.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.set_frm_l_type_modul.Location = new System.Drawing.Point(10, 20);
            this.set_frm_l_type_modul.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.set_frm_l_type_modul.Name = "set_frm_l_type_modul";
            this.set_frm_l_type_modul.Size = new System.Drawing.Size(109, 22);
            this.set_frm_l_type_modul.TabIndex = 0;
            this.set_frm_l_type_modul.Text = "Тип модуля";
            // 
            // set_frm_ddl_type_modul
            // 
            this.set_frm_ddl_type_modul.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.set_frm_ddl_type_modul.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.set_frm_ddl_type_modul.FormattingEnabled = true;
            this.set_frm_ddl_type_modul.Items.AddRange(new object[] {
            "Модуль аналоговый ввода (AI)",
            "Модуль аналоговый вывода (AO)"});
            this.set_frm_ddl_type_modul.Location = new System.Drawing.Point(126, 17);
            this.set_frm_ddl_type_modul.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.set_frm_ddl_type_modul.Name = "set_frm_ddl_type_modul";
            this.set_frm_ddl_type_modul.Size = new System.Drawing.Size(254, 30);
            this.set_frm_ddl_type_modul.TabIndex = 1;
            // 
            // set_frm_l_channel_count
            // 
            this.set_frm_l_channel_count.AutoSize = true;
            this.set_frm_l_channel_count.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.set_frm_l_channel_count.Location = new System.Drawing.Point(10, 58);
            this.set_frm_l_channel_count.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.set_frm_l_channel_count.Name = "set_frm_l_channel_count";
            this.set_frm_l_channel_count.Size = new System.Drawing.Size(186, 22);
            this.set_frm_l_channel_count.TabIndex = 2;
            this.set_frm_l_channel_count.Text = "Количество каналов";
            // 
            // set_frm_n_channel_count
            // 
            this.set_frm_n_channel_count.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.set_frm_n_channel_count.Location = new System.Drawing.Point(247, 57);
            this.set_frm_n_channel_count.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.set_frm_n_channel_count.Name = "set_frm_n_channel_count";
            this.set_frm_n_channel_count.Size = new System.Drawing.Size(133, 29);
            this.set_frm_n_channel_count.TabIndex = 3;
            this.set_frm_n_channel_count.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.set_frm_n_channel_count.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // set_frm_b_ok
            // 
            this.set_frm_b_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.set_frm_b_ok.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.set_frm_b_ok.Location = new System.Drawing.Point(41, 114);
            this.set_frm_b_ok.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.set_frm_b_ok.Name = "set_frm_b_ok";
            this.set_frm_b_ok.Size = new System.Drawing.Size(140, 40);
            this.set_frm_b_ok.TabIndex = 4;
            this.set_frm_b_ok.Text = "Применить";
            // 
            // set_frm_b_cancel
            // 
            this.set_frm_b_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.set_frm_b_cancel.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.set_frm_b_cancel.Location = new System.Drawing.Point(220, 114);
            this.set_frm_b_cancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.set_frm_b_cancel.Name = "set_frm_b_cancel";
            this.set_frm_b_cancel.Size = new System.Drawing.Size(140, 40);
            this.set_frm_b_cancel.TabIndex = 5;
            this.set_frm_b_cancel.Text = "Отмена";
            this.set_frm_b_cancel.UseVisualStyleBackColor = true;
            // 
            // Set_form_module_add
            // 
            this.AcceptButton = this.set_frm_b_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.set_frm_b_ok;
            this.ClientSize = new System.Drawing.Size(405, 172);
            this.Controls.Add(this.set_frm_b_cancel);
            this.Controls.Add(this.set_frm_b_ok);
            this.Controls.Add(this.set_frm_n_channel_count);
            this.Controls.Add(this.set_frm_l_channel_count);
            this.Controls.Add(this.set_frm_ddl_type_modul);
            this.Controls.Add(this.set_frm_l_type_modul);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Set_form_module_add";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добавление модуля";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.set_frm_n_channel_count)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label set_frm_l_type_modul;
        private System.Windows.Forms.ComboBox set_frm_ddl_type_modul;
        private System.Windows.Forms.Label set_frm_l_channel_count;
        private System.Windows.Forms.NumericUpDown set_frm_n_channel_count;
        private System.Windows.Forms.Button set_frm_b_ok;
        private System.Windows.Forms.Button set_frm_b_cancel;
    }
}