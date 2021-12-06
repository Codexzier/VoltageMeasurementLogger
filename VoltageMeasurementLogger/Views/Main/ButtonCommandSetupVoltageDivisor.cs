using Codexzier.Wpf.ApplicationFramework.Views.Base;
using VoltageMeasurementLogger.Components;
using VoltageMeasurementLogger.Components.ArduinoConnection;

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

            var setting = UserSettingsLoaderHelper.Load();

            var val = serialConnection.RawValue;
            setting.DivisorValue = val;
            this._viewModel.DivisorValue = val; 

            UserSettingsLoaderHelper.Save(setting);
        }
    }
}