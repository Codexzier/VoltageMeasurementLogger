using Codexzier.Wpf.ApplicationFramework.Commands;
using Codexzier.Wpf.ApplicationFramework.Components.Ui.EventBus;
using Codexzier.Wpf.ApplicationFramework.Views.Base;
using System.Windows.Input;
using VoltageMeasurementLogger.Views.LogDataResult;

namespace VoltageMeasurementLogger.Views.LogData
{
    internal class SelectChangedCommandSelectedFileItem : BaseCommand
    {
        private LogDataViewModel _viewModel;

        public SelectChangedCommandSelectedFileItem(LogDataViewModel viewModel) => this._viewModel = viewModel;

        public override void Execute(object parameter)
        {
            if (parameter == null)
            {
                return;
            }

            if (!EventBusManager.IsViewOpen<LogDataResultView>(1))
            {
                EventBusManager.OpenView<LogDataResultView>(1);
            }

            EventBusManager.Send<LogDataResultView, BaseMessage>(new BaseMessage(parameter), 1);
        }
    }
}