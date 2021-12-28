using Codexzier.Wpf.ApplicationFramework.Commands;
using Codexzier.Wpf.ApplicationFramework.Components.Ui.EventBus;
using Codexzier.Wpf.ApplicationFramework.Views.Base;
using VoltageMeasurementLogger.Views.LogDataResult;

namespace VoltageMeasurementLogger.Views.LogData
{
    internal class SelectChangedCommandSelectedFileItem : BaseCommand
    {
        public SelectChangedCommandSelectedFileItem() { }

        public override void Execute(object parameter)
        {
            if (parameter == null)
            {
                return;
            }

            if (!EventBusManager.IsViewOpen<LogDataResultView>(SideHostChannel.MainRight))
            {
                EventBusManager.OpenView<LogDataResultView>(SideHostChannel.MainRight);
            }

            EventBusManager.Send<LogDataResultView, BaseMessage>(new BaseMessage(parameter), SideHostChannel.MainRight);
        }
    }
}