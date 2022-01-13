using Codexzier.Wpf.ApplicationFramework.Commands;
using Codexzier.Wpf.ApplicationFramework.Components.Ui.EventBus;
using Codexzier.Wpf.ApplicationFramework.Views.Base;
using System.Timers;
using System.Windows.Controls;
using VoltageMeasurementLogger.Components;
using VoltageMeasurementLogger.Components.ArduinoConnection;
using System.Linq;

namespace VoltageMeasurementLogger.Views.DivisorSetup
{
    public partial class DivisorSetupView : UserControl
    {
        private readonly DivisorSetupViewModel _viewModel;
        private UartConnection _uartConnection;
        private readonly Timer _timer = new();

        public DivisorSetupView()
        {
            this.InitializeComponent();

            this._viewModel = (DivisorSetupViewModel)this.DataContext;

            this._viewModel.CommandDivisorSetupCancel = new ButtonCommandDivisorSetupCancel();
            this._viewModel.CommandDivisorSetupAccept = new ButtonCommandDivisorSetupAccept(this._viewModel);

            EventBusManager.Register<DivisorSetupView, BaseMessage>(this.BaseMessageEvent);

            this._timer.Interval = 10;
            this._timer.Elapsed += this.Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            

            this._viewModel.DivisorValue = this._uartConnection.RawValue1;
            int resolutionValue = this._viewModel.SelectedDivisorResolution.Resolution;

            if (this._viewModel.MultiplikatorAutoSet)
            {
                this._viewModel.DivisorMultiplikator = 5f / ((float)resolutionValue / (float)this._viewModel.DivisorValue);
            }

            double result = (float)resolutionValue / (float)this._viewModel.DivisorValue * this._viewModel.DivisorMultiplikator;

            this._viewModel.CalculateResult = $"{result:N5} = {resolutionValue} / {this._viewModel.DivisorValue} * {this._viewModel.DivisorMultiplikator}";
        }

        private void BaseMessageEvent(IMessageContainer obj)
        {
            this._uartConnection = UartConnection.GetInstance();

            if (!this._uartConnection.IsOpen)
            {
                SimpleStatusOverlays.Show("Info", "Start connection to arduino!");

                EventBusManager.CloseView<DivisorSetupView>(99);
            }

            var setting = UserSettingsLoaderHelper.Load();

            this._viewModel.DivisorMultiplikator = setting.DivisorMultiplikator;

            DivisorResolutionItem refItem = null;
            if(!string.IsNullOrEmpty(setting.DivisorValueResolution))
            {
                var divItem = UartConnection.GetDivisorValueResolution(setting.DivisorValueResolution);
                refItem = this._viewModel.DivisorValueResolutions.FirstOrDefault(f => f.ToString() == divItem.ToString());
            }

            if (refItem == null)
            {
                refItem = this._viewModel.DivisorValueResolutions.First();
            }

            this._viewModel.SelectedDivisorResolution = refItem;

            this._timer.Start();
        }
    }
}
