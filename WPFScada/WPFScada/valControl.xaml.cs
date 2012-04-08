using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Config_PLC_SIEMENS
{
	public partial class valControl : UserControl
	{
        Color nameTextColor = Color.FromArgb(255, 0xF1, 0xF1, 0xF1);
        Color bacStrokeColor = Color.FromArgb(255, 0x9D, 0x2d, 0x2d);
        Color bacFillColor = Color.FromArgb(0x0d, 0x9d, 0x2d, 0x2d);
        Color albacColor = Color.FromArgb(0xFF, 0xe6, 0x4a, 0x4a);
        Color alTextColor = Color.FromArgb(0xFF, 0, 0, 0);
        Color valbacColor = Color.FromArgb(0xff, 0xc2, 0xc2, 0xc2);
        Color valTextColor = Color.FromArgb(0xFF, 0, 0, 0);
        Color valBorderColor = Color.FromArgb(255, 0, 0, 0);
        double bacTicknes = 2;
        double valTicnes = 3;
        double val = 0.0;
        int alarm = 0;
        int status = -1;
        bool enAlarm = true;
        string nameString = "";
        string valFormat = "0.0";

		public valControl()
		{
			// Required to initialize variables
			InitializeComponent();
            //SetComponent();
		}

        public void SetComponent()
        {
            dCanvas.Height = this.Height;
            dCanvas.Width = this.Width;
            anPane.Width = this.Width;
            anPane.Height = this.Height;
            valText.Height = this.Height - anPane.StrokeThickness;
            valText.Width = this.Width * 0.20;
           // anPane.SetValue(Canvas.ZIndexProperty, 10);
            valText.SetValue(Canvas.TopProperty, anPane.StrokeThickness);
            valText.SetValue(Canvas.LeftProperty, this.Width - valText.Width);
            valText.FontSize = FontSizeM(valText.Width, valText.Height, valText.Text.Length);
            valText.VerticalContentAlignment = VerticalAlignment.Center;         
            nametext.Height = this.Height - anPane.StrokeThickness;
            nametext.FontSize = FontSizeM(nametext.Width, nametext.Height, nametext.Text.Length);
            nametext.Width = this.Width - valText.Width;
            nametext.SetValue(Canvas.LeftProperty, 0.5);
            nametext.SetValue(Canvas.TopProperty, nametext.Height/3);

            anPane.MouseEnter += new MouseEventHandler(anPane_MouseEnter);
            anPane.MouseLeave += new MouseEventHandler(anPane_MouseLeave);
            nametext.MouseEnter += new MouseEventHandler(anPane_MouseEnter);
            nametext.MouseLeave += new MouseEventHandler(anPane_MouseLeave);
        }

        void anPane_MouseLeave(object sender, MouseEventArgs e)
        {
            bacFillColor.A = 5;
            anPane.Fill = new SolidColorBrush(bacFillColor);
        }

        void anPane_MouseEnter(object sender, MouseEventArgs e)
        {
            bacFillColor.A = 125;
            anPane.Fill = new SolidColorBrush(bacFillColor);
        }

        private void SetVal()
        {
             if (enAlarm && alarm != 0)
                {
                    if (alarm == -1)
                    {
                        valText.Text = "\u25bc " + val.ToString(valFormat == "" ? "0.00" : valFormat);   
                    }
                    if(alarm == 1)
                    {
                        valText.Text = "\u25b2 " + val.ToString(valFormat == "" ? "0.00" : valFormat);
                    }
                    SetAlarmColor();
                }
                else
                {
                    valText.Text = val.ToString(valFormat == "" ? "0.00" : valFormat);
                    SetValColor();
                }
                switch(alarm)
                {
                    case 3:
                    valText.Text = "-----";
                    SetAlarmColor();
                    break;
                    case 2:
                    valText.Text ="\u25b2\u25b2\u25b2";
                    SetAlarmColor();
                    break;
                    case -2:
                     valText.Text ="\u25bc\u25bc\u25bc";
                     SetAlarmColor();
                    break;
                    case 4:
                    valText.Text = "????";
                    SetAlarmColor();
                    break;
                    default:
                    break;
                }
            if (status == -1)
            {
                valText.Text = "????";
                SetAlarmColor();
            }
            valText.FontSize = GenericMetods.CalculateMaximumFontSize(valText.FontSize, 4.0, 0.5, valText.Text,
                valText.FontFamily, new Size(valText.Width, valText.Height), valText.BorderThickness, "ru-ru");//
               // FontSizeM(valText.Width, valText.Height, valText.Text.Length);
        }

        private void SetAlarmColor()
        {
            valText.Background = new SolidColorBrush(albacColor);
            valText.Foreground = new SolidColorBrush(alTextColor);
        }
        private void SetValColor()
        {
            valText.Background = new SolidColorBrush(valbacColor);
            valText.Foreground = new SolidColorBrush(valTextColor);
        }

        private double FontSizeM(double w, double h, int l)
        {
            double res = 0.0;
            double hd= 0.0;
            double koeff = 2.933;
            if(l > 10)
                koeff = 0.9;

            if (l != 0)
            {
                res = (h - 1.6) / 0.738;
                hd = (0.193 * res - 0.533);
                res = ((h -hd*2) - 1.6) / 0.858;

                if (l * (res * 0.492 + 1.933) > w)
                {
                    res = (w / l - koeff) / 0.512;
                }
                
                //if ((h  - (0.193*res - 0.533)) < ((res  - 1.6)/ 0.738))
                //{
                    
                //    res = (h - 1.6) / 0.738;
                    
                //}
                return res;

            }
            else
            {
                return 0.0;
            }
            //if (l == 1 || l == 2)
            //    return h / 1.8 * (5.0 / l) * 0.20;
            //else if (l > 2 && l <= 3)
            //    return h / 1.8 * (5.5 / l) * 0.85;
            //else if (l > 2 && l < 5)
            //    return h / 1.8 * (6.0 / l) * 0.90;
            //else if (l >= 5 && l < 10)
            //    return h / 1.8 * (9.0 / l) * 0.90 * w/5;
            //else
            //    return h / 1.45 * (9.0 / l) * 0.90;
        }
        #region Propertis
        public Color NameTextColor
        {
            set
            {
                nameTextColor = value;
                nametext.Foreground = new SolidColorBrush(nameTextColor);
            }
            get
            {
                return nameTextColor;
            }
        }
        public Color BackgroundStrokeColor
        {
            set
            {
                bacStrokeColor = value;
                anPane.Stroke = new SolidColorBrush(bacStrokeColor);
            }
            get
            {
                return bacStrokeColor;
            }
        }
        public Color BackgroundFillColor
        {
            set
            {
                bacFillColor = value;
                anPane.Fill = new SolidColorBrush(bacFillColor);
            }
            get
            {
                return bacFillColor;
            }
        }
        public Color AlarmBackColor
        {
            set
            {
                albacColor = value;
            }
            get
            {
                return albacColor;
            }
        }
        public Color AlarmTextColor
        {
            set
            {
                alTextColor = value;
            }
            get
            {
                return alTextColor;
            }
        }
        public Color ValueBackColor
        {
            set
            {
                valbacColor = value;
                valText.Background = new SolidColorBrush(valbacColor);
            }
            get
            {
                return valbacColor;
            }
        }
        public Color ValueTextColor
        {
            set
            {
                valTextColor = value;
                valText.Foreground = new SolidColorBrush(valTextColor);
            }
            get
            {
                return valTextColor;
            }
        }
        public Color ValueBorderColor
        {
            set
            {
                valBorderColor = value;
                valText.BorderBrush = new SolidColorBrush(valBorderColor);
            }
            get
            {
                return valBorderColor;
            }
        }
        public double ValueTickness
        {
            set
            {
                valTicnes = value;
                valText.BorderThickness = new Thickness(valTicnes);
            }
            get
            {
                return valTicnes;
            }
        }
        public double BackgroundTickness
        {
            set
            {
                bacTicknes = value;
                anPane.StrokeThickness = bacTicknes;
            }
            get
            {
                return bacTicknes;
            }
        }
        public double Value
        {
            set
            {
                val = value;
                SetVal();
            }
            get
            {
                return val;
            }
        }
        public int Alarm
        {
            set
            {
                alarm = value;
            }
            get
            {
                return alarm;
            }
        }
        public int Status
        {
            set
            {
                status = value;
            }
            get
            {
                return status;
            }
        }
        public bool EnableAlarm
        {
            set
            {
                enAlarm = value;
            }
            get
            {
                return enAlarm;
            }
        }
        public string NameText
        {
            set
            {
                nameString = value;
                nametext.Text = nameString + " ";
                nametext.FontSize = GenericMetods.CalculateMaximumFontSize(nametext.FontSize,
                    4.0, 0.5, nametext.Text, nametext.FontFamily, new Size(nametext.Width, nametext.Height),
                    new Thickness(1.0), "ru-ru");
                    //FontSizeM(nametext.Width, nametext.Height, nametext.Text.Length);
            }
            get
            {
                return nameString;
            }
        }
        public string ValueFormat
        {
            set
            {
                valFormat = value;
            }
            get
            {
                return valFormat;
            }
        }
        #endregion
    }
}