using Codexzier.Wpf.ApplicationFramework.Commands;
using Codexzier.Wpf.ApplicationFramework.Components.Ui.EventBus;
using Codexzier.Wpf.ApplicationFramework.Views.Base;
using System.Threading.Tasks;
using VoltageMeasurementLogger.Views.MonitorLog;

namespace VoltageMeasurementLogger.Views.Main
{
    public partial class MainView
    {
        private readonly MainViewModel _viewModel;
        public MainView()
        {
            this.InitializeComponent();

            this._viewModel = (MainViewModel)this.DataContext;
            this._viewModel.CommandRefreshComPortList = new ButtonCommandRefreshComPortList(this._viewModel);
            this._viewModel.CommandConnectUart = new ButtonCommandConnectUart(this._viewModel);
            this._viewModel.CommandDisconnectUart = new ButtonCommandDisconnectUart();

            EventBusManager.Register<MainView, BaseMessage>(this.BaseMessageEvent);
        }

        private async void BaseMessageEvent(IMessageContainer obj)
        {
            SimpleStatusOverlays.ActivityOn();

            //if (EventBusManager.IsViewOpen<MonitorLogView>(1))
            //{
            //    return;
            //}

            //EventBusManager.OpenView<MonitorLogView>(1);

            await Task.Delay(200);

            SimpleStatusOverlays.ActivityOff();
        }
    }
}