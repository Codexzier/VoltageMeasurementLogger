using Codexzier.Wpf.ApplicationFramework.Views.Base;
using System.IO;
using System.Windows.Media;
using VoltageMeasurementLogger.Components.Log;

namespace VoltageMeasurementLogger.Views.Main
{
    internal class ButtonCommandWriteLogOnOff : BaseCommand
    {
        private MainViewModel _viewModel;
        public static bool LogOnOff;

        public ButtonCommandWriteLogOnOff(MainViewModel viewModel) => this._viewModel = viewModel;

        public override void Execute(object parameter)
        {
            if(LogOnOff)
            {
                LogManager.GetInstance().Stop();
                this._viewModel.WriteState = Brushes.Black;
                LogOnOff = false;

                this._viewModel.Filename = FileNameCreator.Create();
                return;
            }

            if(File.Exists($"{LogManager.PathOfLogFiles}{this._viewModel.Filename}"))
            {
                SimpleStatusOverlays.ShowAsk("INFO", $"Log filename '{this._viewModel.Filename}' exist!", this.PressedOk);
                return;
            }

            LogManager.GetInstance().WriteToFile(this._viewModel.Filename);
            this._viewModel.WriteState = Brushes.Green;
            LogOnOff = true;
        }

        private void PressedOk(bool obj)
        {
            LogManager.GetInstance().WriteToFile(this._viewModel.Filename);
            this._viewModel.WriteState = Brushes.Green;
            LogOnOff = true;
        }
    }
}