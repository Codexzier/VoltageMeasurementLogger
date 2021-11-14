using Codexzier.Wpf.ApplicationFramework.Controls.GameTree;
using Codexzier.Wpf.ApplicationFramework.Views.Base;
using System.Collections.ObjectModel;

namespace VoltageMeasurementLogger.Views.SecondTab
{
    internal class SecondTabViewModel : BaseViewModel
    {
        private ObservableCollection<GameTreeItem> _gameTreeItems;

        public ObservableCollection<GameTreeItem> GameTreeItems
        {
            get => this._gameTreeItems;
            set
            {
                this._gameTreeItems = value;
                this.OnNotifyPropertyChanged(nameof(this.GameTreeItems));
            }
        }
    }
}