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
            this._viewModel.CommandDisconnectUart = new ButtonCommandDisconnectUart();
            this._viewModel.CommandSetupVoltageOffset = new ButtonCommandSetupVoltageOffset(this._viewModel);
            this._viewModel.CommandWriteLogOnOff = new ButtonCommandWriteLogOnOff(this._viewModel);

            EventBusManager.Register<MainView, BaseMessage>(this.BaseMessageEvent);
        }

        private void BaseMessageEvent(IMessageContainer obj)
        {
            SimpleStatusOverlays.ActivityOn();

            var setting = UserSettingsLoaderHelper.Load();

            this._viewModel.OffsetValue = setting.OffsetValue;

            SimpleStatusOverlays.ActivityOff();
        }
    }

    internal class ButtonCommandWriteLogOnOff : BaseCommand
    {
        private MainViewModel _viewModel;

        public ButtonCommandWriteLogOnOff(MainViewModel viewModel) => this._viewModel = viewModel;

        public override void Execute(object parameter)
        {
            base.Execute(parameter);

            // TODO: Write start Stop Logging.
            SimpleStatusOverlays.Show("INFO", "Not implement!");
        }
    }
}