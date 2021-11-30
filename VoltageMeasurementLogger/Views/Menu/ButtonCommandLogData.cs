using Codexzier.Wpf.ApplicationFramework.Commands;
using Codexzier.Wpf.ApplicationFramework.Components.Ui.EventBus;
using Codexzier.Wpf.ApplicationFramework.Views.Base;
using System.Windows.Input;
using VoltageMeasurementLogger.Views.LogData;
using VoltageMeasurementLogger.Views.LogDataResult;

namespace VoltageMeasurementLogger.Views.Menu
{
    internal class ButtonCommandLogData : BaseCommand
    {
        public override void Execute(object parameter)
        {
            if (!EventBusManager.IsViewOpen<LogDataView>(0))
            {
                EventBusManager.OpenView<LogDataView>(0);
                EventBusManager.Send<LogDataView, BaseMessage>(new BaseMessage(""), 0);
            }

            if (!EventBusManager.IsViewOpen<LogDataResultView>(1))
            {
                EventBusManager.OpenView<LogDataResultView>(1);
                EventBusManager.Send<LogDataResultView, BaseMessage>(new BaseMessage(""), 1);
            }
        }
    }
}