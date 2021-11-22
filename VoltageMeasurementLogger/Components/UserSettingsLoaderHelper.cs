using Codexzier.Wpf.ApplicationFramework.Components.UserSettings;
using VoltageMeasurementLogger.Components.UserSettings;

namespace VoltageMeasurementLogger.Components
{
    internal static class UserSettingsLoaderHelper
    {
        internal static CustomSettingsFile Load() => UserSettingsLoader<CustomSettingsFile>
                    .GetInstance(
                        SerializeHelper.Serialize,
                        SerializeHelper.Deserialize)
                    .Load();

        internal static void Save(CustomSettingsFile setting) =>
            UserSettingsLoader<CustomSettingsFile>
                   .GetInstance(
                        SerializeHelper.Serialize, 
                        SerializeHelper.Deserialize)
                   .Save(setting);
    }
}
