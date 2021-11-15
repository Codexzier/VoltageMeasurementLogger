using Codexzier.Wpf.ApplicationFramework.Views.Base;
using System.Windows.Input;

namespace VoltageMeasurementLogger.Views.Menu
{
    internal class MenuViewModel : BaseViewModel
    {
        private ICommand _commandOpenMain;

        public ICommand CommandOpenMain
        {
            get => this._commandOpenMain;
            set
            {
                this._commandOpenMain = value;
                this.OnNotifyPropertyChanged(nameof(this.CommandOpenMain));
            }
        }
    }
}