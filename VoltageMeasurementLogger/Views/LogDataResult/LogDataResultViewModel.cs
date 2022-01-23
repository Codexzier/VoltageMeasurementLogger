using Codexzier.Wpf.ApplicationFramework.Views.Base;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace VoltageMeasurementLogger.Views.LogDataResult
{
    internal class LogDataResultViewModel : BaseViewModel
    {
        private string _filename;
        private int _countMeasures;
        private List<LogValueItem> _values;
        private double _averageValue;
        private double _minValue;
        private double _maxValue;
        private ICommand _commandLastDeviations;
        private ICommand _commandNextDeviations;
        private int _levelItemIndex;
        private double _deviationTolerance = .2;

        public string Filename
        {
            get => _filename;
            set
            {
                _filename = value;
                this.OnNotifyPropertyChanged(nameof(this.Filename));
            }
        }

        public DataGrid DataGridLogResult { get; internal set; }

        public int CountMeasures
        {
            get => _countMeasures;
            set
            {
                _countMeasures = value;
                this.OnNotifyPropertyChanged(nameof(this.CountMeasures));
            }
        }

        public List<LogValueItem> Values
        {
            get => this._values;
            set
            {
                this._values = value;
                this.OnNotifyPropertyChanged(nameof(this.Values));
            }
        }

        public double AverageValue
        {
            get => _averageValue; set
            {
                _averageValue = value;
                this.OnNotifyPropertyChanged(nameof(this.AverageValue));
            }
        }

        public double MinValue
        {
            get => _minValue; set
            {
                _minValue = value;
                this.OnNotifyPropertyChanged(nameof(this.MinValue));
            }
        }

        public double MaxValue
        {
            get => _maxValue; set
            {
                _maxValue = value;
                this.OnNotifyPropertyChanged(nameof(this.MaxValue));
            }
        }

        public ICommand CommandLastDeviations
        {
            get => _commandLastDeviations;
            set
            {
                _commandLastDeviations = value;
                this.OnNotifyPropertyChanged(nameof(this.CommandLastDeviations));
            }
        }

        public ICommand CommandNextDeviations
        {
            get => _commandNextDeviations;
            set
            {
                _commandNextDeviations = value;
                this.OnNotifyPropertyChanged(nameof(this.CommandNextDeviations));
            }
        }

        public int LevelItemIndex
        {
            get => _levelItemIndex;
            set
            {
                _levelItemIndex = value;
                this.OnNotifyPropertyChanged(nameof(this.LevelItemIndex));
            }
        }

        public double DeviationTolerance
        {
            get => _deviationTolerance;
            set
            {
                _deviationTolerance = value;
                this.OnNotifyPropertyChanged(nameof(this.DeviationTolerance));
            }
        }
    }
}