using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Config_PLC_SIEMENS
{
    public partial class AddEditTag : Form
    {
        int _minraw = 0;
        int _maxraw = 0;
        int _timefiltr = 0;
        double _mineu = 0.0;
        double _maxeu = 0.0;    
        public string NamePLC { get; set; }
        public string NameScada { get; set; }
        public string Description { get; set; }
        public double EUmin { get; set; }
        public double EUmax { get; set; }
        public int RAWmin { get; set; }
        public int RAWmax { get; set; }
        public int TimeFilter { get; set; }
        public bool TimeFiltrEnable { get; set; }
        public bool Invert { get; set; }
        public bool Edit { get; set; }

        public AddEditTag()
        {
            InitializeComponent();
        }

        private void AddedBOkClick(object sender, EventArgs e)
        {
            
            try
            {
                _minraw = Convert.ToInt32(added_inp_rawMIN.Value);
            }
            catch
            {
                MessageBox.Show("Введите правильное минимальное значение в кодах", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                added_inp_rawMIN.BackColor = Color.Red;
                added_inp_rawMIN.Focus();
                return;
            }
            try
            {
                _maxraw = Convert.ToInt32(added_inp_rawMAX.Value);
            }
            catch
            {
                MessageBox.Show("Введите правильное максимальное значение в кодах", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                added_inp_rawMAX.BackColor = Color.Red;
                added_inp_rawMAX.Focus();
                return;
            }
           
                _mineu = added_inp_minEU.Value;
                _maxeu = added_inp_maxEU.Value;
            
            if (added_inp_timeFiltr.Text != "")
            {
                try
                {
                    _timefiltr = Convert.ToInt32(added_inp_timeFiltr.Text);
                }
                catch
                {
                    MessageBox.Show("Введите правильное значение времени фильтрации", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    added_inp_timeFiltr.BackColor = Color.Red;
                    added_inp_timeFiltr.Focus();
                    return;
                }
            }
            if (_minraw >= _maxraw)
            {
                MessageBox.Show("Минимальное значение не может быть больше\n или равно максимальному в кодах модуля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                added_inp_rawMIN.BackColor = Color.Red;
                added_inp_rawMIN.Focus();
                return;
            }
            if (_mineu >= _maxeu)
            {
                MessageBox.Show("Минимальное значение не может быть больше\n или равно максимальному в инженерных еденицах", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                added_inp_minEU.BackColor = Color.Red;
                added_inp_minEU.Focus();
                return;
            }
            if (!Edit)
            {
                ConfigPLCStore configClass = new ConfigPLCStore();
                if(configClass.CheckNamePlc(added_inp_namePLC.Text))
                {
                    MessageBox.Show("Данное имя PLC уже существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            added_inp_namePLC.BackColor = Color.Red;
                            added_inp_namePLC.Focus();
                            return;
                }
                if(configClass.CheckNameSCADA(added_inp_nameScada.Text))
                {
                    MessageBox.Show("Данное имя SCADA уже существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            added_inp_nameScada.BackColor = Color.Red;
                            added_inp_nameScada.Focus();
                            return;
                }
            }
            NamePLC = added_inp_namePLC.Text;
            NameScada = added_inp_nameScada.Text;
            Description = added_inp_descr.Text;
            EUmax = _maxeu;
            EUmin = _mineu;
            RAWmax = _maxraw;
            RAWmin = _minraw;
            TimeFilter = _timefiltr;
            TimeFiltrEnable = added_ch_enFiltr.Checked;
            Invert = added_enInv.Checked;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

        }

        private void AddedChEnFiltrCheckedChanged(object sender, EventArgs e)
        {
            if (added_ch_enFiltr.Checked)
            {
                added_l_timeFiltr.Visible = true;
                added_inp_timeFiltr.Visible = true;
            }
            else
            {
                added_l_timeFiltr.Visible = false;
                added_inp_timeFiltr.Visible = false;
            }
        }

        private void AddEditTagLoad(object sender, EventArgs e)
        {
             added_inp_namePLC.Text = NamePLC;
             added_inp_nameScada.Text= NameScada;
             added_inp_descr.Text = Description;
             added_inp_maxEU.Text = EUmax.ToString();
             added_inp_minEU.Text = EUmin.ToString();
             added_inp_rawMAX.Text = RAWmax.ToString();
             added_inp_rawMIN.Text = RAWmin.ToString();
             added_inp_timeFiltr.Text = TimeFilter.ToString();
             added_ch_enFiltr.Checked = TimeFiltrEnable;
             added_enInv.Checked = Invert;
             if (added_ch_enFiltr.Checked)
             {
                 added_l_timeFiltr.Visible = true;
                 added_inp_timeFiltr.Visible = true;
             }
             else
             {
                 added_l_timeFiltr.Visible = false;
                 added_inp_timeFiltr.Visible = false;
             }
        }

        private void added_inp_rawMIN_TextChanged(object sender, EventArgs e)
        {
            ((TextBox)sender).BackColor = System.Drawing.SystemColors.Window;
        }
    }
}
