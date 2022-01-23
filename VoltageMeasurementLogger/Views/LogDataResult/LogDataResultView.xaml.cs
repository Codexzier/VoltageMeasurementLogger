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

            this._viewModel.DataGridLogResult = this.DataGridLogResult;

            this._viewModel.CommandLastDeviations = new ButtonCommandLastDeviations(this._viewModel);
            this._viewModel.CommandNextDeviations = new ButtonCommandNextDeviations(this._viewModel);


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

                var list = new List<LogValueItem>();
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

                        if (item.LogValues == null)
                        {
                            list.Add(new LogValueItem
                            {
                                RawValue1 = item.NumericContent,
                                RawValue2 = 0,
                                RawValue3 = 0,
                                RawValue4 = 0,
                                Date = item.Written,
                                Nr = lineNr
                            });
                        }
                        else
                        {
                            list.Add(new LogValueItem
                            {
                                RawValue1 = item.LogValues[0].Value,
                                RawValue2 = item.LogValues[1].Value,
                                RawValue3 = item.LogValues[2].Value,
                                RawValue4 = item.LogValues[3].Value,
                                Date = item.Written,
                                Nr = lineNr
                            });
                        }
                        lineNr++;
                    }
                }
                else
                {
                    this._viewModel.MinValue = 0d;
                    this._viewModel.MaxValue = 0d;
                }

                this._viewModel.AverageValue = list.Count == 0 ? 0 : list.Average(a => a.RawValue1);

                this._viewModel.CountMeasures = list.Count;
                this._viewModel.Values = list;
            }
        }
    }

    internal class ButtonCommandNextDeviations : BaseCommand
    {
        private LogDataResultViewModel _viewModel;

        public ButtonCommandNextDeviations(LogDataResultViewModel viewModel) => this._viewModel = viewModel;

        public override void Execute(object parameter)
        {
            double lastValue = this._viewModel.Values[this._viewModel.LevelItemIndex].RawValue1;
            for (int index = this._viewModel.LevelItemIndex; index < this._viewModel.Values.Count; index++)
            {
                if(IsDeviationViolated(lastValue, this._viewModel.Values[index], this._viewModel.DeviationTolerance))
                {
                    this._viewModel.LevelItemIndex = index;
                    this._viewModel.DataGridLogResult.UpdateLayout();
                    this._viewModel.DataGridLogResult.ScrollIntoView(this._viewModel.DataGridLogResult.SelectedItem);
                    return;
                }

                lastValue = this._viewModel.Values[index].RawValue1;
            }
        }

        internal static bool IsDeviationViolated(double lastValue, LogValueItem item, double tolerance)
        {
            return item.RawValue1 < lastValue - tolerance ||
                   item.RawValue1 > lastValue + tolerance;
        }
    }

    internal class ButtonCommandLastDeviations : BaseCommand
    {
        private LogDataResultViewModel _viewModel;

        public ButtonCommandLastDeviations(LogDataResultViewModel viewModel) => this._viewModel = viewModel;

        public override void Execute(object parameter)
        {
            double lastValue = this._viewModel.Values[this._viewModel.LevelItemIndex].RawValue1;
            for (int index = this._viewModel.LevelItemIndex; index > 0; index--)
            {
                if(ButtonCommandNextDeviations.IsDeviationViolated(lastValue, this._viewModel.Values[index], this._viewModel.DeviationTolerance))
                {
                    this._viewModel.LevelItemIndex = index;
                    this._viewModel.DataGridLogResult.UpdateLayout();
                    this._viewModel.DataGridLogResult.ScrollIntoView(this._viewModel.DataGridLogResult.SelectedItem);
                    return;
                }

                lastValue = this._viewModel.Values[index].RawValue1;
            }
        }
    }
}
