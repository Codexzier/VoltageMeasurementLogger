using Codexzier.Wpf.ApplicationFramework.Commands;

namespace VoltageMeasurementLogger.Views.MonitorLog
{
    internal class UpdateDivisorMessage : BaseMessage
    {
        public UpdateDivisorMessage(int rawValue, float divisorMultiplikator, string reso) 
            : base($"{rawValue}:{divisorMultiplikator}:{reso}")
        {
            this.DivisorValue = rawValue;
            this.DivisorMultiplikator = divisorMultiplikator;
        }

        public int DivisorValue { get; } = 1024;

        public float DivisorMultiplikator { get; }

        public string DivisorResolution { get; }
    }
}
