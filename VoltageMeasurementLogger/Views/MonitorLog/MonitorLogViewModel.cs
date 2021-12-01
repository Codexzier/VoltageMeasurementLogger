using Codexzier.Wpf.ApplicationFramework.Views.Base;
using System.Collections.Generic;
using VoltageMeasurementLogger.UserControls.LineDiagram;

namespace VoltageMeasurementLogger.Views.MonitorLog
{
    public class MonitorLogViewModel : BaseViewModel
    {

        private string _comPortname;
        private int _rawValue;
        private int _minRawValue = 0;
        private int _maxRawValue = 1024;
        private List<LineDiagramLevelItem> _measurementValues;
        private int _measurementValueIndex;
        private float _voltageValue;
        private string _levelLineText = "5.0V";
               

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

        public List<LineDiagramLevelItem> MeasurementValues
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