using Codexzier.Wpf.ApplicationFramework.Commands;
using Codexzier.Wpf.ApplicationFramework.Components.Ui.EventBus;
using Codexzier.Wpf.ApplicationFramework.Views.Base;
using VoltageMeasurementLogger.Components;

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
            this._viewModel.CommandDisconnectUart = new ButtonCommandDisconnectUart(this._viewModel);
            this._viewModel.CommandSetupVoltageDivisor = new ButtonCommandSetupVoltageDivisor(this._viewModel);
            this._viewModel.CommandWriteLogOnOff = new ButtonCommandWriteLogOnOff(this._viewModel);

            EventBusManager.Register<MainView, BaseMessage>(this.BaseMessageEvent);
        }

        private void BaseMessageEvent(IMessageContainer obj)
        {
            SimpleStatusOverlays.ActivityOn();

            var setting = UserSettingsLoaderHelper.Load();

            this._viewModel.DivisorValue = setting.DivisorValue;
            this._viewModel.Filename = FileNameCreator.Create();

            this._viewModel.CommandRefreshComPortList.Execute(null);

            SimpleStatusOverlays.ActivityOff();
        }
    }
}