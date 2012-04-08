using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Configuration;
using System.Reflection;

namespace Config_PLC_SIEMENS
{
    public partial class Trnd : UserControl
    {
        double yAxisM = 10.0;
        double xAxisM = 10.0;
        double topMarning = 10.0;
        double xAxisAfterLine = 7.0;
        double yAxistAfterLine = 5.0;
        double hTiknes = 4.5;
        double lTiknes = 1.5;
        string nameScadaSelect = "";
        int cTimerDev = 0;
        Rectangle rchPane;
        int yLineCount = 11;
        int xLineCount = 11;
        Color colorPaneChart = Color.FromArgb(255, 0, 0, 0);
        System.Windows.Threading.DispatcherTimer tmr;
        System.Windows.Threading.DispatcherTimer tmrControl;
        rPID _rPid;
        ValuePIDChannel _value;
        int _timeIntervalSeconds = 300;
        int _timeSamlesMSeconds = 1000;
        Dictionary<string, ATag> tags;
        List<ValuePIDChannel> values;

        double _sp_Value = 0.0;
        double _cv_Value = 0.0;
        double _cv_MANUAL_Value = 0.0;
        double _pv_Value = 0.0;


        public delegate void ChangeValueD(object sender, ChangeModeArgs e);
        public event ChangeValueD ChangeValue;

        public Trnd()
        {
           
            InitializeComponent();

        
            tmr = new System.Windows.Threading.DispatcherTimer();
            tmrControl = new System.Windows.Threading.DispatcherTimer();
            tmr.Interval = new TimeSpan(0, 0, 0, 0, _timeSamlesMSeconds);
            tmrControl.Interval = new TimeSpan(0, 0, 5);
            tmr.Tick += new EventHandler(tmr_Tick);
            tmrControl.Tick += new EventHandler(tmrControl_Tick);
            NameScope.SetNameScope(this.chPane, new NameScope());
            this.Width = 1000.0;
            this.Height = 350.0;
            _rPid = new rPID(-1);
            _value = new ValuePIDChannel();
            FillTags(_rPid);
            FillValues();
            CreateControl();
            CreateChart();
            tmr.Start();
            tmrControl.Start();

        }

        void tmrControl_Tick(object sender, EventArgs e)
        {
            if (!tmr.IsEnabled)
                cTimerDev++;
            if (cTimerDev > 4)
            {
                tmr.Stop();
                cTimerDev = 0;
            }
        }

        void UpdateValue(ValuePIDChannel value)
        {
            try
            {

                if (_rPid.id != -1)
                {
                    topName.Content= _rPid.namePID + ", " + _rPid.Description;
                    topName.FontSize = GenericMetods.CalculateMaximumFontSize(topName.FontSize, 4, 0.5, topName.Content.ToString(), topName.FontFamily,
                        new Size(topName.Width, topName.Height), new Thickness(1.0), "ru-ru");
                        //FontSizeV(topName.Width, topName.Height, topName.Text.Length);
                }
                else
                {
                    topName.Content = "Регулятор не выбран";
                }
                botControl.UpdateValue(value);
                r_sl.Value = value.TagValue[_rPid.atagSP.nameSCADA];
            }
            catch { }
            cTimerDev = 0;
            tmr.Start();
        }

        void UpdateTrend()
        {
            try
            {
                if (values.Count > 0)
                {
                    double[] d = new double[values.Count];
                    bool flg = false;
                    if (values[values.Count - 1].DateValue > values[0].DateValue)
                    {
                        TimeSpan ts = values[values.Count - 1].DateValue - values[0].DateValue;
                        double dDiff = ts.TotalSeconds / (xLineCount + 1);
                        UpdatexAxistLabel(values[values.Count - 1].DateValue, dDiff);
                        flg = true;
                    }
                    else
                    {
                        TimeSpan ts = values[0].DateValue - values[values.Count - 1].DateValue;
                        double dDiff = ts.TotalSeconds / (xLineCount + 1);
                        UpdatexAxistLabel(values[0].DateValue, dDiff);
                    }
                    int i = 0;
                    foreach(KeyValuePair<string, double> key in values[0].TagValue)
                    {
                        for (int j = 0; j < values.Count; j++)
                        {
                            if (flg)
                            {
                                d[j] = values[values.Count - 1 - j].TagValue[key.Key];
                            }
                            else
                            {
                                d[j] = values[j].TagValue[key.Key];
                            }
                        }
                        object o = chPane.FindName("l" + key.Key);
                        if (o != null)
                        {
                            double max = 0.0;
                            double min = 0.0;
                            try
                            {
                                max = tags[key.Key].EU_MAX;
                                min = tags[key.Key].EU_MIN;
                            }
                            catch { }
                            System.Windows.Shapes.Path p = (System.Windows.Shapes.Path)o;
                            //if (!flgFirstget && jumoDevice.MaxLevel != null && jumoDevice.MinLevel != null &&
                            //    jumoDevice.MaxLevel.Length > 0 && jumoDevice.MinLevel.Length > 0 && jumoDevice.format != null &&
                            //    jumoDevice.format.Length > 0)
                            //{
                            //    UpdateyAxistlabel(new SolidColorBrush(GraphicColor.GetColor(i)), jumoDevice.MaxLevel.ToArray<double>()[0],
                            //        jumoDevice.MinLevel.ToArray<double>()[0], jumoDevice.format.ToArray<string>()[0]);
                            //    flgFirstget = true;
                            //}
                            if (nameScadaSelect == key.Key)
                            {
                                SetTrend(ref p, d.Length, d, hTiknes, GraphicColor.GetColor(i), max, min);
                            }
                            else
                            {
                                SetTrend(ref p, d.Length, d, lTiknes, GraphicColor.GetColor(i++), max, min);
                            }
                        }

                    }


                }
            }
            catch
            { }
            cTimerDev = 0;
        }

        void tmr_Tick(object sender, EventArgs e)
        {
            tmr.Stop();
            try
            {
                if (_value.TagValue != null && _value.TagValue.Count > 0)
                {
                    _value.DateValue = DateTime.Now;
                    AddToValues(_value);
                    UpdateValue(_value);
                    UpdateTrend();
                }
            }
            catch
            {
            }
            tmr.Start();

        }

        private void FillTags(rPID _rPid)
        {
            try
            {
                if (tags == null)
                    tags = new Dictionary<string, ATag>();
                tags.Clear();
                if (_rPid.atagPV.id != -1 && _rPid.atagPV.nameSCADA != "")
                    tags.Add(_rPid.atagPV.nameSCADA, _rPid.atagPV);
                if (_rPid.atagSP.id != -1 && _rPid.atagSP.nameSCADA != "")
                    tags.Add(_rPid.atagSP.nameSCADA, _rPid.atagSP);
                if (_rPid.atagCV.id != -1 && _rPid.atagCV.nameSCADA != "")
                    tags.Add(_rPid.atagCV.nameSCADA, _rPid.atagCV);
                if (_rPid.atagCV_MANUAL.id != -1 && _rPid.atagCV_MANUAL.nameSCADA != "")
                    tags.Add(_rPid.atagCV_MANUAL.nameSCADA, _rPid.atagCV_MANUAL);
            }
            catch { }

        }

        private void FillValues()
        {
            if (values == null)
                values = new List<ValuePIDChannel>();
            values.Clear();
            if (_timeSamlesMSeconds == 0 || tags.Count == 0)
                return;
            int count = _timeIntervalSeconds * 1000 / _timeSamlesMSeconds;
            DateTime dt = DateTime.Now.AddSeconds(-(double)_timeIntervalSeconds);
            ValuePIDChannel v = new ValuePIDChannel();
            v.TagValue = new Dictionary<string, double>();
            foreach (KeyValuePair<string, ATag> key in tags)
            {
                v.TagValue.Add(key.Key, 0.0);
            }
           
            for (int i = 0; i < count; i++)
            {
                v.DateValue = dt;
                values.Add(v);
                dt = dt.AddMilliseconds(_timeSamlesMSeconds);
            } 
            _value = v;
        }

        private void AddToValues(ValuePIDChannel value)
        {
            if (values != null || values.Count > 0)
            {
                values.Add(value);
                values.Remove(values[0]);
            }
        }


        private double GetDouble(string d)
        {
            double dbl = 0.0;
            d = d.Replace(",", ".");
            try
            {
                dbl = Convert.ToDouble(d);
            }
            catch
            {
                d = d.Replace(".", ",");
                try
                {
                    dbl = Convert.ToDouble(d);
                }
                catch
                {
                    dbl = 0.0;
                }
            }
            return dbl;

        }
        private void CreateControl()
        {
            topCanvas.Height = this.Height * 0.05;
            topCanvas.Width = this.Width;
            topRect.Height = topCanvas.Height - topRect.StrokeThickness * 5;
            topRect.Width = topCanvas.Width - topRect.StrokeThickness * 2;
            topName.Height = topRect.Height;// *0.5;
            topName.Width = topRect.Width;// * 0.8;
            topRect.SetValue(Canvas.TopProperty, topRect.StrokeThickness*4);
            topRect.SetValue(Canvas.LeftProperty, topRect.StrokeThickness*4);
            topName.SetValue(Canvas.TopProperty, topRect.StrokeThickness * 4/*topName.Height / 2*/);
            topName.SetValue(Canvas.LeftProperty, topRect.StrokeThickness * 4/*topName.Width / 8*/);
            if (topName.Content.ToString().Length > 0)
                topName.FontSize = GenericMetods.CalculateMaximumFontSize(topName.FontSize, 4, 0.5, topName.Content.ToString(), topName.FontFamily,
                        new Size(topName.Width, topName.Height), new Thickness(1.0), "ru-ru");
                //FontSizeM(topName.Width, topName.Height, topName.Text.Length)*1.2;
            topName.Content= "";
            Color[] c = new Color[4];
            for (int i = 0; i < c.Length; i++)
            {
                c[i] = GraphicColor.GetColor(i);
            }
            botControl.Ws = this.Width;
            botControl.Hs = this.Height * 0.3;// > 250.0 ? 250.0 : this.Height * 0.2;
            botControl.CanvasColor = topRect.Fill;
            botControl.SetValue(StackPanel.VerticalAlignmentProperty, VerticalAlignment.Bottom);
               botControl.SelectControl += new botPanel.EventControl(botControl_SelectControl);
               botControl.ChangeValue += new botPanel.ChangeValueD(botControl_ChangeValue);
            botControl.BackgroundFillColor = c;
            botControl.BackgroundStrokeColor = c;
            botControl.BackgroundTickness = 2.0;
            botControl.AlarmBackColor = Color.FromArgb(255, 255, 0, 0);
            botControl.NameTextColor = Color.FromArgb(255, 0, 0, 0);
            botControl.CreateChanelVisual(_rPid);
        }

        void botControl_ChangeValue(object sender, ChangeModeArgs e)
        {
            e = new ChangeModeArgs(e.CV_Manual, r_sl.Value, e.ModePid);
            if (ChangeValue != null)
                ChangeValue(this, e);
        }

        void botControl_SelectControl(string nameScada, int hov_lev)
        {
            if (nameScada == "")
                return;
            System.Windows.Shapes.Path p = (System.Windows.Shapes.Path)chPane.FindName("l" + nameScada);
            if (p != null)
            {
                
                if (hov_lev == 0)
                {
                    Geometry g = p.Data;
                    p.StrokeThickness = lTiknes;
                    p.SetValue(Canvas.ZIndexProperty, 0);
                    p.Data = g;
                    nameScadaSelect = "";
                    
                }
                else
                {
                    Geometry g = p.Data;
                    p.StrokeThickness = hTiknes;
                    p.SetValue(Canvas.ZIndexProperty, 10);
                    p.Data = g;
                    nameScadaSelect = nameScada;
                    try
                    {
                        double max = tags[nameScada].EU_MAX;
                        double min = tags[nameScada].EU_MIN;
                        string format = "0.00";
                        UpdateyAxistlabel(p.Fill, max, min, format);
                    }
                    catch { }
                    
                }
            }
        }
        private void CreateChart()
        {
            chPane.Children.Clear();
            Canvas canvas = new Canvas();
            SetCanvas();
            foreach(KeyValuePair<string, ATag> tag in tags)
           // for (int i = 0; i < ch.Length; i++)
            {
                System.Windows.Shapes.Path lin1 = new System.Windows.Shapes.Path();
                lin1.MouseEnter += new MouseEventHandler(lin1_MouseEnter);
                lin1.MouseLeave += new MouseEventHandler(lin1_MouseLeave);
                double[] v = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
                if (chPane.FindName("l" + tag.Key) != null)
                    chPane.UnregisterName("l" + tag.Key);
                  chPane.RegisterName("l"+tag.Key, lin1);
                SetTrend(ref lin1, v.Length, v, lTiknes, colorPaneChart, 15.0, 8.0);
                chPane.Children.Add(lin1);
            }
            SetLabel(100, 0);
        }

        void lin1_MouseLeave(object sender, MouseEventArgs e)
        {
            nameScadaSelect = "";
            System.Windows.Shapes.Path p = (System.Windows.Shapes.Path)sender;
            Geometry g = p.Data;
            p.StrokeThickness = lTiknes;
            p.SetValue(Canvas.ZIndexProperty, 0);
            p.Data = g;
            botControl.SelectChannel = "";
        }

        void lin1_MouseEnter(object sender, MouseEventArgs e)
        {
            nameScadaSelect = ((System.Windows.Shapes.Path)sender).Name.Replace("l", "");
            System.Windows.Shapes.Path p = (System.Windows.Shapes.Path)sender;
            Geometry g = p.Data;
            p.StrokeThickness = hTiknes;
            p.SetValue(Canvas.ZIndexProperty, 10);
            p.Data = g;
            try
            {
                double max = tags[ nameScadaSelect].EU_MAX;
                double min = tags[ nameScadaSelect].EU_MIN;
                string format = "0.00";
                UpdateyAxistlabel(p.Fill, max, min, format);
                botControl.SelectChannel = nameScadaSelect;
            }
            catch { }
        }

        private void SetCanvas()
        {
            double xAxisDelen = 0.0;
            double yAxisDelen = 0.0;
            int xLineC = 0;
            int yLineC = 0;
            yAxisM = this.Height * 0.1;
            xAxisM = this.Width * 0.02;
            yLineC = yLineCount - 2;
            xLineC = xLineCount - 2;
            double wC = this.Width;
            double hC = (this.Height - botControl.Height)*0.95; //(botControl.Height == 150.0 ? this.Height - 0.50 * this.Height : this.Height - 0.55 * this.Height);
            chPane.Height = hC;
            chPane.Width = wC;

            rchPane = new Rectangle();
            r_sl = new r_slider(); 
            rchPane.StrokeThickness = 2.0;
            rchPane.Stroke = new SolidColorBrush(colorPaneChart);
            chPane.Children.Add(rchPane);
            rchPane.Height = chPane.Height - (chPane.Height * 0.02 + xAxisAfterLine + xAxisM);

            r_sl.Height = rchPane.Height + 0.0574 * rchPane.Height;
            r_sl.Width = chPane.Width * 0.3056;


            rchPane.Width = chPane.Width - yAxisM*1.5;
            rchPane.SetValue(Canvas.LeftProperty, yAxisM);
            rchPane.SetValue(Canvas.TopProperty, topMarning);
            

            r_sl.SetValue(Canvas.TopProperty, topMarning +rchPane.Height/2  - r_sl.Height/2);
            r_sl.SetValue(Canvas.LeftProperty, yAxisM + rchPane.Width - r_sl.Width + r_sl.Width * 0.06377 + 2.0);
            r_sl.SetValue(Canvas.ZIndexProperty, 15);
            r_sl.Value = 0.0;

            r_sl.ChangeValue += new r_slider.ChangeValueDelegat(r_sl_ChangeValue);
            chPane.Children.Add(r_sl);


            if (rchPane.Height != 0)
            {
                yAxisDelen = rchPane.Height / (yLineC + 1.0);
            }
            if (rchPane.Width != 0)
            {
                xAxisDelen = rchPane.Width / (xLineC + 1.0);
            }
            System.Windows.Shapes.Path xAxistPath = new System.Windows.Shapes.Path();
            xAxistPath.Stroke = new SolidColorBrush(colorPaneChart);
            xAxistPath.StrokeThickness = 0.3;
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = colorPaneChart;
            xAxistPath.Fill = mySolidColorBrush;
            GeometryGroup myGeometryGroup = new GeometryGroup();
           // bool flg = false;
            for (int i = 0; i < xLineC; i++)
            {
                LineGeometry myLineGeometry = new LineGeometry();
                myLineGeometry.StartPoint = new Point(yAxisM + (i + 1) * xAxisDelen, topMarning);
              //  if (flg)
              //  {
               //     myLineGeometry.EndPoint = new Point(yAxisM + (i + 1) * xAxisDelen, rchPane.Height + xAxisAfterLine);
               //     flg = false;
              //  }
              //  else
              //  {
                    myLineGeometry.EndPoint = new Point(yAxisM + (i + 1) * xAxisDelen, rchPane.Height + 2.0 * xAxisAfterLine);
               //     flg = true;
              //  }
                myGeometryGroup.Children.Add(myLineGeometry);
            }
            xAxistPath.Data = myGeometryGroup;
            chPane.Children.Add(xAxistPath);
            System.Windows.Shapes.Path yAxistPath = new System.Windows.Shapes.Path();
            yAxistPath.Stroke = new SolidColorBrush(colorPaneChart);
            yAxistPath.StrokeThickness = 0.3;
            mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = colorPaneChart;
            yAxistPath.Fill = mySolidColorBrush;
            myGeometryGroup = new GeometryGroup();

            for (int i = 0; i < yLineC; i++)
            {
                LineGeometry myLineGeometry = new LineGeometry();
                myLineGeometry.StartPoint = new Point(yAxisM - yAxistAfterLine, topMarning + (i + 1) * yAxisDelen);
                myLineGeometry.EndPoint = new Point(yAxisM + rchPane.Width, topMarning + (i + 1) * yAxisDelen);
                myGeometryGroup.Children.Add(myLineGeometry);
            }
            yAxistPath.Data = myGeometryGroup;
            chPane.Children.Add(yAxistPath);
        }

        void r_sl_ChangeValue(double Value)
        {
            ChangeModeArgs e = new ChangeModeArgs(botControl.SP_Manual, Value,  botControl.ModePid);
            if (ChangeValue != null)
                ChangeValue(this, e);

        }
        private void SetLabel(double defMax, double defMin)
        {
            double xAxisDelen = 0.0;
            double yAxisDelen = 0.0;
            double yAxisStep = 0.0;
            xAxisDelen = rchPane.Width / (xLineCount - 1);
            yAxisDelen = rchPane.Height / (yLineCount - 1);
            if (yLineCount != 0)
            {
                yAxisStep = (defMax - defMin) / (yLineCount - 1);
            }
            for (int i = 1; i < xLineCount; i++)
            {
                TextBlock txb = new TextBlock();
                txb.Name = "xl" + i.ToString();
                if (chPane.FindName(txb.Name) != null)
                    chPane.UnregisterName(txb.Name);
                chPane.RegisterName(txb.Name, txb);
                //txb.Height = chPane.Height * 0.04;
                //txb.Width = chPane.Width * 0.03;
                txb.Height = (xAxisAfterLine + xAxisM) * 0.5;
                txb.Width = xAxisDelen * 0.8;
                txb.Text = "00:00:00";
                txb.FontSize = GenericMetods.CalculateMaximumFontSize(12, 4, 0.5, txb.Text, new System.Windows.Media.FontFamily("Arial Narrow"),
                    new Size(txb.Width, txb.Height), new Thickness(1.0), "ru-ru");//FontSizeM(txb.Width, txb.Height*1.5, txb.Text.Length);

               // txb.SetValue(Canvas.LeftProperty, (yAxisM + rchPane.Width - xAxisDelen * i) - txb.Width);
               // txb.SetValue(Canvas.TopProperty, 2.0 + txb.Height*0.2 + rchPane.Height + xAxisAfterLine);
                txb.SetValue(Canvas.LeftProperty, (yAxisM + rchPane.Width -xAxisDelen * i) - txb.Width/2);
                txb.SetValue(Canvas.TopProperty, 4.0 + txb.Height * 0.2 + rchPane.Height + xAxisAfterLine);
                chPane.Children.Add(txb);
            }
            for (int i = 0; i < yLineCount; i++)
            {
                TextBlock txb = new TextBlock();
                txb.Name = "yl" + i.ToString();
                if (chPane.FindName(txb.Name) != null)
                    chPane.UnregisterName(txb.Name);
                chPane.RegisterName(txb.Name, txb);
                txb.Height = chPane.Height * 0.045;
                txb.Width = chPane.Width * 0.045;
                txb.Text = (defMin + yAxisStep * i).ToString("0.0");
                txb.FontSize = GenericMetods.CalculateMaximumFontSize(12, 4, 0.5, txb.Text, new System.Windows.Media.FontFamily("Arial Narrow"),
                    new Size(txb.Width, txb.Height), new Thickness(1.0), "ru-ru");//FontSizeM(txb.Width, txb.Height, txb.Text.Length);
                txb.TextAlignment = TextAlignment.Center;
                txb.SetValue(Canvas.LeftProperty, yAxisM / 5);
                txb.SetValue(Canvas.TopProperty, topMarning + rchPane.Height - yAxisDelen * i*0.98 - txb.Height);
                chPane.Children.Add(txb);
            }
        }
        private void SetTrend(ref System.Windows.Shapes.Path Trend_p, int x_point_count, double[] y_point, double ticknes, Color ColorLine, double max, double min)
        {
            double xAxisDelen = 0.0;
            double yAxisDelen = 0.0;
            if (max != 0.0 || max - min != 0.0)
            {
                yAxisDelen = rchPane.Height / (max - min);
            }
            else
            {
                yAxisDelen = rchPane.Height;
            }

            if (x_point_count <= xLineCount)
            {

                if (rchPane.Width != 0)
                {
                    xAxisDelen = rchPane.Width / (xLineCount + 2);
                }
            }
            else
            {
                if (rchPane.Width != 0)
                {
                    xAxisDelen = rchPane.Width / (x_point_count - 1);
                }
            }

            //Trend_p.Name = name;
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            Trend_p.StrokeDashCap = PenLineCap.Round;
            Trend_p.StrokeEndLineCap = PenLineCap.Round;
            Trend_p.StrokeStartLineCap = PenLineCap.Round;
            Trend_p.StrokeLineJoin = PenLineJoin.Round;
            Trend_p.Stroke = new SolidColorBrush(ColorLine);
            Trend_p.StrokeThickness = ticknes;
            mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = ColorLine;
            Trend_p.Fill = mySolidColorBrush;
            GeometryGroup myGeometryGroup = new GeometryGroup();
            double y_value = 0.0;
            double y_value1 = 0.0;
            for (int i = 1; i < x_point_count; i++)
            {
                y_value = y_point[i - 1] < min ? 0.0 : y_point[i - 1] - min;
                if (y_value > max - min)
                {
                    y_value = max - min;
                }
                y_value1 = y_point[i] < min ? 0.0 : y_point[i] - min;
                if (y_value1 > max - min)
                {
                    y_value1 = max - min;
                }
                LineGeometry myLineGeometry = new LineGeometry();
                myLineGeometry.StartPoint = new Point(yAxisM + (rchPane.Width - (i - 1) * xAxisDelen), topMarning + rchPane.Height - y_value * yAxisDelen);
                myLineGeometry.EndPoint = new Point(yAxisM + (rchPane.Width - i * xAxisDelen), topMarning + rchPane.Height - y_value1 * yAxisDelen);
                myGeometryGroup.Children.Add(myLineGeometry);
            }
            Trend_p.Data = myGeometryGroup;
        }
        private void UpdatexAxistLabel(DateTime dStart, double C_second)
        {

            for (int i = 1; i < xLineCount; i++)
            {
                dStart = dStart.AddSeconds(-C_second);
                TextBlock txb = (TextBlock)chPane.FindName("xl" + i.ToString());
                if (txb != null)
                {
                    //dStart = dStart.AddSeconds((double)Math.Round((decimal)dStart.Millisecond));
                    txb.Text = dStart.ToString("H:mm:ss");
                }

            }
        }
        private void UpdateyAxistlabel(Brush b, double max, double min, string format)
        {
            double yAxisStep = 0.0;
            if (yLineCount != 0)
            {
                yAxisStep = (max - min) / (yLineCount - 1);
            }
            for (int i = 0; i < yLineCount ; i++)
            {
                TextBlock txb = (TextBlock)chPane.FindName("yl" + i.ToString());
                txb.Foreground = b;
                txb.Text = (min + yAxisStep * i).ToString(format == ""? "0.0" : format);
                txb.FontSize = GenericMetods.CalculateMaximumFontSize(12, 4, 0.5, txb.Text, new System.Windows.Media.FontFamily("Arial Narrow"),
                    new Size(txb.Width, txb.Height), new Thickness(1.0), "ru-ru");
            }
        }

        private double FontSizeM(double w, double h, int l)
        {

            if (l == 1 || l == 2)
                return h / 1.85 * (14.0 / l) * 0.20;
            else if (l > 2 && l <= 3)
                return h / 1.85 * (15.0 / l) * 0.25;
            else if (l > 3 && l < 5)
                return h / 1.85 * (14.0 / l) * 0.40;
            else
                return h / 1.85 * (14.0 / l) * 0.65;
        }
        private double FontSizeV(double w, double h, int l)
        {
            double res = 0.0;
            double hd = 0.0;
            double koeff = 2.933;
            if (l > 10)
                koeff = 0.9;

            if (l != 0)
            {
                res = (h - 1.6) / 0.738;
                hd = (0.193 * res - 0.533);
                res = ((h - hd * 2) - 1.6) / 0.538;

                if (l * (res * 0.492 + 1.933) > w)
                {
                    res = (w / l - koeff) / 0.592;
                }
                return res;

            }
            else
            {
                return 0.0;
            }
        }
        #region Propertis
        public rPID PidRegulator
        {
            get
            {
                return _rPid;
            }
            set
            {
                _rPid = value;
                FillTags(_rPid);
                FillValues();
                botControl.CreateChanelVisual(_rPid);
            }
        }
        public int TimeIntervalSeconds
        {
            set
            {
                _timeIntervalSeconds = value;
                FillValues();
            }
            get
            {
                return _timeIntervalSeconds;
            }
        }
        public double TimeSamlesMSeconds
        {
            get
            {
                return Convert.ToDouble(_timeSamlesMSeconds) / 1000;
                
            }
            set
            {
                _timeSamlesMSeconds = Convert.ToInt32(value * 1000);
                FillValues();
            }
        }
        //public ValuePIDChannel Value
        //{
        //    set
        //    {
        //        _value = value;
        //    }
        //}
        public bool ModePid
        {
            set
            {
                botControl.ModePid = value;
            }
            get
            {
                return botControl.ModePid;
            }
        }

        public double SP_Value
        { 
            get { return _sp_Value; }
            set 
            {
                if (_rPid.atagSP.id != -1 && _rPid.atagSP.namePLC != "")
                {
                    _sp_Value = value;
                    AddValues(_rPid.atagSP.nameSCADA, _sp_Value);

                }
            }
        }
        public double CV_Value
        {
            get { return _cv_Value; }
            set
            {
                if (_rPid.atagCV.id != -1 && _rPid.atagCV.namePLC != "")
                {
                    _cv_Value= value;
                    AddValues(_rPid.atagCV.nameSCADA, _cv_Value);

                }
            }
        }
        public double CV_MANUAL_Value 
        {
            get { return _cv_MANUAL_Value; }
            set
            {
                if (_rPid.atagCV_MANUAL.id != -1 && _rPid.atagCV_MANUAL.namePLC != "")
                {
                    _cv_MANUAL_Value = value;
                    AddValues(_rPid.atagCV_MANUAL.nameSCADA, _cv_MANUAL_Value);

                }
            }
        }
        public double PV_Value
        {
            get { return _pv_Value; }
            set
            {
                if (_rPid.atagPV.id != -1 && _rPid.atagPV.namePLC != "")
                {
                    _pv_Value = value;
                    AddValues(_rPid.atagPV.nameSCADA, _pv_Value);

                }
            }
        }
        #endregion

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = e.NewSize.Width;
            this.Height = e.NewSize.Height;
            chPane.Children.Clear();
            CreateControl();
            CreateChart();
        }

        public void StartAdviseValue()
        {
            tmr.Start();
            tmrControl.Start();
        }
        public void StopAdviseValue()
        {
            tmr.Stop();
            tmrControl.Stop();
        }

        private void AddValues(string ATagName, double Value)
        {
            if (ATagName == "")
                return;
            if (_value.TagValue == null)
                _value.TagValue = new Dictionary<string, double>();
            if (_value.TagValue.ContainsKey(ATagName))
            {
                _value.TagValue[ATagName] = Value;
            }
            else
            {
                _value.TagValue.Add(ATagName, Value);
            }

        }
    }

    public class GraphicColor
    {
        private static byte[] R = { 255,  0,   0,    12,    51,   0,  255, 102,  36, 2,  0,  102, 0,   255, 0 };
        private static byte[] G = { 0,   102, 204,   84,   102,  51,   51,  0,   55, 113, 51, 153, 128, 102, 51 };
        private static byte[] B = { 102,  0,   0,    87,    255, 103,  0,  255,  27, 153, 102, 0,  128, 204, 204 };
        public static Color GetColor(int index)
        {
            int ind = index / 15;
            return Color.FromArgb(255, R[index - 15 * ind], G[index - 15 * ind], B[index - 15 * ind]);
        }
    }
    public static class GenericMetods
    {
        /// <summary>
        /// Calculates a maximum font size that will fit in a given space
        /// </summary>
        /// <param name="maximumFontSize">The maximum (and default) font size</param>
        /// <param name="minimumFontSize">The minimum size the font is capped at</param>
        /// <param name="reductionStep">The step to de-increment font size with. A higher step is less expensive, whereas a lower step sizes font with greater accuracy</param>
        /// <param name="text">The string to measure</param>
        /// <param name="fontFamily">The font family the string provided should be measured in</param>
        /// <param name="containerAreaSize">The total area of the containing area for font placement, specified as a size</param>
        /// <param name="containerInnerMargin">An internal margin to specify the distance the font should keep from the edges of the container</param>
        /// <returns>The caculated maximum font size</returns>
        public static Double CalculateMaximumFontSize(Double maximumFontSize, Double minimumFontSize, Double reductionStep, String text, FontFamily fontFamily, Size containerAreaSize, Thickness containerInnerMargin, string CultureInfo)
        {
            // set fontsize to maimumfont size
            Double fontSize = maximumFontSize;

            // holds formatted text - Culture info is setup for US-Engish, this can be changed for different language sets
            FormattedText formattedText = new FormattedText(text, System.Globalization.CultureInfo.GetCultureInfo(CultureInfo), FlowDirection.LeftToRight, new Typeface(fontFamily.ToString()), fontSize, Brushes.Black);

            // hold the maximum internal space allocation as an absolute value
            Double maximumInternalWidth = containerAreaSize.Width - (containerInnerMargin.Left + containerInnerMargin.Right);

            // if measured font is too big for container size, with regard for internal margin
            if (formattedText.WidthIncludingTrailingWhitespace > maximumInternalWidth)
            {
                do
                {
                    // reduce font size by step
                    fontSize -= reductionStep;

                    // set fontsize ready for re-measure
                    formattedText.SetFontSize(fontSize);
                }
                while ((formattedText.WidthIncludingTrailingWhitespace > maximumInternalWidth) && (fontSize > minimumFontSize));
                
            }
            double maximumInternalHeight = containerAreaSize.Height - (containerInnerMargin.Top + containerInnerMargin.Bottom);
            if (fontSize > maximumInternalHeight)
            {
                 do
                {
                    // reduce font size by step
                    fontSize -= reductionStep;

                    // set fontsize ready for re-measure
                    formattedText.SetFontSize(fontSize);
                }
                 while ((fontSize > maximumInternalHeight) && (fontSize > minimumFontSize));
            }

            // return ammended fontsize
            return fontSize;
        }

        public static double GetEU(double MAX_EU, double MIN_EU, double MAX_RAW, double MIN_RAW, double Value)
        {
            double result = 0.0;
            if (MAX_EU <= MIN_EU)
                return result;
            if (MAX_RAW <= MIN_RAW)
                return result;
            result = (Value - MIN_RAW) * (MAX_EU - MIN_EU) / (MAX_RAW - MIN_RAW) + MIN_EU;
            return result;
        }
        public static double GetRAW(double MAX_EU, double MIN_EU, double MAX_RAW, double MIN_RAW, double Value)
        {
            double result = 0.0;
            if (MAX_EU <= MIN_EU)
                return result;
            if (MAX_RAW <= MIN_RAW)
                return result;
            result = (Value - MIN_EU) * (MAX_RAW - MIN_RAW) / (MAX_EU - MIN_EU) + MIN_RAW;
            return result;
        }
    }
    }
