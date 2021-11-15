using Codexzier.Wpf.ApplicationFramework.Views.Base;
using System.Collections.ObjectModel;
using System.IO.Ports;

namespace VoltageMeasurementLogger.Views.Main
{
    internal class ButtonCommandRefreshComPortList : BaseCommand
    {
        private readonly MainViewModel _viewModel;

        public ButtonCommandRefreshComPortList(MainViewModel viewModel) => this._viewModel = viewModel;

        public override void Execute(object parameter)
        {
            base.Execute(parameter);

            this._viewModel.ComPorts = new ObservableCollection<string>(SerialPort.GetPortNames());
        }
    }
}