using Codexzier.Wpf.ApplicationFramework.Commands;
using Codexzier.Wpf.ApplicationFramework.Components.Ui.EventBus;
using Codexzier.Wpf.ApplicationFramework.Views.Base;
using VoltageMeasurementLogger.Views.Main;

namespace VoltageMeasurementLogger.Views.Menu
{
    internal class ButtonCommandOpenMain : BaseCommand
    {
        public override void Execute(object parameter)
        {
            if (EventBusManager.IsViewOpen<MainView>(1)) return;

            EventBusManager.OpenView<MainView>(1);
            EventBusManager.Send<MainView, BaseMessage>(new BaseMessage(""), 1);
        }
    }
}