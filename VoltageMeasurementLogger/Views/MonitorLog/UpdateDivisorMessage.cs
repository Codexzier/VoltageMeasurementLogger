using Codexzier.Wpf.ApplicationFramework.Commands;

namespace VoltageMeasurementLogger.Views.MonitorLog
{
    internal class UpdateDivisorMessage : BaseMessage
    {
        public UpdateDivisorMessage(object content) : base(content)
        {
            if(content is float f)
            {
                this.DivisorValue = f;
            }
        }

        public float DivisorValue { get; } = 1024;
    }
}
