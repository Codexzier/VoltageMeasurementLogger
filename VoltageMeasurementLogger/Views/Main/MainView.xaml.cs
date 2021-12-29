using Codexzier.Wpf.ApplicationFramework.Commands;
using Codexzier.Wpf.ApplicationFramework.Components.Ui.EventBus;
using Codexzier.Wpf.ApplicationFramework.Views.Base;
using System.Timers;
using VoltageMeasurementLogger.Components;

namespace VoltageMeasurementLogger.Views.Main
{
    public partial class MainView
    {
        private readonly MainViewModel _viewModel;
        private readonly Timer _timer = new();

        public MainView()
        {
            this.InitializeComponent();

            this._viewModel = (MainViewModel)this.DataContext;
            this._viewModel.CommandRefreshComPortList = new ButtonCommandRefreshComPortList(this._viewModel);
            this._viewModel.CommandConnectUart = new ButtonCommandConnectUart(this._viewModel);
            this._viewModel.CommandDisconnectUart = new ButtonCommandDisconnectUart(this._viewModel);
            this._viewModel.CommandSetupVoltageDivisor = new ButtonCommandSetupVoltageDivisor();
            this._viewModel.CommandWriteLogOnOff = new ButtonCommandWriteLogOnOff(this._viewModel);

            EventBusManager.Register<MainView, BaseMessage>(this.BaseMessageEvent);

            // update all 5 secounds
            this._timer.Interval = 3000;
            this._timer.Elapsed += this.Timer_Elapsed;
            this._timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                this._viewModel.CommandRefreshComPortList.Execute(null);
            });
        }

        private void BaseMessageEvent(IMessageContainer obj)
        {
            SimpleStatusOverlays.ActivityOn();

            var setting = UserSettingsLoaderHelper.Load();

            //this._viewModel.DivisorValue = setting.DivisorValue;
            this._viewModel.Filename = FileNameCreator.Create();
            this._viewModel.CommandRefreshComPortList.Execute(null);

            SimpleStatusOverlays.ActivityOff();
        }
    }
}