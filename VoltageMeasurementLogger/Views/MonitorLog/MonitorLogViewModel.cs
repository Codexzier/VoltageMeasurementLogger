using Codexzier.Wpf.ApplicationFramework.Views.Base;

namespace VoltageMeasurementLogger.Views.MonitorLog
{
    public class MonitorLogViewModel : BaseViewModel
    {
        private string _comPortname;
        private int _rawValue;
        private int _minRawValue = 0;
        private int _maxRawValue = 1024;

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
            if(this._rawValue < this._minRawValue)
            {
                this.MinRawValue = this._rawValue;
            }

            if(this._rawValue > this._maxRawValue)
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
    }
}