using Codexzier.Wpf.ApplicationFramework.Commands;
using Codexzier.Wpf.ApplicationFramework.Components.Ui.EventBus;
using Codexzier.Wpf.ApplicationFramework.Components.UserSettings;
using Codexzier.Wpf.ApplicationFramework.Views.Base;
using System.Windows;
using VoltageMeasurementLogger.Components;
using VoltageMeasurementLogger.Components.ArduinoConnection;
using VoltageMeasurementLogger.Components.UserSettings;
using VoltageMeasurementLogger.Views.MonitorLog;

namespace VoltageMeasurementLogger.Views.Main
{
    internal class ButtonCommandConnectUart : BaseCommand
    {
        private readonly MainViewModel _viewModel;

        public ButtonCommandConnectUart(MainViewModel viewModel) => this._viewModel = viewModel;

        public override void Execute(object parameter)
        {
            base.Execute(parameter);

            if(string.IsNullOrEmpty(this._viewModel.SelectedPortName))
            {
                SimpleStatusOverlays.Show("INFO", "No Port selected!");
                return;
            }

            var usl = UserSettingsLoader<CustomSettingsFile>.GetInstance(
                SerializeHelper.Serialize,
                SerializeHelper.Deserialize).Load();

            var connector = UartConnection.GetInstance();
            var result = connector.ConnectTo(usl.DivisorValueResulution1(), usl.DivisorMultiplikator,this._viewModel.SelectedPortName, 115200);
            if(!result.Success)
            {
                SimpleStatusOverlays.Show("INFO", result.Message);
                return;
            }

            this._viewModel.VisibilityDisconnect = Visibility.Visible;
            this._viewModel.VisibilityConnect = Visibility.Collapsed;

            EventBusManager.Send<MonitorLogView, BaseMessage>(new BaseMessage(this._viewModel.SelectedPortName), SideHostChannel.MainRight);
        }
    }
}