using Codexzier.Wpf.ApplicationFramework.Commands;
using Codexzier.Wpf.ApplicationFramework.Components.Ui.EventBus;
using Codexzier.Wpf.ApplicationFramework.Views.Base;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace VoltageMeasurementLogger.Views.Main
{
    public partial class MainView
    {
        private readonly MainViewModel _viewModel;
        public MainView()
        {
            this.InitializeComponent();

            this._viewModel = (MainViewModel)this.DataContext;
            this._viewModel.CommandMessageBox = new ButtonCommandTest();
            this._viewModel.CommandAskMessageBox = new ButtonCommandAskMessageBox();

            EventBusManager.Register<MainView, BaseMessage>(this.BaseMessageEvent);
        }

        private async void BaseMessageEvent(IMessageContainer obj)
        {
            // do things
            SimpleStatusOverlays.ActivityOn();

            await Task.Delay(200);

            SimpleStatusOverlays.ActivityOff();
        }
    }

    public class ButtonCommandAskMessageBox : BaseCommand
    {
        public override void Execute(object parameter)
        {
            base.Execute(parameter);

            SimpleStatusOverlays.ShowAsk("Title", "i am asking you. Ok or cancel?", pressOk =>
            {
                // doing things
                if (pressOk)
                {
                }
            });
        }
    }

    public class ButtonCommandTest : BaseCommand
    {
        public override void Execute(object parameter)
        {
            base.Execute(parameter);

            SimpleStatusOverlays.Show("tip", "my message");
        }
    }
}