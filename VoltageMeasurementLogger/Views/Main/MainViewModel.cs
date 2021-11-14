using Codexzier.Wpf.ApplicationFramework.Views.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace VoltageMeasurementLogger.Views.Main
{
    internal class MainViewModel : BaseViewModel
    {
        

        public ICommand CommandConnectUart { get; set; }

        private ObservableCollection<string> _comPorts;

        public ObservableCollection<string> ComPorts
        {
            get => this._comPorts;
            set
            {
                this._comPorts = value;
                this.OnNotifyPropertyChanged(nameof(this.ComPorts));
            }
        }
    }
}