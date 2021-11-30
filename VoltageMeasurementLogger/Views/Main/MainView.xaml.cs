using Codexzier.Wpf.ApplicationFramework.Commands;
using Codexzier.Wpf.ApplicationFramework.Components.Ui.EventBus;
using Codexzier.Wpf.ApplicationFramework.Views.Base;
using System.IO;
using VoltageMeasurementLogger.Components;
using VoltageMeasurementLogger.Components.Log;

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

            int filenumber = 0;

            while (true)
            {
                var newFilename = $"LoggingVoltage_{filenumber:D4}";
                if (!File.Exists($"{LogManager.PathOfLogFiles}{newFilename}"))
                {
                    this._viewModel.Filename = $"LoggingVoltage_{ filenumber: D4}";
                    break;
                }
                filenumber++;
            }

            SimpleStatusOverlays.ActivityOff();
        }
    }
}