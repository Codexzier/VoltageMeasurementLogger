using Codexzier.Wpf.ApplicationFramework.Views.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace VoltageMeasurementLogger.Views.LogData
{
    public class LogDataViewModel : BaseViewModel
    {
        private string _search;
        private ObservableCollection<FileItem> _files;
        private FileItem _selectedFileItem;
        private ICommand _commandSelectedFileItem;

        public string Search
        {
            get => this._search;
            set
            {
                this._search = value;
                this.OnNotifyPropertyChanged(nameof(this.Search));
            }
        }

        public ObservableCollection<FileItem> Files
        {
            get => this._files;
            set
            {
                this._files = value;
                this.OnNotifyPropertyChanged(nameof(this.Files));
            }
        }

        public FileItem SelectedFileItem
        {
            get => this._selectedFileItem;
            set
            {
                if (value != null || !this._selectedFileItem.Equals(value))
                {
                    this.CommandSelectedFileItem?.Execute(value);
                }

                this._selectedFileItem = value;
                this.OnNotifyPropertyChanged(nameof(this.SelectedFileItem));
            }
        }

        public ICommand CommandSelectedFileItem
        {
            get => _commandSelectedFileItem;
            set
            {
                _commandSelectedFileItem = value;
                this.OnNotifyPropertyChanged(nameof(this.CommandSelectedFileItem));
            }
        }

 
    }
}