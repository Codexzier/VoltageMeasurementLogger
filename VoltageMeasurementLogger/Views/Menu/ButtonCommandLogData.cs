using Codexzier.Wpf.ApplicationFramework.Commands;
using Codexzier.Wpf.ApplicationFramework.Components.Ui.EventBus;
using Codexzier.Wpf.ApplicationFramework.Views.Base;
using VoltageMeasurementLogger.Views.LogData;
using VoltageMeasurementLogger.Views.LogDataResult;

namespace VoltageMeasurementLogger.Views.Menu
{
    internal class ButtonCommandLogData : BaseCommand
    {
        public override void Execute(object parameter)
        {
            if (!EventBusManager.IsViewOpen<LogDataView>(SideHostChannel.Main))
            {
                EventBusManager.OpenView<LogDataView>(SideHostChannel.Main);
                EventBusManager.Send<LogDataView, BaseMessage>(new BaseMessage(""), SideHostChannel.Main);
            }

            if (EventBusManager.IsViewOpen<LogDataResultView>(SideHostChannel.MainRight))
            {
                return;
            }

            EventBusManager.OpenView<LogDataResultView>(SideHostChannel.MainRight);
            EventBusManager.Send<LogDataResultView, BaseMessage>(new BaseMessage(""), SideHostChannel.MainRight);
        }
    }
}