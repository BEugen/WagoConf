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
	/// Interaction logic for r_slider.xaml
	/// </summary>
	public partial class r_slider : UserControl
	{
        double _maxValue = 100.0;
        double _minValue = 0.0;
        double _currentValue = 50.0;
        DateTime dt;
        public delegate void ChangeValueDelegat(double Value);
        public event ChangeValueDelegat ChangeValue;
		public r_slider()
		{
			this.InitializeComponent();
            dt = DateTime.Now;
			//SetControl();
        }

        private void SetControl()
        {
            #region Slider
            rec_plz.Width = 0.1393 * this.Width;
            rec_plz.Height = 0.0574 * this.Height;
            dCanvas.Height = this.Height;
            dCanvas.Width = this.Width;

            cslider.Height = this.Height;
            cslider.Width = 0.1393 * this.Width - 1.0;

            rec_slider.Height = this.Height - rec_plz.Height;
            rec_slider.Width = 0.05377 * this.Width;
            rec_slider.RadiusX = rec_slider.RadiusY = 0.02 * this.Height;
            rec_plz.SetValue(Canvas.TopProperty, 0.0);
            rec_plz.SetValue(Canvas.LeftProperty, 0.0);
            rec_slider.SetValue(Canvas.LeftProperty, cslider.Width - rec_slider.Width - 1.0);
            rec_slider.SetValue(Canvas.TopProperty, cslider.Height / 2 - rec_slider.Height / 2);
            cslider.SetValue(Canvas.TopProperty, 0.0);
            cslider.SetValue(Canvas.LeftProperty, this.Width - cslider.Width);

            cinp_zad.Width = rtool_bot.Width = rtool_center.Width = rtool_top.Width = 0.8279 * this.Width;
            cinp_zad.Height = rtool_bot.Height = rtool_center.Height = rtool_top.Height = 0.1112 * this.Height;
            cinp_zad.SetValue(Canvas.TopProperty, 0.0);
            cinp_zad.SetValue(Canvas.LeftProperty, this.Width - cslider.Width - cinp_zad.Width - 2.0);
            cinp_zad.Visibility = System.Windows.Visibility.Hidden;
            #endregion
           
            #region Text

            lab_zad.Width = 0.358 * cinp_zad.Width;
            lab_zad.Height = cinp_zad.Height;
            lab_zad.SetValue(Canvas.LeftProperty, 0.1089 * cinp_zad.Width);
            lab_zad.SetValue(Canvas.TopProperty, 0.0);
            lab_zad.FontSize = GenericMetods.CalculateMaximumFontSize(lab_zad.FontSize, 4, 0.5, lab_zad.Content.ToString(), lab_zad.FontFamily,
                    new Size(lab_zad.Width, lab_zad.Height), new Thickness(1.0), "ru-ru");

            inp_zad.Width = 0.27223 * cinp_zad.Width;
            inp_zad.Height = 0.7419 * cinp_zad.Height;
            inp_zad.SetValue(Canvas.LeftProperty, 0.49496 * cinp_zad.Width);
            inp_zad.SetValue(Canvas.TopProperty, cinp_zad.Height/2 - inp_zad.Height/2);
            inp_zad.FontSize = GenericMetods.CalculateMaximumFontSize(inp_zad.FontSize, 4, 0.5, inp_zad.Text, inp_zad.FontFamily,
                    new Size(inp_zad.Width, inp_zad.Height), new Thickness(3.0), "ru-ru");
            #endregion
            SetValue(_currentValue);

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

        private void rec_plz_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                dt = DateTime.Now.AddSeconds(1.5);
                SetPositionSlider(e.GetPosition(cslider).Y);
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
                SetPositionSlider(e.GetPosition(cslider).Y);
        }
        private void SetPositionSlider(double xPosition)
        {
            double _scaleValue = 0.0;
            SelectTool(xPosition);
            xPosition = xPosition - rec_plz.Height/ 2;
            if (xPosition >= 0.0 && xPosition <= (cslider.Height - rec_plz.Height))
            {

                _scaleValue = _maxValue - GenericMetods.GetEU(_maxValue, _minValue, cslider.Height - rec_plz.Height*1.05, rec_plz.Height / 4, xPosition);
                if (_scaleValue >= _maxValue)
                    _scaleValue = _maxValue;
                if (_scaleValue <= _minValue)
                    _scaleValue = _minValue;
                SetValue(_scaleValue);
                rec_plz.SetValue(Canvas.TopProperty, xPosition);
                cinp_zad.SetValue(Canvas.TopProperty, xPosition - cinp_zad.Height / 2 + rec_plz.Height / 2);
                if(xPosition >= cslider.Height - cinp_zad.Height)
                    cinp_zad.SetValue(Canvas.TopProperty, cslider.Height - cinp_zad.Height);
                if (xPosition <= cinp_zad.Height)
                  cinp_zad.SetValue(Canvas.TopProperty, 0.0);

            }
        }
        private void cslider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
                SetPositionSlider(e.GetPosition(cslider).Y);
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
                cinp_zad.Visibility = System.Windows.Visibility.Hidden;
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
            SetGradien(NewValue);
            inp_zad.Text = NewValue.ToString("0.00");
            NewValue = _maxValue - NewValue;        
            rec_plz.SetValue(Canvas.TopProperty, GenericMetods.GetRAW(_maxValue,
                _minValue, cslider.Height - rec_plz.Height*1.05, 0.0, NewValue));
            cinp_zad.SetValue(Canvas.TopProperty, GenericMetods.GetRAW(_maxValue,
                _minValue, cslider.Height - cinp_zad.Height*1.05, 0.0, NewValue));
            SelectTool((double)cinp_zad.GetValue(Canvas.TopProperty));
            if (ChangeValue != null)
                ChangeValue(_currentValue);
        }

        private void SelectTool(double xPosition)
        {
            if (xPosition < cinp_zad.Height / 2)
            {
                rtool_top.Visibility = Visibility.Visible;
                rtool_bot.Visibility = rtool_center.Visibility = Visibility.Hidden;
            }
            if (xPosition >= (cslider.Height - cinp_zad.Height*1.05))
            {
                rtool_bot.Visibility = Visibility.Visible;
                rtool_top.Visibility = rtool_center.Visibility = Visibility.Hidden;
            }
            if (xPosition >= cinp_zad.Height / 2 && xPosition < (cslider.Height - cinp_zad.Height*1.05))
            {
                rtool_center.Visibility = Visibility.Visible;
                rtool_top.Visibility = rtool_bot.Visibility = Visibility.Hidden;
            }
        }
        private void SetGradien(double NewValue)
        {
            double position = GenericMetods.GetEU(1.0, 0.0, _maxValue, _minValue, NewValue);
            if (position < 0.02)
                position *= 1.1;
            LinearGradientBrush lg = new LinearGradientBrush();
            lg.StartPoint = new Point(0.5, 1);
            lg.EndPoint = new Point(0.5, 0);
            lg.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0x00, 0x00, 0x00), position));
            lg.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF), position + 0.01));
            lg.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0x0D, 0x6D, 0x29), 0.0));
            if ((position -= 0.01) < 0)
                position = 0.0;
            lg.GradientStops.Add(new GradientStop(Color.FromArgb(0xFF, 0x4C, 0xF6, 0x04), position));

                rec_slider.Fill = lg;
        }

        private void rec_plz_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
                cinp_zad.Visibility = System.Windows.Visibility.Hidden;      
        }

        private void rec_plz_LostFocus(object sender, RoutedEventArgs e)
        {
            cinp_zad.Visibility = System.Windows.Visibility.Hidden;
        }

        private void rec_plz_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
           
              cinp_zad.Visibility = System.Windows.Visibility.Visible;
        }

  }
}