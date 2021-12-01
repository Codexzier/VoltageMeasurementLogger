using Codexzier.Wpf.ApplicationFramework.Commands;
using Codexzier.Wpf.ApplicationFramework.Components.Ui.EventBus;
using Codexzier.Wpf.ApplicationFramework.Views.Base;
using VoltageMeasurementLogger.Components.ArduinoConnection;
using VoltageMeasurementLogger.Views.MonitorLog;

namespace VoltageMeasurementLogger.Views.Main
{
    internal class ButtonCommandDisconnectUart : BaseCommand
    {
        public override void Execute(object parameter)
        {
            base.Execute(parameter);

            var result = UartConnection.GetInstance().Close();
            if (!result.Success)
            {
                SimpleStatusOverlays.Show("INFO", result.Message);
            }

            EventBusManager.Send<MonitorLogView, BaseMessage>(new BaseMessage(false), 1);
        }
    }
}