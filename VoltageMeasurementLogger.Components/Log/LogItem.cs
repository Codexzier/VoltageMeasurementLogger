using System;

namespace VoltageMeasurementLogger.Components.Log
{
    public class LogItem
    {
        public LogItem(string content, DateTime date)
        {
            this.Content = content;
            this.Written = date;

            if(double.TryParse(this.Content, out var d))
            {
                this.NumericContent = d;
            }
        }

        public string Content { get; }
        public double NumericContent { get; }

        public DateTime Written { get; }


    }
}
