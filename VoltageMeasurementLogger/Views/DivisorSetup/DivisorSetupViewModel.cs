using Codexzier.Wpf.ApplicationFramework.Views.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using VoltageMeasurementLogger.Components.ArduinoConnection;

namespace VoltageMeasurementLogger.Views.DivisorSetup
{
    internal class DivisorSetupViewModel : BaseViewModel
    {
        private int _divisorValue = 1024;
        private float _divisorMultiplicator = 10.0f;
        private string _calculateResult = "1024 /  1024 * 10.0";
        private Brush _calculateResultOk = Brushes.Red;
        private DivisorResolutionItem _divisorValueResolution = UartConnection.GetDivisorValueResolutions()[0];
        private ICommand _commandDivisorSetupCancel;
        private ICommand _commandDivisorSetupAccept;
        private ObservableCollection<DivisorResolutionItem> _divisorValueResolutions = new ();
        private int _selectedDivisorResolutionIndex;
        private bool _multiplicatorAutoSet = true;

        public DivisorSetupViewModel()
        {
            foreach (var item in UartConnection.GetDivisorValueResolutions())
            {
                this._divisorValueResolutions.Add(item);
            }
        }

        public int DivisorValue
        {
            get => this._divisorValue;
            set
            {
                this._divisorValue = value;
                this.OnNotifyPropertyChanged(nameof(this.DivisorValue));
            }
        }

        public ObservableCollection<DivisorResolutionItem> DivisorValueResolutions
        {
            get => _divisorValueResolutions;
            set
            {
                _divisorValueResolutions = value;
                this.OnNotifyPropertyChanged(nameof(this.DivisorValueResolutions));
            }
        }

        public DivisorResolutionItem SelectedDivisorResolution
        {
            get => _divisorValueResolution; set
            {
                _divisorValueResolution = value;
                this.OnNotifyPropertyChanged(nameof(this.SelectedDivisorResolution));
            }
        }

        public int SelectedDivisorResolutionIndex
        {
            get => _selectedDivisorResolutionIndex; set
            {
                _selectedDivisorResolutionIndex = value;
                this.OnNotifyPropertyChanged(nameof(this.SelectedDivisorResolutionIndex));
            }
        }

        public float DivisorMultiplicator
        {
            get => this._divisorMultiplicator;
            set
            {
                this._divisorMultiplicator = value;
                this.OnNotifyPropertyChanged(nameof(this.DivisorMultiplicator));
            }
        }

        public string CalculateResult
        {
            get => this._calculateResult;
            set
            {
                this._calculateResult = value;
                this.OnNotifyPropertyChanged(nameof(this.CalculateResult));
            }
        }

        public Brush CalculateResultOk
        {
            get => _calculateResultOk;
            set
            {
                _calculateResultOk = value;
                this.OnNotifyPropertyChanged(nameof(this.CalculateResultOk));
            }
        }

        public ICommand CommandDivisorSetupCancel
        {
            get => _commandDivisorSetupCancel; set
            {
                _commandDivisorSetupCancel = value;
                this.OnNotifyPropertyChanged(nameof(this.CommandDivisorSetupCancel));
            }
        }

        public ICommand CommandDivisorSetupAccept
        {
            get => _commandDivisorSetupAccept; set
            {
                _commandDivisorSetupAccept = value;
                this.OnNotifyPropertyChanged(nameof(this.CommandDivisorSetupAccept));
            }
        }

        //public string SelectedDivisorResolutionValuePath
        //{
        //    get => _selectedDivisorResolutionValuePath;
        //    set
        //    {
        //        _selectedDivisorResolutionValuePath = value;
        //        this.OnNotifyPropertyChanged(nameof(this.SelectedDivisorResolutionValuePath));
        //    }
        //}

        public bool MultiplicatorAutoSet
        {
            get => this._multiplicatorAutoSet; set
            {
                this._multiplicatorAutoSet = value;
                this.OnNotifyPropertyChanged(nameof(this.MultiplicatorAutoSet));
            }
        }
    }
}