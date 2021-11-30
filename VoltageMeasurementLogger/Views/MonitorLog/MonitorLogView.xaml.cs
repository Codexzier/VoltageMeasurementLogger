using Codexzier.Wpf.ApplicationFramework.Commands;
using Codexzier.Wpf.ApplicationFramework.Components.Ui.EventBus;
using System;
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
        //private readonly Timer _timer = new();
        private float _offsetValue = 1024;
        private UartConnection _uartConnection;

        public MonitorLogView()
        {
            this.InitializeComponent();

            this._viewModel = (MonitorLogViewModel)this.DataContext;

            this._uartConnection = UartConnection.GetInstance();

            EventBusManager.Register<MonitorLogView, BaseMessage>(this.BaseMessageEvent);
            EventBusManager.Register<MonitorLogView, UpdateOffsetMessage>(this.UpdateOffsetEvent);

            //this._timer.Elapsed += this.Timer_Elapsed;
            //this._timer.Interval = 100;
            //this._timer.Start();
        }

        private void UpdateOffsetEvent(IMessageContainer obj)
        {
            if(obj is UpdateOffsetMessage update)
            {
                this._offsetValue = update.OffsetValue;
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this._viewModel.RawValue = this._uartConnection.RawValue;
            this._viewModel.VoltageValue = this._viewModel.RawValue / this._offsetValue * 10.0f;
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
