using System;

namespace VoltageMeasurementLogger.Components.Helpers
{
    public class VoltageCalculateHelperException : Exception
    {
        public VoltageCalculateHelperException(string message):base(message)
        {
        }
    }
}