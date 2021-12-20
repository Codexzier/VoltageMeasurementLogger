﻿using Codexzier.Wpf.ApplicationFramework.Views.Base;
using System.Windows.Input;
using System.Windows.Media;
using VoltageMeasurementLogger.Components.ArduinoConnection;

namespace VoltageMeasurementLogger.Views.DivisorSetup
{
    internal class DivisorSetupViewModel : BaseViewModel
    {
        private int _divisorValue = 1024;
        private float _divisorMultiplikator = 10.0f;
        private string _calculateResult = "1024 /  1024 * 10.0";
        private Brush _calculateResultOk = Brushes.Red;
        private DivisiorResolutionItem _divisorValueResolution = UartConnection.GetDivisorValueResolutions()[0];
        private ICommand _commandDivisorSetupCancel;
        private ICommand _commandDivisorSetupAccept;
        private DivisiorResolutionItem[] _divisorValueResolutions = UartConnection.GetDivisorValueResolutions();
        private int _selectedDivisorResolutionIndex;

        public int DivisorValue
        {
            get => this._divisorValue;
            set
            {
                this._divisorValue = value;
                this.OnNotifyPropertyChanged(nameof(this.DivisorValue));
            }
        }

        public DivisiorResolutionItem[] DivisorValueResolutions
        {
            get => _divisorValueResolutions;
            set
            {
                _divisorValueResolutions = value;
                this.OnNotifyPropertyChanged(nameof(this.DivisorValueResolutions));
            }
        }

        public DivisiorResolutionItem SelectedDivisorResolution
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

        public float DivisorMultiplikator
        {
            get => _divisorMultiplikator;
            set
            {
                _divisorMultiplikator = value;
                this.OnNotifyPropertyChanged(nameof(this.DivisorMultiplikator));
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
    }
}