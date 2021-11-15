using Codexzier.Wpf.ApplicationFramework.Commands;
using Codexzier.Wpf.ApplicationFramework.Components.Ui.EventBus;
using Codexzier.Wpf.ApplicationFramework.Views.Base;
using VoltageMeasurementLogger.Views.Main;
using VoltageMeasurementLogger.Views.MonitorLog;

namespace VoltageMeasurementLogger.Views.Menu
{
    internal class ButtonCommandOpenMain : BaseCommand
    {
        public override void Execute(object parameter)
        {
            if (!EventBusManager.IsViewOpen<MainView>(0))
            {
                EventBusManager.OpenView<MainView>(0);
                EventBusManager.Send<MainView, BaseMessage>(new BaseMessage(""), 0);
            }

            if (!EventBusManager.IsViewOpen<MonitorLogView>(1))
            {
                EventBusManager.OpenView<MonitorLogView>(1);
                EventBusManager.Send<MonitorLogView, BaseMessage>(new BaseMessage(""), 1);
            }            
        }
    }
}