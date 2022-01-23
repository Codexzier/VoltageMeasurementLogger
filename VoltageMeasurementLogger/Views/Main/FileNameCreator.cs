using System.IO;
using VoltageMeasurementLogger.Components.Log;

namespace VoltageMeasurementLogger.Views.Main
{
    internal static class FileNameCreator
    {
        internal static string Create()
        {
            var fileNumber = 0;

            string str;
            while (true)
            {
                var newFilename = $"LoggingVoltage_{fileNumber:0000}";
                if (!File.Exists($"{LogManager.PathOfLogFiles}{newFilename}"))
                {
                    str = newFilename;
                    break;
                }
                fileNumber++;
            }

            return str;
        }
    }
}