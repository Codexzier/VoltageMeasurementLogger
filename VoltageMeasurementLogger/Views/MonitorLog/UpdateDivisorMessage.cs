using Codexzier.Wpf.ApplicationFramework.Commands;

namespace VoltageMeasurementLogger.Views.MonitorLog
{
    internal class UpdateDivisorMessage : BaseMessage
    {
        public UpdateDivisorMessage(int divisor, float multiplicator, string divisorResolution) 
            : base($"{divisor}:{multiplicator}:{divisorResolution}")
        {
            this.Divisor = divisor;
            this.Multiplicator = multiplicator;
            this.DivisorResolution = divisorResolution;
        }

        public int Divisor { get; } = 1024;

        public float Multiplicator { get; }

        public string DivisorResolution { get; }
    }
}
