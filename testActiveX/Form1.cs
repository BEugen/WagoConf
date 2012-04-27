using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            tmrElapsedCMD.Elapsed += tmrElapsedCMD_Elapsed;
            configPLC_S71.CommandEvent += configPLC_S71_CommandEvent;
            
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

                WriteToLogFile(param);

                log.AddText = "Command:"+ param[0] + "; P1:" + param[1] + "; P2:" +
                              param[2] + "; P3:" + param[3] + "; P4:" + param[4] +
                              "; P5:" + param[5] + "; P6:" + param[6];
                tmrElapsedCMD.Start();
                
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
            if (configPLC_S71.Command != 0)
                    configPLC_S71.Accept = configPLC_S71.Command * 10;


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

        private void button2_Click(object sender, EventArgs e)
        {
            configPLC_S71.ShiberSelect = 24;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            configPLC_S71.CurrentAccessLevel = 9999;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            configPLC_S71.GroupSetup = 1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            configPLC_S71.SingleSetup = 1;
        }

        private void WriteToLogFile(int[] param)
        {
           StreamWriter str = new StreamWriter("protocol.txt", true, Encoding.Default);
            string str_command = "";
            switch (param[0])
            {
                case 1:
                    str_command = DateTime.Now.ToString("d.MM.yyyy") + "=============================================\n";
                    str_command += "Команда сохранения времени открытия и закрытия\n";
                    str_command += "Номер шибера: " + param[1] + "\n";
                    str_command += "Время открытия: " + param[2] + "\n";
                    str_command += "Время закрытия: " + param[3] + "\n";
                    str_command +=
                        "==================================================================================\n";
                   break;
                case 2:
                    str_command = DateTime.Now.ToString("d.MM.yyyy") + "=============================================\n";
                    str_command += "Команда сохранения настроек переоткрытия\n";
                    str_command += "Номер шибера: " + param[1] + "\n";
                    str_command += "Количество аереоткрытий: " + param[2] + "\n";
                    str_command +=
                        "==================================================================================\n";
                    break;
                case 3:
                    str_command = DateTime.Now.ToString("d.MM.yyyy") + "=============================================\n";
                    str_command += "Команда сохранения настроек модулей\n";
                    str_command += "Номер шибера: " + param[1] + "\n";
                    str_command += "Номер канала закрытия шибера: " + param[2] + "\n";
                    str_command += "Номер каналя ручной/автомат: " + param[3] + "\n";
                    str_command += "Номер канала открытия: " + param[4] + "\n";
                    str_command += "Номер канала закрытия: " + param[5] + "\n";
                    str_command +=
                        "==================================================================================\n";
                    break;
                case 4:
                    str_command = DateTime.Now.ToString("d.MM.yyyy") + "=============================================\n";
                    str_command += "Команда сохранения смещения адресов модулей\n";
                    str_command += "Номер шибера: " + param[1] + "\n";
                    str_command += "Смещение байта закрытия шибера: " + param[2] + "\n";
                    str_command += "Смещение байта ручной/автомат: " + param[3] + "\n";
                    str_command += "Смещение байта открытия: " + param[4] + "\n";
                    str_command += "Смещение байта закрытия: " + param[5] + "\n";
                    str_command +=
                        "==================================================================================\n";
                    break;
                case 5:
                    str_command = DateTime.Now.ToString("d.MM.yyyy") + "=============================================\n";
                    str_command += "Команда сохранения настроек общих каналов\n";
                    str_command += "Номер шибера: " + param[1] + "\n";
                    str_command += "Номер канала: " + param[2] + "\n";
                    str_command += "Тип канала: " + param[3] + "\n";
                    str_command += "Смещение байта " + param[4] + "\n";
                    str_command +=
                        "==================================================================================\n";
                    break;
                case 6:
                    str_command = DateTime.Now.ToString("d.MM.yyyy") + "=============================================\n";
                    str_command += "Команда сохранения настроек шибера для одиночного режима\n";
                    str_command += "Номер последовательности: " + param[1] + "\n";
                    str_command += "Номер шибера: " + param[2] + "\n";
                    str_command += "Время между шиберами: " + param[3] + "\n";
                    str_command +=
                        "==================================================================================\n";
                    break;
                case 7:
                    str_command = DateTime.Now.ToString("d.MM.yyyy") + "=============================================\n";
                    str_command += "Команда сохранения настроек группового режима\n";
                    str_command += "Номер последовательности: " + param[1] + "\n";
                    str_command += "Номер группы: " + param[2] + "\n";
                    str_command +=
                        "==================================================================================\n";
                    break;
                case 8:
                    str_command = DateTime.Now.ToString("d.MM.yyyy") + "=============================================\n";
                    str_command += "Команда сохранения настроек группы для группового режима\n";
                    str_command += "Номер группы: " + param[1] + "\n";
                    str_command += "Номер шибера 1: " + param[2] + "\n";
                    str_command += "Номер шибера 2: " + param[3] + "\n";
                    str_command += "Время до следующей загрузки: " + param[4] + "\n";
                    str_command +=
                        "==================================================================================\n";
                    break;
                case 10:
                    str_command = DateTime.Now.ToString("d.MM.yyyy") + "=============================================\n";
                    str_command += "Команда сохранения настроек времни 2ч. для шиберов\n";
                    str_command += "Номер шибера: " + param[1] + "\n";
                    str_command += "Время открытия для устранения аварии: " + param[2] + "\n";
                    str_command += "Время закрытия для устранения аварии: " + param[3] + "\n";
                    str_command += "Время до следующей загрузки: " + param[4] + "\n";
                    str_command +=
                        "==================================================================================\n";
                    break;
                case 11:
                    str_command = DateTime.Now.ToString("d.MM.yyyy") + "=============================================\n";
                    str_command += "Команда сохранения времени между циклом\n";
                    str_command += "Время между циклом: " + param[1] + "\n";
                    str_command +=
                        "==================================================================================\n";
                    break;
            }
           str.WriteLine(str_command);
           str.Close();
        }
    }
}
