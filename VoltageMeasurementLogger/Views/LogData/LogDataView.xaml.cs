using Codexzier.Wpf.ApplicationFramework.Commands;
using Codexzier.Wpf.ApplicationFramework.Components.Ui.EventBus;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using VoltageMeasurementLogger.Components.Log;

namespace VoltageMeasurementLogger.Views.LogData
{
    /// <summary>
    /// Interaction logic for LogDataView.xaml
    /// </summary>
    public partial class LogDataView : UserControl
    {
        public readonly LogDataViewModel _viewModel;

        private IList<FileItem> _allFileItems = new List<FileItem>();

        public LogDataView()
        {
            this.InitializeComponent();

            this._viewModel = (LogDataViewModel)this.DataContext;

            EventBusManager.Register<LogDataView, BaseMessage>(this.BaseMessageEvent);

            this._viewModel.CommandSelectedFileItem = new SelectChangedCommandSelectedFileItem(this._viewModel);
        }

        private void BaseMessageEvent(IMessageContainer obj)
        {
            _allFileItems.Clear();

            if (!Directory.Exists(LogManager.PathOfLogFiles))
            {
                _allFileItems.Add(new FileItem());

                this._viewModel.Files = new ObservableCollection<FileItem>(_allFileItems);

                return;
            }

            foreach (var filename in Directory.GetFiles(LogManager.PathOfLogFiles))
            {
                _allFileItems.Add(new FileItem(new FileInfo(filename)));
            }
            this._viewModel.Files = new ObservableCollection<FileItem>(_allFileItems);
        }

        private void TextBoxSearch_KeyUp(object sender, KeyEventArgs e)
        {
            var listSearchresult = new List<FileItem>();

            if(sender is TextBox tb && !string.IsNullOrEmpty(tb.Text))
            {
                foreach (var item in _allFileItems)
                {
                    if (item.Filename.ToLower().Contains(tb.Text.ToLower()))
                    {
                        listSearchresult.Add(item);
                    }
                }

                this._viewModel.Files = new ObservableCollection<FileItem>(listSearchresult);
                return;
            }

            this._viewModel.Files = new ObservableCollection<FileItem>(this._allFileItems);
        }
    }
}
