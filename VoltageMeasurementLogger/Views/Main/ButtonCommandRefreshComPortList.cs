using Codexzier.Wpf.ApplicationFramework.Views.Base;
using System.IO.Ports;
using System.Linq;

namespace VoltageMeasurementLogger.Views.Main
{
    internal class ButtonCommandRefreshComPortList : BaseCommand
    {
        private readonly MainViewModel _viewModel;

        public ButtonCommandRefreshComPortList(MainViewModel viewModel) => this._viewModel = viewModel;

        public override void Execute(object parameter)
        {
            var ports = SerialPort.GetPortNames();

            var removeList = this._viewModel.ComPorts.Select(s => s).ToList();
            foreach (var port in ports)
            {
                if (!this._viewModel.ComPorts.Contains(port))
                {
                    this._viewModel.ComPorts.Add(port);
                }

                removeList.Remove(port);
            }

            foreach (var removePort in removeList)
            {
                if (this._viewModel.SelectedPortName != null && this._viewModel.SelectedPortName.Equals(removePort))
                {
                    this._viewModel.SelectedPortName = string.Empty;
                }

                this._viewModel.ComPorts.Remove(removePort);
            }
        }
    }
}