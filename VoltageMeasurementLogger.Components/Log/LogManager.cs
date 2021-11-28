using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoltageMeasurementLogger.Components.Log
{
    public class LogManager
    {
        private static LogManager _instance;

        private LogManager()
        {
        }

        public static LogManager GetInstance()
        {
            if(_instance == null)
            {
                _instance = new LogManager();
            }

            return _instance;
        }

        public void WriteToFile(string filename)
        {
            string fullname = $"{PathOfLogFiles}{filename}";
            if (File.Exists(fullname))
            {
                File.WriteAllText(fullname, string.Empty);
                return;
            }

            File.Create(fullname);
        }

        public static string PathOfLogFiles => $"{Directory.GetCurrentDirectory()}/Logs/";
    }
}
