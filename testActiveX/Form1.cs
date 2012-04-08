using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace testActiveX
{
    public partial class Form1 : Form
    {
        System.Timers.Timer tmrElapsedCMD;
        delegate void UpdateInterface(bool result, string err);
        public Form1()
        {
            InitializeComponent();
            tmrElapsedCMD = new System.Timers.Timer(1000);
            tmrElapsedCMD.Elapsed += new System.Timers.ElapsedEventHandler(tmrElapsedCMD_Elapsed);
            tmrElapsedCMD.Start();
        }

        void tmrElapsedCMD_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                UpdateInterface ui = new UpdateInterface(UI);
                cmd.BeginInvoke(ui, new object[] { true, "" });
            }
            catch { }
            
        }

        private void configPLC_S71_Load(object sender, EventArgs e)
        {
          
        }

        private void UI(bool ui, string err)
        {
            cmd.Text = configPLC_S71.Command.ToString();
           // address.Text = configPLC_S71.Address.ToString();
            p1.Text = configPLC_S71.P1.ToString();
            p2.Text = configPLC_S71.P2.ToString();
            p3.Text = configPLC_S71.P3.ToString();
            p4.Text = configPLC_S71.P4.ToString();
            p5.Text = configPLC_S71.P5.ToString(); 
            p6.Text = configPLC_S71.P6.ToString();
        }

        private void accept_Click(object sender, EventArgs e)
        {
            if(configPLC_S71.Accept == 1)
                    configPLC_S71.Accept += 1;
        }
    }
}
