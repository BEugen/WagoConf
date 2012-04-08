using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Config_PLC_SIEMENS
{
    public partial class botPanel : UserControl
    {
        public delegate void EventControl(string nameSCADA, int hov_lev);
        public delegate void EventControlClick(int ch);
        public delegate void ChangeValueD(object sender, ChangeModeArgs e);
        public event ChangeValueD ChangeValue;
       // public event EventControlClick ClickControl;
        public event EventControl SelectControl;
        Color nameTextColor = Color.FromArgb(255, 0xF1, 0xF1, 0xF1);
        Color[] bacStrokeColor;
        Color[] bacFillColor;
        Color albacColor = Color.FromArgb(0xFF, 0xe6, 0x4a, 0x4a);
        Color alTextColor = Color.FromArgb(0xFF, 0, 0, 0);
        Color valbacColor = Color.FromArgb(0xff, 0xc2, 0xc2, 0xc2);
        Color valTextColor = Color.FromArgb(0xFF, 0, 0, 0);
        Color valBorderColor = Color.FromArgb(255, 0, 0, 0);
        Brush canvasColor;
        double bacTicknes = 1;
        double valTicnes = 1;
        rPID _rPID;
        string SelectNameSCADA = "";
        double sp_value = 0.0;
        public botPanel()
        {
            InitializeComponent();
            NameScope.SetNameScope(this.botCanvas, new NameScope());
            //dtTimeUpdate = new DateTime();
            
        }


        public void CreateChanelVisual(rPID PidRegulator)
        { 
            CreateCanvas();
            botCanvas.Children.Clear();
            _rPID = PidRegulator;
            CreateControlAft4El();
        }

        public void UpdateValue(ValuePIDChannel ValuePID)
        {
            foreach (KeyValuePair<string, double> keyPar in ValuePID.TagValue)
            {
                valControl vc = (valControl)this.botCanvas.FindName(keyPar.Key);
                if (vc != null)
                {
                    vc.Status = 0;
                    
                    if ((_rPID.PID_Mode & 0x01) == 0x01 && keyPar.Key == _rPID.atagCV_MANUAL.nameSCADA)
                    {
                        bot_slider bs = (bot_slider)this.botCanvas.FindName("slider");
                        //if (dtTimeUpdate < ValuePID.DateValue)
                       // {
                            bs.Value = keyPar.Value;
                        //}
                    }
                     if ((_rPID.PID_Mode & 0x01) == 0x00 && keyPar.Key == _rPID.atagCV.nameSCADA)
                    {
                        bot_slider bs = (bot_slider)this.botCanvas.FindName("slider");
                        bs.Value = keyPar.Value;
                    }
                    vc.Value = keyPar.Value;
                }
            }
        }

        private void CreateCanvas()
        {
            botCanvas.Width = this.Width;
            botCanvas.Height = this.Height;
           
        }
        
        private void Selectchannel(string nameSCADA)
        {
            valControl vc = (valControl)botCanvas.FindName(nameSCADA);
            if (vc != null)
            {
                Color c = vc.BackgroundFillColor;
                c.A = 5;
                vc.BackgroundFillColor = c;
            }
            vc = (valControl)botCanvas.FindName(nameSCADA);
            if (vc != null)
            {
                Color c1 = vc.BackgroundFillColor;
                c1.A = 125;
                vc.BackgroundFillColor = c1;
                SelectNameSCADA = nameSCADA;
            }
            else
            {
                SelectNameSCADA = "";

            }
        }

        void valC_MouseEnter(object sender, MouseEventArgs e)
        {
            string nameSCADA = "";
            try { nameSCADA =((valControl)sender).Name; }
            catch {nameSCADA = ""; }
            SelectControl(nameSCADA, 1);
            //throw new NotImplementedException();
        }
        void valC_MouseLeave(object sender, MouseEventArgs e)
        {
            string nameSCADA = "";
            try { nameSCADA = ((valControl)sender).Name; }
            catch { nameSCADA = ""; }
            SelectControl(nameSCADA, 0);
            //throw new NotImplementedException();
        }
        void valC_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // int id = -1;
            //try { id = Convert.ToInt32(((valControl)sender).Name.Replace("ch", "")); }
            //catch { id = -1; }
            //ClickControl(id);
        }
       
        private void CreateControlAft4El()
        {
            try
            {
                double left = bacTicknes;
                double top = bacTicknes;
                botCanvas.Children.Clear();
                double h = this.Height / 5 - bacTicknes * 2;
                double w = this.Width / 2 - bacTicknes * 4;
                for (int i = 0; i < 4; i++)
                {
                    if (i % 2 == 0 && i > 1)
                    {
                        top += h + bacTicknes * 0.70;
                        left = bacTicknes;
                    }
                    valControl valC = new valControl();
                    valC.Height = h;
                    valC.Width = w;
                    switch (i)
                    {
                        case 0:
                            if (_rPID.atagPV.id == -1)
                                continue;
                            valC.NameText = _rPID.atagPV.description + " (" + _rPID.atagPV.nameSCADA + ")";
                            valC.Name = _rPID.atagPV.nameSCADA;
                            break;
                        case 1:
                            if (_rPID.atagSP.id == -1)
                                continue;
                            valC.NameText = _rPID.atagSP.description + " (" + _rPID.atagSP.nameSCADA + ")";
                            valC.Name = _rPID.atagSP.nameSCADA;
                            break;
                        case 2:
                            if (_rPID.atagCV.id == -1)
                                continue;
                            valC.NameText = _rPID.atagCV.description + " (" + _rPID.atagCV.nameSCADA + ")";
                            valC.Name = _rPID.atagCV.nameSCADA;
                            break;
                        case 3:
                            if (_rPID.atagCV_MANUAL.id == -1)
                                continue;
                            valC.NameText = _rPID.atagCV_MANUAL.description + " (" + _rPID.atagCV_MANUAL.nameSCADA + ")";
                            valC.Name = _rPID.atagCV_MANUAL.nameSCADA;
                            break;
                        default:
                            valC.NameText = valC.Name = "Нет данных";
                            break;
                    }
                    if (botCanvas.FindName(valC.Name) != null)
                        botCanvas.UnregisterName(valC.Name);
                    botCanvas.RegisterName(valC.Name, valC);
                    valC.AlarmBackColor = albacColor;
                    valC.AlarmTextColor = alTextColor;
                    valC.BackgroundTickness = bacTicknes;
                    valC.BackgroundStrokeColor = bacStrokeColor[i];
                    bacFillColor[i].A = 5;
                    valC.BackgroundFillColor = bacFillColor[i];
                    valC.NameTextColor = nameTextColor;
                    valC.ValueBackColor = valbacColor;
                    valC.ValueBorderColor = valBorderColor;
                    valC.ValueTextColor = valTextColor;
                    valC.ValueTickness = valTicnes;
                    valC.Status = -1;
                    valC.Value = 0.0;
                    valC.EnableAlarm = false;
                    valC.SetComponent();
                    valC.MouseLeave += new MouseEventHandler(valC_MouseLeave);
                    valC.MouseEnter += new MouseEventHandler(valC_MouseEnter);
                    valC.MouseLeftButtonDown += new MouseButtonEventHandler(valC_MouseLeftButtonDown);
                    valC.SetValue(Canvas.TopProperty, top);
                    valC.SetValue(Canvas.LeftProperty, left);
                    left += w + bacTicknes;
                    botCanvas.Children.Add(valC);
                }
                bot_slider bs = new bot_slider();
                bs.Name = "slider";
                if (botCanvas.FindName(bs.Name) != null)
                    botCanvas.UnregisterName(bs.Name);
                botCanvas.RegisterName(bs.Name, bs);
                bs.ChangeValue += new bot_slider.ChangeValueDelegat(bs_ChangeValue);
                bs.AutomaticMode = (_rPID.PID_Mode & 0x01) == 0x01 ? false : true;
                bs.Height = this.Height - top - this.Height * 0.3;
                bs.Width = this.Width;
                bs.SetValue(Canvas.TopProperty, top + this.Height * 0.2);
                bs.SetValue(Canvas.LeftProperty, 0.0);
                botCanvas.Children.Add(bs);
            }
            catch
            {
            }
        }

        void bs_ChangeValue(double Value, bool AutomaticMode)
        {
          // dtTimeUpdate = DateTime.Now.AddSeconds(1.5);
            if (AutomaticMode)
            {
                _rPID.PID_Mode &= 0xFE;
            }
            else
            {
                _rPID.PID_Mode |= 0x01;
            }
            sp_value = Value;
            ChangeModeArgs e = new ChangeModeArgs(Value, 0.0, AutomaticMode);
            if (ChangeValue != null)
                ChangeValue(this, e);

        }
        #region Propertis
        public Brush CanvasColor
        {
            set
            {
                canvasColor = value;
                botCanvas.Background = canvasColor;
            }
            get
            {
                return canvasColor;
            }
        }
        public Color NameTextColor
        {
            set
            {
                nameTextColor = value;
            }
            get
            {
                return nameTextColor;
            }
        }
        public Color[] BackgroundStrokeColor
        {
            set
            {
                bacStrokeColor = value;
            }
            get
            {
                return bacStrokeColor;
            }
        }
        public Color[] BackgroundFillColor
        {
            set
            {
                bacFillColor = value;
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
            }
            get
            {
                return bacTicknes;
            }
        }
        public double Hs
        {
            set
            {
                this.Height = value;
                CreateCanvas();
            }
            get
            {
                return this.Height;
            }
        }
        public double Ws
        {
            set
            {
                this.Width = value;
                CreateCanvas();
            }
            get
            {
                return this.Width;
            }
        }
        public string SelectChannel
        {
            set
            {
                Selectchannel(value);
            }
            get
            {
                return SelectNameSCADA;
            }
        }
        public bool ModePid
        {
            set
            {
                if (value)
                {
                    _rPID.PID_Mode &= 0xFE;
                }
                else
                {
                    _rPID.PID_Mode |= 0x01;
                }
            }
            get
            {
                if ((_rPID.PID_Mode & 0x01) == 0x01) //manual mode;
                    return false;
                else
                    return true;
            }
        }
        public double SP_Manual
        {
            get
            {
                return sp_value;
            }
        }
        #endregion
    }
}
