using System.IO;
using VoltageMeasurementLogger.Components.Log;

namespace VoltageMeasurementLogger.Views.Main
{
    internal static class FileNameCreator
    {
        internal static string Create()
        {
            int filenumber = 0;

            string str;
            while (true)
            {
                string newFilename = $"LoggingVoltage_{filenumber:0000}";
                if (!File.Exists($"{LogManager.PathOfLogFiles}{newFilename}"))
                {
                    str = newFilename;
                    break;
                }
                filenumber++;
            }

            return str;
        }
    }
}