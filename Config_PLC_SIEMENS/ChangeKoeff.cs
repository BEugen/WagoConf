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
    public partial class ChangeKoeff : Form
    {
        public ChangeKoeff()
        {
            InitializeComponent();
        }

        public double KoeffOpen
        {
            get { return inp_koeff_open.Value; }
            set { inp_koeff_open.Value = value; }
        }
        public double KoeffClose
        {
            get { return inp_koeff_close.Value; }
            set { inp_koeff_close.Value = value; }
        }



        private void InpKoeffOpenKeyUp(object sender, KeyEventArgs e)
        {
            inp_koeff_close.Value = 1.0 - inp_koeff_open.Value;
        }

        private void InpKoeffCloseKeyUp(object sender, KeyEventArgs e)
        {
            inp_koeff_open.Value = 1.0 - inp_koeff_close.Value;
        }

    }
}
