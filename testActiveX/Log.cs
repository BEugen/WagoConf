﻿using System;
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
        public string AddText
        {
            set
            {
                richTextBox1.AppendText(DateTime.Now.ToString("d.MM.yyyy HH:mm:ss") + " " + value + "\n\r");
            }
        }
        public Log()
        {
            InitializeComponent();
        }
    }
}