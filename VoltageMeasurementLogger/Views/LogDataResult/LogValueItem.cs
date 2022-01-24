using System;

namespace VoltageMeasurementLogger.Views.LogDataResult
{
    internal class LogValueItem
    {
        public DateTime Date { get; internal set; }
        public int Nr { get; internal set; }
        public int RawValue1 { get; internal set; }
        public int RawValue2 { get; internal set; }
        public int RawValue3 { get; internal set; }
        public int RawValue4 { get; internal set; }
    }
}