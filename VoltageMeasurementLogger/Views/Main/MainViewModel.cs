using Codexzier.Wpf.ApplicationFramework.Views.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace VoltageMeasurementLogger.Views.Main
{
    internal class MainViewModel : BaseViewModel
    {
        private ObservableCollection<string> _comPorts;
        private string _selectedPortName;
        private ICommand _commandConnectUart;
        private ICommand _commandRefreshComPortList;
        private ICommand _commandDisconnectUart;

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
    }
}