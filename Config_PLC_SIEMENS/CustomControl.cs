using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Config_PLC_SIEMENS
{
   public class CustomControl
    {
       public class DigitTextBox : TextBox
        {
            const int ES_NUMBER = 0x2000;
            const int WM_PASTE = 0x0302;
            const string NumberTemplate = @"\d+[.,]\d+";
            const string NumberTemplatePoinDigit = @"[\d,.]";
            const string NumberTemplateDelemiter = @"[,.]";

            public double Value
            {
                get
                {
                    return GetDouble(this.Text);
                }
                set
                {
                    this.Text = value.ToString();
                }
            }
            
            protected override void WndProc(ref Message m)
            {

                if (m.Msg == WM_PASTE)
                {

                    string data = Clipboard.GetDataObject().GetData(DataFormats.Text) as string;

                    if (!Regex.IsMatch(data, NumberTemplate))

                        return;

                }

                base.WndProc(ref m);
            }
            protected override void OnTextChanged(EventArgs e)
            {
                if (this.Text.Length != 0 && !Regex.IsMatch(this.Text[this.Text.Length - 1].ToString(), NumberTemplatePoinDigit))
                {
                    this.Text = this.Text.Substring(0, this.Text.Length - 1);
                    this.Select(this.Text.Length, 1);

                }
                if (this.Text.Length != 0 && Regex.IsMatch(this.Text, NumberTemplateDelemiter))
                {
                    if(CountDeleimiter(this.Text) < 2)
                        return;
                    this.Text = this.Text.Remove(this.Text.Length - 1, 1);
                    this.Select(this.Text.Length, 1);
                }
                base.OnTextChanged(e);
            }
            private int CountDeleimiter(string Text)
            {
                return Text.Count(c => c.ToString() == "." || c.ToString() == ",");

            }
            private double GetDouble(string value)
            {
                double result = 0.0;
                try
                {
                    result = Convert.ToDouble(value.Replace(".", ","));
                }
                catch
                {
                    try
                    {
                        result = Convert.ToDouble(value.Replace(",", "."));
                    }
                    catch
                    {
                        result = 0.0;
                    }
                }
                return result;
            }
           

        }
    }
}
