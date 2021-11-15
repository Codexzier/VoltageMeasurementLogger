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
        }
    }
}