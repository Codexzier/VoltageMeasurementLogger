using Codexzier.Wpf.ApplicationFramework.Components.UserSettings;

namespace VoltageMeasurementLogger.Components.UserSettings
{
    public class CustomSettingsFile : SettingsFile
    {
        private string _lastImportDirectory;
        private string _lastImportFilename;
        private bool _loadFromService;
        private bool _loadRkiDataByApplicationStart;

        public CustomSettingsFile() : base(false)
        {
        }

        // ReSharper disable once UnusedMember.Global
        public string LastImportDirectory
        {
            get => this._lastImportDirectory;
            set
            {
                this._lastImportDirectory = value;
                this.SetChanged();
            }
        }

        // ReSharper disable once UnusedMember.Global
        public string LastImportFilename
        {
            get => this._lastImportFilename;
            set
            {
                this._lastImportFilename = value;
                this.SetChanged();
            }
        }

        // ReSharper disable once UnusedMember.Global
        public bool LoadFromService
        {
            get => this._loadFromService;
            set
            {
                this._loadFromService = value;
                this.SetChanged();
            }
        }

        public bool LoadRkiDataByApplicationStart
        {
            get => this._loadRkiDataByApplicationStart;
            set
            {
                this._loadRkiDataByApplicationStart = value;
                this.SetChanged();
            }
        }
    }
}