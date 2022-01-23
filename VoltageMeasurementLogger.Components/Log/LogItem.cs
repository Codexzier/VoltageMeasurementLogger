using System;
using System.Collections.Generic;
using System.Linq;

namespace VoltageMeasurementLogger.Components.Log
{
    public class LogItem
    {
        public LogItem(string[] sa)
        {
            if (!DateTime.TryParse(sa[4], out var date))
            {
                date = DateTime.MinValue;
            }

            this.Written = date;

            var list = new List<LogValue>();
            for (int i = 0; i < sa.Length - 1; i++)
            {
                list.Add(LogValue.Create(sa[i]));

            }

            this.LogValues = list.ToArray();
        }

        public LogItem(string content, DateTime date)
        {
            this.Content = content;
            this.Written = date;

            if(int.TryParse(this.Content, out var d))
            {
                this.NumericContent = d;
            }

            if(this.Content.Contains(":"))
            {
                var sa = this.Content.Split(':');

                if(int.TryParse(sa[0], out var s1))
                {
                    this.NumericContent = s1;
                }

                if (int.TryParse(sa[1], out var s2))
                {
                    this.Divisor = s2;
                }
            }
        }

        public string Content { get; }
        public int NumericContent { get; }
        
        public int Divisor { get; }
        
        public float Multiplicator { get; }
        
        public LogValue[] LogValues { get; }

        public DateTime Written { get; }
    }
}
