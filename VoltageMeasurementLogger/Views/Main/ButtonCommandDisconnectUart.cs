using Codexzier.Wpf.ApplicationFramework.Commands;
using Codexzier.Wpf.ApplicationFramework.Components.Ui.EventBus;
using Codexzier.Wpf.ApplicationFramework.Views.Base;
using System.Windows;
using VoltageMeasurementLogger.Components.ArduinoConnection;
using VoltageMeasurementLogger.Views.MonitorLog;

namespace VoltageMeasurementLogger.Views.Main
{
    internal class ButtonCommandDisconnectUart : BaseCommand
    {
        private MainViewModel _viewModel;

        public ButtonCommandDisconnectUart(MainViewModel viewModel) => this._viewModel = viewModel;

        public override void Execute(object parameter)
        {
            base.Execute(parameter);

            var result = UartConnection.GetInstance().Close();
            if (!result.Success)
            {
                SimpleStatusOverlays.Show("INFO", result.Message);
            }

            this._viewModel.VisibilityDisconnect = Visibility.Collapsed;
            this._viewModel.VisibilityConnect = Visibility.Visible;

            EventBusManager.Send<MonitorLogView, BaseMessage>(new BaseMessage(false), 1);
        }
    }
}