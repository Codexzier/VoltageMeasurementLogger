using Codexzier.Wpf.ApplicationFramework.Views.Base;
using System.Collections.Generic;
using VoltageMeasurementLogger.UserControls.LineDiagram;

namespace VoltageMeasurementLogger.Views.MonitorLog
{
    public class MonitorLogViewModel : BaseViewModel
    {

        private string _comPortname;

        private int _minRawValue = 0;
        private int _maxRawValue = 1024;
        private List<LineDiagramLevelItem> _measurementValues1;
        private int _measurementValueIndex;
        private List<LineDiagramLevelItem> _measurementValues2;
        private List<LineDiagramLevelItem> _measurementValues3;
        private List<LineDiagramLevelItem> _measurementValues4;
        //private float _voltageValue;
        private string _levelLineText = "5.0V";
        private int _rawValue1;
        private int _rawValue2;
        private int _rawValue3;
        private int _rawValue4;
        private float _resultValue1 = 0.0f;
        private float _resultValue2 = 0.0f;
        private float _resultValue3 = 0.0f;
        private float _resultValue4 = 0.0f;

        public string ComPortname
        {
            get => this._comPortname;
            set
            {
                this._comPortname = value;
                this.OnNotifyPropertyChanged(nameof(this.ComPortname));
            }
        }

        public int RawValue1
        {
            get => this._rawValue1;
            set
            {
                this._rawValue1 = value;
                //this.RenewMinMax();
                this.OnNotifyPropertyChanged(nameof(this.RawValue1));
            }
        }

        public int RawValue2
        {
            get => _rawValue2; set
            {
                _rawValue2 = value;
                OnNotifyPropertyChanged(nameof(this.RawValue2));
            }
        }

        public int RawValue3
        {
            get => _rawValue3; set
            {
                _rawValue3 = value;
                OnNotifyPropertyChanged(nameof(this.RawValue3));
            }
        }

        public int RawValue4
        {
            get => _rawValue4; set
            {
                _rawValue4 = value;
                OnNotifyPropertyChanged(nameof(this.RawValue4));
            }
        }

        public float ResultValue1
        {
            get => _resultValue1; set
            {
                _resultValue1 = value;
                this.OnNotifyPropertyChanged(nameof(this.ResultValue1));
            }
        }

        public float ResultValue2
        {
            get => _resultValue2; set
            {
                _resultValue2 = value;
                OnNotifyPropertyChanged(nameof(this.ResultValue2));
            }
        }

        public float ResultValue3
        {
            get => _resultValue3; set
            {
                _resultValue3 = value;
                OnNotifyPropertyChanged(nameof(this.ResultValue3));
            }
        }

        public float ResultValue4
        {
            get => _resultValue4; set
            {
                _resultValue4 = value;
                OnNotifyPropertyChanged(nameof(this.ResultValue4));
            }
        }

        //private void RenewMinMax()
        //{
        //    if (this._rawValue1 < this._minRawValue)
        //    {
        //        this.MinRawValue = this._rawValue1;
        //    }

        //    if (this._rawValue1 > this._maxRawValue)
        //    {
        //        this.MaxRawValue = this._rawValue1;
        //    }
        //}

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

        public int MeasurementValueIndex
        {
            get => this._measurementValueIndex;
            set
            {
                this._measurementValueIndex = value;
                this.OnNotifyPropertyChanged(nameof(this.MeasurementValueIndex));
            }
        }

        public List<LineDiagramLevelItem> MeasurementValues1
        {
            get => this._measurementValues1;
            set
            {
                this._measurementValues1 = value;
                this.OnNotifyPropertyChanged(nameof(this.MeasurementValues1));
            }
        }

        public List<LineDiagramLevelItem> MeasurementValues2
        {
            get => this._measurementValues2;
            set
            {
                this._measurementValues2 = value;
                this.OnNotifyPropertyChanged(nameof(this.MeasurementValues2));
            }
        }

        public List<LineDiagramLevelItem> MeasurementValues3
        {
            get => this._measurementValues3;
            set
            {
                this._measurementValues3 = value;
                this.OnNotifyPropertyChanged(nameof(this.MeasurementValues3));
            }
        }

        public List<LineDiagramLevelItem> MeasurementValues4
        {
            get => this._measurementValues4;
            set
            {
                this._measurementValues4 = value;
                this.OnNotifyPropertyChanged(nameof(this.MeasurementValues4));
            }
        }

        //public float VoltageValue
        //{
        //    get => this._voltageValue;
        //    set
        //    {
        //        this._voltageValue = value;
        //        this.OnNotifyPropertyChanged(nameof(this.VoltageValue));
        //    }
        //}

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