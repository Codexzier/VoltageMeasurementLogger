using Codexzier.Wpf.ApplicationFramework.Commands;
using Codexzier.Wpf.ApplicationFramework.Components.Ui.EventBus;
using System.Timers;
using System.Windows.Controls;
using VoltageMeasurementLogger.Components.ArduinoConnection;

namespace VoltageMeasurementLogger.Views.MonitorLog
{
    /// <summary>
    /// Interaction logic for MonitorLogView.xaml
    /// </summary>
    public partial class MonitorLogView : UserControl
    {
        private readonly MonitorLogViewModel _viewModel;
        private Timer _timer = new Timer();

        public MonitorLogView()
        {
            this.InitializeComponent();

            this._viewModel = (MonitorLogViewModel)this.DataContext;

            EventBusManager.Register<MonitorLogView, BaseMessage>(this.BaseMessageEvent);
            //UartConnection.GetInstance()
            this._timer.Elapsed += this._timer_Elapsed;
            this._timer.Interval = 100;
            this._timer.Start();
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this._viewModel.RawValue = UartConnection.GetInstance().RawValue;
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
                return;
            }

            this._viewModel.ComPortname = "---";
        }
    }
}
