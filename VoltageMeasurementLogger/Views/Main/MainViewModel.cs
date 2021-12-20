using Codexzier.Wpf.ApplicationFramework.Views.Base;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace VoltageMeasurementLogger.Views.Main
{
    internal class MainViewModel : BaseViewModel
    {
        private ObservableCollection<string> _comPorts;
        private string _selectedPortName;
        private ICommand _commandConnectUart;
        private ICommand _commandRefreshComPortList;
        private ICommand _commandDisconnectUart;
        private int _divisorValue;
        private ICommand _commandSetupVoltageDivisor;
        private ICommand _commandWriteLogOnOff;
        private string _filename;
        private Brush _writeState = Brushes.Black;
        private Visibility _visibilityDisconnect = Visibility.Collapsed;
        private Visibility _visibilityConnect;
        private int _divisorMultiplikator;

        public ICommand CommandConnectUart
        {
            get => this._commandConnectUart;
            set
            {
                this._commandConnectUart = value;
                this.OnNotifyPropertyChanged(nameof(this.CommandConnectUart));
            }
        }

        public ICommand CommandDisconnectUart
        {
            get => this._commandDisconnectUart;
            set
            {
                this._commandDisconnectUart = value;
                this.OnNotifyPropertyChanged(nameof(this.CommandDisconnectUart));
            }
        }

        public ICommand CommandRefreshComPortList
        {
            get => this._commandRefreshComPortList;
            set
            {
                this._commandRefreshComPortList = value;
                this.OnNotifyPropertyChanged(nameof(this.CommandRefreshComPortList));
            }
        }


        public ObservableCollection<string> ComPorts
        {
            get => this._comPorts;
            set
            {
                this._comPorts = value;
                this.OnNotifyPropertyChanged(nameof(this.ComPorts));
            }
        }

        public string SelectedPortName
        {
            get => this._selectedPortName;
            set
            {
                this._selectedPortName = value;
                this.OnNotifyPropertyChanged(nameof(this.SelectedPortName));
            }
        }

        public int DivisorValue
        {
            get => this._divisorValue;
            set
            {
                this._divisorValue = value;
                this.OnNotifyPropertyChanged(nameof(this.DivisorValue));
            }
        }

        public int DivisorMultiplikator
        {
            get => _divisorMultiplikator;
            set
            {
                _divisorMultiplikator = value;
                this.OnNotifyPropertyChanged(nameof(this.DivisorMultiplikator));
            }
        }

        public ICommand CommandSetupVoltageDivisor
        {
            get => _commandSetupVoltageDivisor;
            set
            {
                _commandSetupVoltageDivisor = value;
                this.OnNotifyPropertyChanged(nameof(this.CommandSetupVoltageDivisor));
            }
        }

        public ICommand CommandWriteLogOnOff
        {
            get => _commandWriteLogOnOff;
            set
            {
                _commandWriteLogOnOff = value;
                this.OnNotifyPropertyChanged(nameof(this.CommandWriteLogOnOff));
            }
        }

        public string Filename
        {
            get => _filename; set
            {
                _filename = value;
                this.OnNotifyPropertyChanged(nameof(this.Filename));
            }
        }

        public Brush WriteState
        {
            get => _writeState;
            set
            {
                _writeState = value;
                this.OnNotifyPropertyChanged(nameof(this.WriteState));
            }
        }

        public Visibility VisibilityDisconnect
        {
            get => _visibilityDisconnect; set
            {
                _visibilityDisconnect = value;
                this.OnNotifyPropertyChanged(nameof(this.VisibilityDisconnect));
            }
        }

        public Visibility VisibilityConnect
        {
            get => this._visibilityConnect; set
            {
                this._visibilityConnect = value;
                this.OnNotifyPropertyChanged(nameof(this.VisibilityConnect));
            }
        }
    }
}