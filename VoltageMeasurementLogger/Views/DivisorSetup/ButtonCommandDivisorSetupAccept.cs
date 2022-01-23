using Codexzier.Wpf.ApplicationFramework.Components.Ui.EventBus;
using Codexzier.Wpf.ApplicationFramework.Views.Base;
using VoltageMeasurementLogger.Components;
using VoltageMeasurementLogger.Views.MonitorLog;

namespace VoltageMeasurementLogger.Views.DivisorSetup
{
    internal class ButtonCommandDivisorSetupAccept : BaseCommand
    {
        private readonly DivisorSetupViewModel _viewModel;

        public ButtonCommandDivisorSetupAccept(DivisorSetupViewModel viewModel) => this._viewModel = viewModel;

        public override void Execute(object parameter)
        {

            var setting = UserSettingsLoaderHelper.Load();
            var rawValue = this._viewModel.DivisorValue;
            var multi = this._viewModel.DivisorMultiplicator;
            setting.DivisorMultiplicator = multi;

            var resolution = this._viewModel.SelectedDivisorResolution.ToString();
            setting.DivisorValueResolution = resolution;

            UserSettingsLoaderHelper.Save(setting);

            EventBusManager.Send<MonitorLogView, UpdateDivisorMessage>(
                new UpdateDivisorMessage(
                    rawValue, 
                    multi,
                    resolution), 
                SideHostChannel.MainRight);
            EventBusManager.CloseView<DivisorSetupView>(SideHostChannel.DialogWindow);
        }
    }
}
