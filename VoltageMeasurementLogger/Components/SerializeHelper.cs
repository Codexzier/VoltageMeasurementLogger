using Newtonsoft.Json;
using System;
using VoltageMeasurementLogger.Components.UserSettings;

namespace VoltageMeasurementLogger.Components
{
    public static class SerializeHelper
    {
        public static Func<CustomSettingsFile, string> Serialize = JsonConvert.SerializeObject;
        public static Func<string, CustomSettingsFile> Deserialize = JsonConvert.DeserializeObject<CustomSettingsFile>;
    }
}