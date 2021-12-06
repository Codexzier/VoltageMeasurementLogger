using Codexzier.Wpf.ApplicationFramework.Commands;
using Codexzier.Wpf.ApplicationFramework.Components.Ui.EventBus;
using Codexzier.Wpf.ApplicationFramework.Views.Base;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using VoltageMeasurementLogger.Components.Log;
using VoltageMeasurementLogger.UserControls.LineDiagram;
using VoltageMeasurementLogger.Views.LogData;

namespace VoltageMeasurementLogger.Views.LogDataResult
{
    /// <summary>
    /// Interaction logic for LogDataResultView.xaml
    /// </summary>
    public partial class LogDataResultView : UserControl
    {
        private readonly LogDataResultViewModel _viewModel;

        public LogDataResultView()
        {
            this.InitializeComponent();

            this._viewModel = (LogDataResultViewModel)this.DataContext;

            EventBusManager.Register<LogDataResultView, BaseMessage>(this.BaseMessageEvent);
        }

        private void BaseMessageEvent(IMessageContainer obj)
        {
            if (obj.Content is FileItem fileItem)
            {
                if(LogManager.GetInstance().IsOn)
                {
                    SimpleStatusOverlays.Show("Warnung", "The log is on. Please take off the log!");
                    return;
                }

                var data = LogManager.GetInstance().GetLogs(fileItem.Filename);

                var list = new List<LineDiagramLevelItem>();
                var lineNr = 1;

                if (data.Any())
                {
                    this._viewModel.MinValue = double.MaxValue;
                    this._viewModel.MaxValue = double.MinValue;
                    foreach (var item in data)
                    {
                        if (item.NumericContent > this._viewModel.MaxValue)
                        {
                            this._viewModel.MaxValue = item.NumericContent;
                        }

                        if (item.NumericContent < this._viewModel.MinValue)
                        {
                            this._viewModel.MinValue = item.NumericContent;
                        }

                        list.Add(new LineDiagramLevelItem
                        {
                            Value = item.NumericContent,
                            MinValue = item.NumericContent,
                            MaxValue = item.NumericContent,
                            Date = item.Written,
                            Nr = lineNr
                        });
                        lineNr++;
                    }
                }
                else
                {
                    this._viewModel.MinValue = 0d;
                    this._viewModel.MaxValue = 0d;
                }

                this._viewModel.AverageValue = list.Count == 0 ? 0 : list.Average(a => a.Value);

                this._viewModel.CountMeasures = list.Count;
                this._viewModel.Values = list;
            }
        }
    }
}
