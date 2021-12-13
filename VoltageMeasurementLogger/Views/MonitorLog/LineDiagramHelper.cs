using Codexzier.Wpf.ApplicationFramework.Views.Base;
using System.Collections.Generic;
using System.Timers;
using VoltageMeasurementLogger.Components.ArduinoConnection;
using VoltageMeasurementLogger.UserControls.LineDiagram;

namespace VoltageMeasurementLogger.Views.MonitorLog
{
    public class LineDiagramHelper
    {
        private readonly MonitorLogViewModel _viewModel;
        private readonly Timer _timer = new();
        private int _index;
        private float _divisorValue = 1024;
        private UartConnection _uartConnection;

        public LineDiagramHelper(MonitorLogViewModel viewModel)
        {
            this._viewModel = viewModel;
            this._uartConnection = UartConnection.GetInstance();
            this._uartConnection.NoIncomingDataEvent += this.UartConnection_NoIncomingDataEvent;
            this.Init();
        }

        private bool _isMessageBoxOpenWarningNoIncomingData;

        private void UartConnection_NoIncomingDataEvent()
        {
            if(this._isMessageBoxOpenWarningNoIncomingData)
            {
                return;
            }

            this._isMessageBoxOpenWarningNoIncomingData = true;
            // TODO: Callback funktion in die Methode show nach pflegen.
            // Damit hier das öffnen zustand zurück gesetzt werden kann.
            SimpleStatusOverlays.Show("Warning", "No incoming data");
        }

        private void Init()
        {
            var diagramLevelItemsSource = new List<LineDiagramLevelItem>();

            for (int i = 0; i < 200; i++)
            {
                diagramLevelItemsSource.Add(new LineDiagramLevelItem
                {
                    Value = 0,
                    SetColor = 1,
                    SetHighlightMark = false,
                    ToolTipText = $"Value: {i}"
                });
            }

            this._viewModel.MeasurementValues = diagramLevelItemsSource;

            this._timer.Interval = 10;
            this._timer.Elapsed += this.Timer_Elapsed;
        }

        public void Start() => this._timer.Start();

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this._viewModel.RawValue = this._uartConnection.RawValue;
            this._viewModel.VoltageValue = this._viewModel.RawValue / this._divisorValue * 10.0f;

            if (this._index < this._viewModel.MeasurementValues.Count)
            {
                this._viewModel.MeasurementValues[this._index].Value = this._viewModel.VoltageValue;
                this._viewModel.MeasurementValueIndex = this._index;
                this._index++;
            }
            else
            {
                this._index = 0;
            }
        }

        internal void SetDivisor(float divisorValue) => this._divisorValue = divisorValue;

        internal void Stop() => this._timer.Stop();
    }
}