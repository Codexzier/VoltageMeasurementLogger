using Codexzier.Wpf.ApplicationFramework.Components.Ui.EventBus;
using Codexzier.Wpf.ApplicationFramework.Views.Base;

namespace VoltageMeasurementLogger.Views.DivisorSetup
{
    internal class ButtonCommandDivisorSetupCancel : BaseCommand
    {
        public override void Execute(object parameter) => EventBusManager.CloseView<DivisorSetupView>(SideHostChannel.DialogWindow);
    }
}
