using Codexzier.Wpf.ApplicationFramework.Controls.Diagram;
using Codexzier.Wpf.ApplicationFramework.Views.Base;
using System;
using System.Collections.Generic;
using System.Timers;

namespace VoltageMeasurementLogger.Views.MonitorLog
{
    public class MonitorLogViewModel : BaseViewModel
    {
        private readonly Timer _timer = new();
        private readonly Random _random = new();

        private string _comPortname;
        private int _rawValue;
        private int _minRawValue = 0;
        private int _maxRawValue = 1024;
        private List<DiagramLevelItem> _measurementValues;

        public MonitorLogViewModel()
        {
            var diagramLevelItemsSource = new List<DiagramLevelItem>();

            for (int i = 0; i < 200; i++)
            {
                diagramLevelItemsSource.Add(new DiagramLevelItem
                {
                    Value = this._random.NextDouble() * 5d,
                    SetColor = 1,
                    SetHighlightMark = false,
                    ToolTipText = $"Value: {i}"
                });
            }

            this.MeasurementValues = diagramLevelItemsSource;

            this._timer.Interval = 10;
            this._timer.Elapsed += this._timer_Elapsed;
            this._timer.Start();
        }

        private int _index = 0;
        private int _measurementValueIndex;
        private double _startValue = .1;
        private double _applitude = 50;
        private double _periodTime = 10;
        private float _voltageValue;
        private string _levelLineText = "5.0V";

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (this._index < this.MeasurementValues.Count)
            {
                this.MeasurementValues[this._index].Value = this._voltageValue; // * 20.0; // (Math.Sin(this._startValue + (this._index / _periodTime)) * this._applitude) + 100;
                this.MeasurementValueIndex = this._index;
                this._index++;
            }
            else
            {
                this._startValue = this._random.NextDouble() * 50;
                this._index = 0;
            }
        }

        public string ComPortname
        {
            get => this._comPortname;
            set
            {
                this._comPortname = value;
                this.OnNotifyPropertyChanged(nameof(this.ComPortname));
            }
        }

        public int RawValue
        {
            get => this._rawValue;
            set
            {
                this._rawValue = value;
                //this.RenewMinMax();
                this.OnNotifyPropertyChanged(nameof(this.RawValue));
            }
        }

        private void RenewMinMax()
        {
            if (this._rawValue < this._minRawValue)
            {
                this.MinRawValue = this._rawValue;
            }

            if (this._rawValue > this._maxRawValue)
            {
                this.MaxRawValue = this._rawValue;
            }
        }

        public int MinRawValue
        {
            get => this._minRawValue;
            set
            {
                this._minRawValue = value;
                this.OnNotifyPropertyChanged(nameof(this.MinRawValue));
            }
        }

        public int MaxRawValue
        {
            get => this._maxRawValue;
            set
            {
                this._maxRawValue = value;
                this.OnNotifyPropertyChanged(nameof(this.MaxRawValue));
            }
        }

        public List<DiagramLevelItem> MeasurementValues
        {
            get => this._measurementValues;
            set
            {
                this._measurementValues = value;
                this.OnNotifyPropertyChanged(nameof(this.MeasurementValues));
            }
        }

        public int MeasurementValueIndex
        {
            get => this._measurementValueIndex;
            set
            {
                this._measurementValueIndex = value;
                this.OnNotifyPropertyChanged(nameof(this.MeasurementValueIndex));
            }
        }

        public float VoltageValue
        {
            get => this._voltageValue;
            set
            {
                this._voltageValue = value;
                this.OnNotifyPropertyChanged(nameof(this.VoltageValue));
            }
        }

        public string LevelLineText
        {
            get => _levelLineText;
            set
            {
                _levelLineText = value;
                this.OnNotifyPropertyChanged(nameof(this.LevelLineText));
            }
        }
    }
}