using Newtonsoft.Json;
using System;
using VoltageMeasurementLogger.Components.UserSettings;

namespace VoltageMeasurementLogger.Components
{
    public static class SerializeHelper
    {
        public static readonly Func<CustomSettingsFile, string> Serialize = JsonConvert.SerializeObject;
        public static readonly Func<string, CustomSettingsFile> Deserialize = JsonConvert.DeserializeObject<CustomSettingsFile>;
    }
}