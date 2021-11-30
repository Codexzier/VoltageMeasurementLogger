using Codexzier.Wpf.ApplicationFramework.Views.Base;
using System.Collections.Generic;
using VoltageMeasurementLogger.UserControls.LineDiagram;

namespace VoltageMeasurementLogger.Views.LogDataResult
{
    internal class LogDataResultViewModel : BaseViewModel
    {
        private string _filename;
        private int _countMeasures;
        private List<LineDiagramLevelItem> _values;
        private double _averageValue;
        private double _minValue;
        private double _maxValue;

        public string Filename
        {
            get => _filename;
            set
            {
                _filename = value;
                this.OnNotifyPropertyChanged(nameof(this.Filename));
            }
        }

        public int CountMeasures
        {
            get => _countMeasures;
            set
            {
                _countMeasures = value;
                this.OnNotifyPropertyChanged(nameof(this.CountMeasures));
            }
        }

        public List<LineDiagramLevelItem> Values
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
    }
}