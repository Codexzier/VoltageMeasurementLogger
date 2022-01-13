using Codexzier.Wpf.ApplicationFramework.Commands;
using Codexzier.Wpf.ApplicationFramework.Components.Ui.EventBus;
using Codexzier.Wpf.ApplicationFramework.Views.Base;
using VoltageMeasurementLogger.Components.ArduinoConnection;
using VoltageMeasurementLogger.Views.DivisorSetup;

namespace VoltageMeasurementLogger.Views.Main
{
    internal class ButtonCommandSetupVoltageDivisor : BaseCommand
    {
        public ButtonCommandSetupVoltageDivisor() { }

        public override void Execute(object parameter)
        {
            base.Execute(parameter);

            var serialConnection = UartConnection.GetInstance();

            if (!serialConnection.IsOpen)
            {
                SimpleStatusOverlays.Show("TIP", "No connection!");
                return;
            }

            if (!EventBusManager.IsViewOpen<DivisorSetupView>(SideHostChannel.DialogWindow))
            {
                EventBusManager.Send<DivisorSetupView, BaseMessage>(
                    new BaseMessage(null),
                    SideHostChannel.DialogWindow,
                    true);
            }
        }
    }
}