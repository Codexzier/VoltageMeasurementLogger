using Codexzier.Wpf.ApplicationFramework.Components.UserSettings;
using VoltageMeasurementLogger.Components.ArduinoConnection;

namespace VoltageMeasurementLogger.Components.UserSettings
{
    public class CustomSettingsFile : SettingsFile
    {
        private string _lastImportDirectory;
        private string _lastImportFilename;
        private bool _loadFromService;
        private float _divisorMultiplicator;
        private string _divisorValueResolution;

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

        // TODO: Multiplikator für vier verschiedene Werte speichern.
        public float DivisorMultiplicator
        {
            get => this._divisorMultiplicator;
            set
            {
                this._divisorMultiplicator = value;
                this.SetChanged();
            }
        }

        // TODO: ggf. als Beschreibung umbenennen.
        public string DivisorValueResolution
        {
            get => _divisorValueResolution; set
            {
                this._divisorValueResolution = value;
                this.SetChanged();
            }
        }

        // TODO: Ausweiten auf die vier divisor Werte, um verschiedene Skalierbare Inhalte anzuzeigen
        public int DivisorValueResolution1()
        {
            return UartConnection.GetDivisorValueResolution(this.DivisorValueResolution).Resolution;
        }
    }
}