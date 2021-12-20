using Codexzier.Wpf.ApplicationFramework.Commands;
using Codexzier.Wpf.ApplicationFramework.Components.Ui.EventBus;
using Codexzier.Wpf.ApplicationFramework.Views.Base;
using VoltageMeasurementLogger.Components;
using VoltageMeasurementLogger.Components.ArduinoConnection;
using VoltageMeasurementLogger.Views.DivisorSetup;
using VoltageMeasurementLogger.Views.MonitorLog;

namespace VoltageMeasurementLogger.Views.Main
{
    internal class ButtonCommandSetupVoltageDivisor : BaseCommand
    {
        private MainViewModel _viewModel;

        public ButtonCommandSetupVoltageDivisor(MainViewModel viewModel) => this._viewModel = viewModel;

        public override void Execute(object parameter)
        {
            base.Execute(parameter);

            var serialConnection = UartConnection.GetInstance();

            if(!serialConnection.IsOpen)
            {
                SimpleStatusOverlays.Show("TIP", "No connection!");
                return;
            }

            if(!EventBusManager.IsViewOpen<DivisorSetupView>(99))
            {
                EventBusManager.Send<DivisorSetupView, BaseMessage>(new BaseMessage(null), 99, true);
            }

            //var setting = UserSettingsLoaderHelper.Load();

            //var rawVal = serialConnection.RawValue;
            //setting.DivisorValue = rawVal;
            //this._viewModel.DivisorValue = rawVal;

            //var multiVal = this._viewModel.DivisorMultiplikator;
            //setting.DivisorMultiplikator = multiVal;

            //UserSettingsLoaderHelper.Save(setting);

            //EventBusManager.Send<MonitorLogView, UpdateDivisorMessage>(new UpdateDivisorMessage(rawVal, multiVal), 1);
        }
    }
}