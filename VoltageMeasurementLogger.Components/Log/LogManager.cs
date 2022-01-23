using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VoltageMeasurementLogger.Components.Log
{
    public class LogManager
    {
        private static LogManager _instance;
        private string _fullFilename;

        private LogManager()
        {
        }

        public static LogManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new LogManager();
            }

            return _instance;
        }

        public void WriteToFile(string filename)
        {
            this._fullFilename = $"{PathOfLogFiles}{filename}";
            this.IsOn = true;

            if (File.Exists(_fullFilename))
            {
                File.WriteAllText(_fullFilename, string.Empty);
                return;
            }

            Directory.CreateDirectory(PathOfLogFiles);

            File.Create(_fullFilename).Close();
        }

        public void Stop() => this.IsOn = false;

        public static string PathOfLogFiles => $"{Directory.GetCurrentDirectory()}/Logs/";

        public bool IsOn { get; private set; }

        public void WriteLine(string lineText)
        {
            if(!this.IsOn)
            {
                return;
            }

            var sw = File.AppendText(this._fullFilename);
            sw.WriteLine($"{lineText};{DateTime.Now}");
            sw.Close();
        }

        public void WriteLine(int rawValue, int divisor) => this.WriteLine($"{rawValue}:{divisor}");

        public IEnumerable<LogItem> GetLogs(string filename)
        {
            var logItems = new List<LogItem>();

            using(var streamReader = new StreamReader($"{PathOfLogFiles}{filename}"))
            {
                string str;
                while ((str = streamReader.ReadLine()) != null)
                {
                    if(string.IsNullOrEmpty(str))
                    {
                        continue;
                    }

                    var sa = str.Split(';');

                    switch(sa.Length)
                    {
                        case 5:
                        {
                            var li = new LogItem(sa);
                            logItems.Add(li);


                            break;
                        }
                        case 2:
                        {
                            if (!DateTime.TryParse(sa[1], out var date))
                            {
                                date = DateTime.MinValue;
                            }

                            var li = new LogItem(sa[0], date);
                            logItems.Add(li);
                            break;
                        }
                        default:
                        {
                            break;
                        }
                    }
                    
                }
            }

            return logItems;
        }


        public void WriteValues(int value1, int value2, int value3, int value4, int divisor, float multiplicator)
        {
            var sb = new StringBuilder();
            sb.Append($"{value1}:{divisor}:{multiplicator};");
            sb.Append($"{value2}:{divisor}:{multiplicator};");
            sb.Append($"{value3}:{divisor}:{multiplicator};");
            sb.Append($"{value4}:{divisor}:{multiplicator}");
            this.WriteLine(sb.ToString());
        }
    }
}
