using Codexzier.Wpf.ApplicationFramework.Commands;
using Codexzier.Wpf.ApplicationFramework.Components.Ui.EventBus;

namespace VoltageMeasurementLogger.Views.MonitorLog
{
    public partial class MonitorLogView
    {
        private readonly MonitorLogViewModel _viewModel;
        private readonly LineDiagramHelper _lineDiagramHelper;

        public MonitorLogView()
        {
            this.InitializeComponent();

            this._viewModel = (MonitorLogViewModel)this.DataContext;

            this._lineDiagramHelper = new LineDiagramHelper(this._viewModel);

            EventBusManager.Register<MonitorLogView, BaseMessage>(this.BaseMessageEvent);
            EventBusManager.Register<MonitorLogView, UpdateDivisorMessage>(this.UpdateDivisorEvent);
        }

        private void UpdateDivisorEvent(IMessageContainer obj)
        {
            if(obj is UpdateDivisorMessage update)
            {
                LineDiagramHelper.SetDivisor(update);
            }
        }

        private void BaseMessageEvent(IMessageContainer obj)
        {
            if (obj.Content is string str)
            {
                if (string.IsNullOrEmpty(str))
                {
                    return;
                }

                this._viewModel.ComPortName = str;

                this._lineDiagramHelper.Start();
                
                return;
            }

            if(obj.Content is bool set)
            {
                if (!set)
                {
                    this._lineDiagramHelper.Stop();
                }
            }

            this._viewModel.ComPortName = "---";
        }
    }
}
