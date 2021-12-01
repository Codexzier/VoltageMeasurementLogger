using Codexzier.Wpf.ApplicationFramework.Commands;
using Codexzier.Wpf.ApplicationFramework.Components.Ui.EventBus;
using System.Windows.Controls;

namespace VoltageMeasurementLogger.Views.MonitorLog
{
    /// <summary>
    /// Interaction logic for MonitorLogView.xaml
    /// </summary>
    public partial class MonitorLogView : UserControl
    {
        private readonly MonitorLogViewModel _viewModel;
        private readonly LineDiagramHelper _lineDiagramHelper;

        public MonitorLogView()
        {
            this.InitializeComponent();

            this._viewModel = (MonitorLogViewModel)this.DataContext;

            this._lineDiagramHelper = new LineDiagramHelper(this._viewModel);

            EventBusManager.Register<MonitorLogView, BaseMessage>(this.BaseMessageEvent);
            EventBusManager.Register<MonitorLogView, UpdateOffsetMessage>(this.UpdateOffsetEvent);
        }

        private void UpdateOffsetEvent(IMessageContainer obj)
        {
            if(obj is UpdateOffsetMessage update)
            {
                this._lineDiagramHelper.SetOffset(update.OffsetValue);
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

                this._viewModel.ComPortname = str;

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

            this._viewModel.ComPortname = "---";
        }
    }
}
