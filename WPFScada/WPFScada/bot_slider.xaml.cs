using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Config_PLC_SIEMENS
{
	/// <summary>
	/// Interaction logic for bot_slider.xaml
	/// </summary>
	public partial class bot_slider : UserControl
	{
        double _maxValue = 100.0;
        double _minValue = 0.0;
        double _currentValue = 50.0;
        bool _aumaticMode = false;
        DateTime dt;
        public delegate void ChangeValueDelegat(double Value, bool AutomaticMode);
        public event ChangeValueDelegat ChangeValue;
		public bot_slider()
		{
			this.InitializeComponent();
            dt = DateTime.Now;
            SetControl();
		}

        private void SetControl()
        {
            #region Slider
            rec_plz.Width = 0.0205 * this.Width;

            cslider.Height = rec_plz.Height = 0.4167 * this.Height;
            cslider.Width =  0.6375 * this.Width - 1.0;
            rec_slider.Height = 0.2083 * this.Height;
            rec_slider.Width = 0.6375 * this.Width;
            rec_slider.RadiusX = rec_slider.RadiusY = 0.1 * this.Height;
            rec_plz.SetValue(Canvas.TopProperty, 0.0);
            rec_plz.SetValue(Canvas.LeftProperty, 0.0);
            rec_slider.SetValue(Canvas.LeftProperty, 1.0);
            rec_slider.SetValue(Canvas.TopProperty, cslider.Height / 2 - rec_slider.Height / 2);
            cslider.SetValue(Canvas.TopProperty, 0.5417 * this.Height);
            cslider.SetValue(Canvas.LeftProperty, 0.08125 * this.Width);
            #endregion
            #region Buttons
            button_auto.Width = auto.Width = text_auto.Width =
                button_manual.Width = manual.Width = text_manual.Width = 0.18125 * this.Width;
            button_auto.Height = auto.Height = text_auto.Height =
                button_manual.Height = manual.Height = text_manual.Height = 0.3083 * this.Height;
            auto.RadiusX = auto.RadiusY = manual.RadiusX = manual.RadiusY =0.08335 * this.Height;

            button_manual.SetValue(Rectangle.RadiusXProperty, 0.1667 * this.Height);
            button_manual.SetValue(Rectangle.RadiusYProperty, 0.1667 * this.Height);

            button_auto.SetValue(Canvas.LeftProperty, 0.80625 * this.Width);
            button_auto.SetValue(Canvas.TopProperty, 0.2 * this.Height);
            button_manual.SetValue(Canvas.LeftProperty, 0.80625 * this.Width);
            button_manual.SetValue(Canvas.TopProperty, 0.5833 * this.Height);
            text_auto.FontSize = GenericMetods.CalculateMaximumFontSize(text_auto.FontSize, 4, 0.5, text_auto.Content.ToString(), text_auto.FontFamily,
                    new Size(text_auto.Width, text_auto.Height), new Thickness(2.0), "ru-ru");
            text_manual.FontSize = GenericMetods.CalculateMaximumFontSize(text_manual.FontSize, 4, 0.5, text_manual.Content.ToString(), text_manual.FontFamily,
                    new Size(text_manual.Width, text_manual.Height), new Thickness(2.0), "ru-ru");
            #endregion
            #region Text
            label_0.Width = 0.09 * this.Width;
            label_0.Height = 0.3167 * this.Height;
            label_0.SetValue(Canvas.LeftProperty,/* 0.0525 * this.Width*/  (double)cslider.GetValue(Canvas.LeftProperty) - label_0.Width / 2 + rec_plz.Width/2);
            label_0.SetValue(Canvas.TopProperty, 0.2 * this.Height);
            label_0.FontSize = GenericMetods.CalculateMaximumFontSize(label_0.FontSize, 4, 0.5, label_0.Content.ToString(), label_0.FontFamily,
                    new Size(label_0.Width, label_0.Height), new Thickness(1.0), "ru-ru");

            label_100.Width = 0.09 * this.Width;
            label_100.Height = 0.3167 * this.Height;
            label_100.SetValue(Canvas.LeftProperty, 0.66 * this.Width);
            label_100.SetValue(Canvas.TopProperty, 0.2 * this.Height);
            label_100.FontSize = GenericMetods.CalculateMaximumFontSize(label_100.FontSize, 4, 0.5, label_100.Content.ToString(), label_100.FontFamily,
                    new Size(label_100.Width, label_100.Height), new Thickness(1.0), "ru-ru");

            lab_zad.Width = 0.1525 * this.Width;
            lab_zad.Height = 0.3167 * this.Height;
            lab_zad.SetValue(Canvas.LeftProperty, 0.22 * this.Width);
            lab_zad.SetValue(Canvas.TopProperty, 0.2 * this.Height);
            lab_zad.FontSize = GenericMetods.CalculateMaximumFontSize(lab_zad.FontSize, 4, 0.5, lab_zad.Content.ToString(), lab_zad.FontFamily,
                    new Size(lab_zad.Width, lab_zad.Height), new Thickness(1.0), "ru-ru");

            inp_zad.Width = 0.115* this.Width;
            inp_zad.Height = 0.3083 * this.Height;
            inp_zad.SetValue(Canvas.LeftProperty, 0.3775 * this.Width);
            inp_zad.SetValue(Canvas.TopProperty, 0.2 * this.Height);
            inp_zad.FontSize = GenericMetods.CalculateMaximumFontSize(inp_zad.FontSize, 4, 0.5, inp_zad.Text, inp_zad.FontFamily,
                    new Size(inp_zad.Width, inp_zad.Height), new Thickness(3.0), "ru-ru");
            #endregion
            //SetValue(_currentValue);

        }

        public double MaxValue
        {
            get
            {
                return _maxValue;
            }
            set
            {
                _maxValue = value;
                SetValue(_currentValue);
            }
        }
        public double MinValue
        {
            get
            {
                return _minValue;
            }
            set
            {
                _minValue = value;
                SetValue(_currentValue);
            }
        }
        public double Value
        {
            get
            {
                return _currentValue;
            }
            set
            {
                _currentValue = value;
                if (dt < DateTime.Now)
                    SetValue(_currentValue);
            }
        }
        public bool AutomaticMode
        {
            set
            {
                _aumaticMode = value;
                ManualAutoMode(_aumaticMode);
                SetValue(_currentValue);
            }
            get
            {
                return _aumaticMode;
            }
        }

        private void rec_plz_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                dt = DateTime.Now.AddSeconds(1.5);
                SetPositionSlider(e.GetPosition(cslider).X);
            }
        }
        private void dCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = e.NewSize.Width;
            this.Height = e.NewSize.Height;
            SetControl();
        }
        private void rec_slider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!_aumaticMode)
            SetPositionSlider(e.GetPosition(cslider).X);
        }
        private void SetPositionSlider(double xPosition)
        {
            double _scaleValue = 0.0;
            xPosition = xPosition - rec_plz.Width / 2;
            if (xPosition >= 0.0 && xPosition <= (cslider.Width - rec_plz.Width / 2))
            {
                
                  _scaleValue = GenericMetods.GetEU(_maxValue, _minValue, cslider.Width - rec_plz.Width / 1.5, rec_plz.Width / 4, xPosition);
                if (_scaleValue >= _maxValue)
                    _scaleValue = _maxValue;
                if (_scaleValue <= _minValue)
                    _scaleValue = _minValue;
                SetValue(_scaleValue);
                rec_plz.SetValue(Canvas.LeftProperty, xPosition);
            }
        }
        private void cslider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(!_aumaticMode)
            SetPositionSlider(e.GetPosition(cslider).X);
        }
        private void ManualAutoMode(bool AutoMode)
        { 
            LinearGradientBrush lg_m = new LinearGradientBrush();
            LinearGradientBrush lg_a = new LinearGradientBrush();
            if (AutoMode)
            {
               rec_plz.Visibility = Visibility.Hidden;
               inp_zad.IsReadOnly = true;
               lg_m.StartPoint = new Point(0.504, 1);
               lg_m.EndPoint = new Point(0.503, 0);
               lg_m.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0x79, 0x79, 0x79), 0.0));
               lg_m.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0xCA, 0xC8, 0xC8), 0.16));
               lg_m.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0xE5, 0xE4, 0xE4), 0.50));
               lg_m.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0xCA, 0xC8, 0xC8), 0.81));
               lg_m.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0x79, 0x79, 0x79), 1.0));

               lg_a.StartPoint = new Point(0.504, 1);
               lg_a.EndPoint = new Point(0.503, 0);
               lg_a.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0x0F, 0x71, 0x28), 0.0));
               lg_a.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0x4B, 0xF4, 0x05), 0.16));
               lg_a.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0xD3, 0xFE, 0xC1), 0.50));
               lg_a.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0x4B, 0xF4, 0x05), 0.81));
               lg_a.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0x0E, 0x70, 0x28), 1.0));
               manual.Fill = lg_m;
               auto.Fill = lg_a;
                
            }
            else
            {
                rec_plz.Visibility = Visibility.Visible;
                inp_zad.IsReadOnly = false;
                lg_a.StartPoint = new Point(0.504, 1);
                lg_a.EndPoint = new Point(0.503, 0);
                lg_a.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0x79, 0x79, 0x79), 0.0));
                lg_a.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0xCA, 0xC8, 0xC8), 0.16));
                lg_a.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0xE5, 0xE4, 0xE4), 0.50));
                lg_a.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0xCA, 0xC8, 0xC8), 0.81));
                lg_a.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0x79, 0x79, 0x79), 1.0));

                lg_m.StartPoint = new Point(0.504, 1);
                lg_m.EndPoint = new Point(0.503, 0);
                lg_m.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0x62, 0x80, 0xF4), 0.0));
                lg_m.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0x7C, 0x96, 0xF9), 0.16));
                lg_m.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0xD4, 0xDB, 0xF3), 0.50));
                lg_m.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0x7C, 0x96, 0xF9), 0.81));
                lg_m.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0x62, 0x80, 0xF4), 1.0));
                manual.Fill = lg_m;
                auto.Fill = lg_a;
            }
        }
        private void inp_zad_KeyDown(object sender, KeyEventArgs e)
        {

            dt = DateTime.Now.AddSeconds(1.5);
            if (e.Key == Key.Return || e.Key == Key.Enter || e.Key == Key.Space)
            {
                double _newValue = 0.0;
               
                try
                {
                    _newValue = Convert.ToDouble(inp_zad.Text);
                }
                catch { }
                SetValue(_newValue);
            }
        }
        private void SetValue(double NewValue)
        {
            dt = DateTime.Now.AddSeconds(1.5);
            if (NewValue > _maxValue)
                NewValue = _maxValue;
            if (NewValue < _minValue)
                NewValue = _minValue;
            _currentValue = NewValue;
            inp_zad.Text = NewValue.ToString("0.00");            
                rec_plz.SetValue(Canvas.LeftProperty, GenericMetods.GetRAW(_maxValue,
                    _minValue, cslider.Width - rec_plz.Width / 1.5, 0.0, NewValue));
                SetGradien(NewValue);
                if (ChangeValue != null)
                    ChangeValue(_currentValue, _aumaticMode);
        }
        private void SetGradien(double NewValue)
        {
            double position = GenericMetods.GetEU(1.0, 0.0, _maxValue, _minValue, NewValue);
            if (position < 0.20)
                position *= 1.1;
            if (position < 0.02)
                position *= 1.1;
            LinearGradientBrush lg = new LinearGradientBrush();
            if (!_aumaticMode)
            {

                lg.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0x62, 0x80, 0xF4), position));

                lg.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF), position + 0.01));
                lg.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0x62, 0x80, 0xF4), 0.0));
                if ((position -= 0.01) < 0)
                    position = 0.0;
                lg.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0x62, 0x80, 0xF4), position));
            }
            else
            {
                lg.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0x00, 0x00, 0x00), position));
                lg.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF), position + 0.01));
                lg.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0x0D, 0x6D, 0x29), 0.0));
                if ((position -= 0.01) < 0)
                    position = 0.0;
                lg.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0x4C, 0xF6, 0x04), position));
            }
            rec_slider.Fill = lg;
        }

        private void text_auto_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!_aumaticMode)
            {
                _aumaticMode = true;
                ManualAutoMode(_aumaticMode);
                SetValue(_currentValue);
            }
        }

        private void text_manual_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_aumaticMode)
            {
                _aumaticMode = false;
                ManualAutoMode(_aumaticMode);
                SetValue(_currentValue);
            }
        }
	}
}