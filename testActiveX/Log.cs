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
    public partial class Log : Form
    {
        private delegate void LogEventArg(string text);
        public string AddText
        {
            set
            {
                if (richTextBox1.InvokeRequired)
                  richTextBox1.BeginInvoke(new LogEventArg(LogsText), new object[] { DateTime.Now.ToString("d.MM.yyyy HH:mm:ss") + " " + value + "\n\r" });
            }
        }
        public Log()
        {
            InitializeComponent();
        }

        private void LogsText(string text)
        {
            richTextBox1.AppendText(text);
        }
    }
}
