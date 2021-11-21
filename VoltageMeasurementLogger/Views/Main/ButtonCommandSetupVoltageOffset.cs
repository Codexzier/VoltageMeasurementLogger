using Codexzier.Wpf.ApplicationFramework.Views.Base;
using VoltageMeasurementLogger.Components;
using VoltageMeasurementLogger.Components.ArduinoConnection;

namespace VoltageMeasurementLogger.Views.Main
{
    internal class ButtonCommandSetupVoltageOffset : BaseCommand
    {
        private MainViewModel _viewModel;

        public ButtonCommandSetupVoltageOffset(MainViewModel viewModel) => this._viewModel = viewModel;

        public override void Execute(object parameter)
        {
            base.Execute(parameter);

            var serialConnection = UartConnection.GetInstance();

            if(!serialConnection.IsOpen)
            {
                SimpleStatusOverlays.Show("TIP", "No connection!");
                return;
            }

            var setting = UserSettingsLoaderHelper.Load();

            var val = serialConnection.RawValue;
            setting.OffsetValue = val;
            this._viewModel.OffsetValue = val; 

            UserSettingsLoaderHelper.Save(setting);
        }
    }
}