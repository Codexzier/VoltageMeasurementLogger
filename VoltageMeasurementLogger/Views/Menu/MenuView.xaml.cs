using Codexzier.Wpf.ApplicationFramework.Commands;
using Codexzier.Wpf.ApplicationFramework.Components.Ui.EventBus;
using Codexzier.Wpf.ApplicationFramework.Views.Base;
using VoltageMeasurementLogger.Views.SecondTab;

namespace VoltageMeasurementLogger.Views.Menu
{
    public partial class MenuView
    {
        private readonly MenuViewModel _viewModel;

        public MenuView()
        {
            this.InitializeComponent();

            this._viewModel = (MenuViewModel)this.DataContext;

            this._viewModel.CommandOpenMain = new ButtonCommandOpenMain();
            this._viewModel.CommandOpenSecond = new ButtonCommandOpenSecond();
        }
    }

    public class ButtonCommandOpenSecond : BaseCommand
    {
        public override void Execute(object parameter)
        {



            if (EventBusManager.IsViewOpen<SecondTabView>(1)) return;

            EventBusManager.OpenView<SecondTabView>(1);
            EventBusManager.Send<SecondTabView, BaseMessage>(new BaseMessage(""), 1);
        }
    }
}