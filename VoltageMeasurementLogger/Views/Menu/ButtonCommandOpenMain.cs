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
            if (!EventBusManager.IsViewOpen<MainView>(SideHostChannel.Main))
            {
                EventBusManager.OpenView<MainView>(SideHostChannel.Main);
                EventBusManager.Send<MainView, BaseMessage>(new BaseMessage(""), SideHostChannel.Main);
            }

            if (!EventBusManager.IsViewOpen<MonitorLogView>(SideHostChannel.MainRight))
            {
                EventBusManager.OpenView<MonitorLogView>(SideHostChannel.MainRight);
                EventBusManager.Send<MonitorLogView, BaseMessage>(new BaseMessage(""), SideHostChannel.MainRight);
            }            
        }
    }
}