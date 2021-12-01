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
        private int _index = 0;
        private float _offsetValue = 1024;
        private UartConnection _uartConnection;

        public LineDiagramHelper(MonitorLogViewModel viewModel)
        {
            this._viewModel = viewModel;
            this._uartConnection = UartConnection.GetInstance();
            this.Init();
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
            this._timer.Elapsed += this._timer_Elapsed;
        }

        public void Start() => this._timer.Start();

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this._viewModel.RawValue = this._uartConnection.RawValue;
            this._viewModel.VoltageValue = this._viewModel.RawValue / this._offsetValue * 10.0f;

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

        internal void SetOffset(float offsetValue) => this._offsetValue = offsetValue;

        internal void Stop() => this._timer.Stop();
    }
}