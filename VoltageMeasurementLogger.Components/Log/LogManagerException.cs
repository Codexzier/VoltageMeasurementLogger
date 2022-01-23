using System;
using System.Runtime.Serialization;

namespace VoltageMeasurementLogger.Components.Log
{
    [Serializable]
    public class LogManagerException : Exception
    {
        public LogManagerException()
        {
        }

        public LogManagerException(string message) : base(message)
        {
        }

        public LogManagerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LogManagerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}