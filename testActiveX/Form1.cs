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
        int[] param = new int[7];
        Log log = new Log();
        public Form1()
        {
            InitializeComponent();
            tmrElapsedCMD = new System.Timers.Timer(2000);
            tmrElapsedCMD.Elapsed += new System.Timers.ElapsedEventHandler(tmrElapsedCMD_Elapsed);
            configPLC_S71.CommandEvent += new Config_PLC_SIEMENS.ConfigPLC_S7.CommandEventHandler(configPLC_S71_CommandEvent);
            tmrElapsedCMD.Start();
        }

        void configPLC_S71_CommandEvent()
        {
            if (param[0] != configPLC_S71.Command ||
                param[1] != configPLC_S71.P1 ||
                param[2] != configPLC_S71.P2 ||
                param[3] != configPLC_S71.P3 ||
                param[4] != configPLC_S71.P4 ||
                param[5] != configPLC_S71.P5 ||
                param[6] != configPLC_S71.P6)
            {
                param[0] = configPLC_S71.Command;
                param[1] = configPLC_S71.P1;
                param[2] = configPLC_S71.P2;
                param[3] = configPLC_S71.P3;
                param[4] = configPLC_S71.P4;
                param[5] = configPLC_S71.P5;
                param[6] = configPLC_S71.P6;

                log.AddText = "Command:"+ param[0] + "; P1:" + param[1] + "; P2:" +
                              param[2] + "; P3:" + param[3] + "; P4:" + param[4] +
                              "; P5:" + param[5] + "; P6:" + param[6];
                if (configPLC_S71.Command != 0)
                    configPLC_S71.Accept = configPLC_S71.Command * 10;
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            log.Show();
        }
    }
}
