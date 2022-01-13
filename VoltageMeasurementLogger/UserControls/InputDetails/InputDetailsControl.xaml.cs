using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VoltageMeasurementLogger.UserControls.InputDetails
{
    /// <summary>
    /// Interaction logic for InputDetailsControl.xaml
    /// </summary>
    public partial class InputDetailsControl : UserControl
    {


        public string InputName
        {
            get => (string)this.GetValue(InputNameProperty);
            set => this.SetValue(InputNameProperty, value);
        }

        public static readonly DependencyProperty InputNameProperty =
            DependencyProperty.RegisterAttached(
                "InputName",
                typeof(string),
                typeof(InputDetailsControl),
                new PropertyMetadata("input name", UpdateInputName));

        private static void UpdateInputName(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            if (d is InputDetailsControl control)
            {
                control.TextBlockInputName.Text = control.InputName;
            }
        }

        public int RawValue
        {
            get => (int)this.GetValue(RawValueProperty);
            set => this.SetValue(RawValueProperty, value);
        }

        public static readonly DependencyProperty RawValueProperty =
            DependencyProperty.RegisterAttached(
                "RawValue",
                typeof(int),
                typeof(InputDetailsControl),
                new PropertyMetadata(2, UpdateDetails));

        public float ResultValue
        {
            get => (float)this.GetValue(ResultValueProperty);
            set => this.SetValue(ResultValueProperty, value);
        }

        public static readonly DependencyProperty ResultValueProperty =
            DependencyProperty.RegisterAttached(
                "ResultValue",
                typeof(float),
                typeof(InputDetailsControl),
                new PropertyMetadata(0f, UpdateDetails));

        private int _lastRawValue;
        private float _lastResult;

        private int _rawValueMin = int.MaxValue;
        private int _rawValueMax = int.MinValue;

        private float _resultValueMin = float.MaxValue;
        private float _resultValueMax = float.MinValue;

        private static void UpdateDetails(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            if (d is InputDetailsControl control)
            {
                var rawValue = control.RawValue;
                var resultValue = control.ResultValue;

                if (!rawValue.Equals(control._lastRawValue))
                {
                    control._lastRawValue = rawValue;

                    if(rawValue < control._rawValueMin)
                    {
                        control._rawValueMin = rawValue;
                    }

                    if(rawValue > control._rawValueMax)
                    {
                        control._rawValueMax = rawValue;
                    }

                    control.TextBlockRawOrResult.Text = "Raw";
                    control.TextBlockRawValue.Text = $"{rawValue}";
                    control.TextBlockRawValueMin.Text = $"{control._rawValueMin}";
                    control.TextBlockRawValueMax.Text = $"{control._rawValueMax}";
                }
                else if (!resultValue.Equals(control._lastResult))
                {
                    control._lastResult = resultValue;

                    if(resultValue < control._resultValueMin)
                    {
                        control._resultValueMin = resultValue;
                    }

                    if(resultValue > control._resultValueMax || float.IsInfinity(control._resultValueMax))
                    {
                        control._resultValueMax = resultValue;
                    }

                    control.TextBlockRawOrResult.Text = "Voltage";
                    control.TextBlockRawValue.Text = $"{resultValue:N2}";
                    control.TextBlockRawValueMin.Text = $"{control._resultValueMin:N2}";
                    control.TextBlockRawValueMax.Text = $"{control._resultValueMax:N2}";
                }

            }
        }

        public void ResetMinAndMaxValues()
        {
            this._resultValueMin = float.MaxValue;
            this._resultValueMax = float.MinValue;
            this._rawValueMin = int.MaxValue;
            this._rawValueMax = int.MinValue;
        }

        public InputDetailsControl()
        {
            InitializeComponent();
        }
    }
}
