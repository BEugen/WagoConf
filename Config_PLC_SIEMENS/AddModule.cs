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
    public partial class Set_form_module_add : Form
    {
        public Set_form_module_add()
        {
            InitializeComponent();          
            GetTypeChannel();
            if(set_frm_ddl_type_modul.Items.Count > 0)
                set_frm_ddl_type_modul.SelectedIndex = 0;
        }
        public int CountChannel
        {
            get
            {
                return (int)set_frm_n_channel_count.Value;
            }
        }
        public int ModuleType
        {
            get
            {
                return set_frm_ddl_type_modul.SelectedIndex;
            }
        }
        private void GetTypeChannel()
        {
            RtpConfigDataContext data = new RtpConfigDataContext();
            set_frm_ddl_type_modul.Items.Clear();
            foreach (var types in data.GetModulType())
            {
                set_frm_ddl_type_modul.Items.Add(types.descript);
            }
        }
    }
}
