using Codexzier.Wpf.ApplicationFramework.Views.Base;
using System.Collections.Generic;
using System.Timers;
using VoltageMeasurementLogger.Components;
using VoltageMeasurementLogger.Components.ArduinoConnection;
using VoltageMeasurementLogger.Components.Helpers;
using VoltageMeasurementLogger.UserControls.LineDiagram;

namespace VoltageMeasurementLogger.Views.MonitorLog
{
    public class LineDiagramHelper
    {
        private readonly MonitorLogViewModel _viewModel;
        private readonly Timer _timer = new();
        private int _index;
        private bool _isMessageBoxOpenWarningNoIncomingData;
        // private int _divisorResolution;
        // private float _divisorMultiplicator = 10;
        private readonly UartConnection _uartConnection;

        public LineDiagramHelper(MonitorLogViewModel viewModel)
        {
            this._viewModel = viewModel;
            this._uartConnection = UartConnection.GetInstance();
            this._uartConnection.NoIncomingDataEvent += this.UartConnection_NoIncomingDataEvent;
            this.Init();
        }

   

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
            var diagramLevelItemsSource1 = new List<LineDiagramLevelItem>();
            var diagramLevelItemsSource2 = new List<LineDiagramLevelItem>();
            var diagramLevelItemsSource3 = new List<LineDiagramLevelItem>();
            var diagramLevelItemsSource4 = new List<LineDiagramLevelItem>();

            for (int i = 0; i < 200; i++)
            {
                diagramLevelItemsSource1.Add(new LineDiagramLevelItem
                {
                    Value = 0,
                    SetColor = 1,
                    SetHighlightMark = false,
                    ToolTipText = $"A0 Value: {i}"
                });

                diagramLevelItemsSource2.Add(new LineDiagramLevelItem
                {
                    Value = 0,
                    SetColor = 2,
                    SetHighlightMark = false,
                    ToolTipText = $"A1 Value: {i}"
                });

                diagramLevelItemsSource3.Add(new LineDiagramLevelItem
                {
                    Value = 0,
                    SetColor = 3,
                    SetHighlightMark = false,
                    ToolTipText = $"A2 Value: {i}"
                });

                diagramLevelItemsSource4.Add(new LineDiagramLevelItem
                {
                    Value = 0,
                    SetColor = 4,
                    SetHighlightMark = false,
                    ToolTipText = $"A3 Value: {i}"
                });
            }

            this._viewModel.MeasurementValues1 = diagramLevelItemsSource1;
            this._viewModel.MeasurementValues2 = diagramLevelItemsSource2;
            this._viewModel.MeasurementValues3 = diagramLevelItemsSource3;
            this._viewModel.MeasurementValues4 = diagramLevelItemsSource4;

            this._timer.Interval = 10;
            this._timer.Elapsed += this.Timer_Elapsed;

            var setting = UserSettingsLoaderHelper.Load();
   
            var divItem = UartConnection.GetDivisorValueResolution(setting.DivisorValueResolution);
            
            // this._divisorMultiplicator = setting.DivisorMultiplicator;
            // this._divisorResolution = divItem.Resolution;
            VoltageCalculateHelper.SetDivisorAndMultiplicator(
                divItem.Resolution,
                setting.DivisorMultiplicator);
        }

        public void Start() => this._timer.Start();

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this._viewModel.RawValue1 = this._uartConnection.RawValue1;
            this._viewModel.RawValue2 = this._uartConnection.RawValue2;
            this._viewModel.RawValue3 = this._uartConnection.RawValue3;
            this._viewModel.RawValue4 = this._uartConnection.RawValue4;

            this._viewModel.ResultValue1 = VoltageCalculateHelper.RawToVoltage(this._uartConnection.RawValue1);
            this._viewModel.ResultValue2 = VoltageCalculateHelper.RawToVoltage(this._uartConnection.RawValue2);
            this._viewModel.ResultValue3 = VoltageCalculateHelper.RawToVoltage(this._uartConnection.RawValue3);
            this._viewModel.ResultValue4 = VoltageCalculateHelper.RawToVoltage(this._uartConnection.RawValue4);

            if (this._index < this._viewModel.MeasurementValues1.Count)
            {
                this._viewModel.MeasurementValues1[this._index].Value = this._viewModel.ResultValue1;
                this._viewModel.MeasurementValues2[this._index].Value = this._viewModel.ResultValue2;
                this._viewModel.MeasurementValues3[this._index].Value = this._viewModel.ResultValue3;
                this._viewModel.MeasurementValues4[this._index].Value = this._viewModel.ResultValue4;
                this._viewModel.MeasurementValueIndex = this._index;
                this._index++;
            }
            else
            {
                this._index = 0;
            }
        }

        internal static void SetDivisor(UpdateDivisorMessage divisorValues)
        {
            var divItem = UartConnection.GetDivisorValueResolution(divisorValues.DivisorResolution);
            VoltageCalculateHelper.SetDivisorAndMultiplicator(divItem.Resolution, divisorValues.Multiplicator);
        }

        internal void Stop() => this._timer.Stop();
    }
}