using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RtpWagoConf
{
    public partial class ChangeKoeff : Form
    {
        private double _kOpen = 0.0;
        private double _kClose = 0.0;
        public ChangeKoeff()
        {
            InitializeComponent();
        }

        public double KoeffOpen
        {
            get { return _kOpen; }
            set { inp_koeff_open.Value = value; }
        }
        public double KoeffClose
        {
            get { return _kClose; }
            set { inp_koeff_close.Value = value; }
        }



        private void InpKoeffOpenKeyUp(object sender, KeyEventArgs e)
        {
           inp_koeff_close.Value = _kClose= 1.0 - inp_koeff_open.Value;
            _kOpen = 1.0 - _kClose;
        }  

        private void InpKoeffCloseKeyUp(object sender, KeyEventArgs e)
        {
           inp_koeff_open.Value = _kOpen = 1.0 - inp_koeff_close.Value;
           _kClose = 1.0 - _kOpen;
        }

    }
}
