using Codexzier.Wpf.ApplicationFramework.Components.Ui.EventBus;
using Codexzier.Wpf.ApplicationFramework.Views.Base;
using VoltageMeasurementLogger.Components;
using VoltageMeasurementLogger.Views.MonitorLog;

namespace VoltageMeasurementLogger.Views.DivisorSetup
{
    internal class ButtonCommandDivisorSetupAccept : BaseCommand
    {
        private DivisorSetupViewModel _viewModel;

        public ButtonCommandDivisorSetupAccept(DivisorSetupViewModel viewModel) => this._viewModel = viewModel;

        public override void Execute(object parameter)
        {

            var setting = UserSettingsLoaderHelper.Load();

            var rawValue = this._viewModel.DivisorValue;
            //setting.DivisorValue = this._viewModel.DivisorValue;

            var multi = this._viewModel.DivisorMultiplikator;
            setting.DivisorMultiplikator = multi;

            var reso = this._viewModel.SelectedDivisorResolution.ToString();
            setting.DivisorValueResolution = reso;

            UserSettingsLoaderHelper.Save(setting);

            EventBusManager.Send<MonitorLogView, UpdateDivisorMessage>(
                new UpdateDivisorMessage(
                    rawValue, 
                    multi,
                    reso), 
                SideHostChannel.MainRight);
            EventBusManager.CloseView<DivisorSetupView>(SideHostChannel.DialogWindow);
        }
    }
}
