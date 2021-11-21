using Codexzier.Wpf.ApplicationFramework.Commands;

namespace VoltageMeasurementLogger.Views.MonitorLog
{
    internal class UpdateOffsetMessage : BaseMessage
    {
        public UpdateOffsetMessage(object content) : base(content)
        {
            if(content is float f)
            {
                this.OffsetValue = f;
            }
        }

        public float OffsetValue { get; } = 1024;
    }
}
