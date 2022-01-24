using Codexzier.Wpf.ApplicationFramework.Commands;
using Codexzier.Wpf.ApplicationFramework.Components.Ui.EventBus;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using VoltageMeasurementLogger.Components.Log;

namespace VoltageMeasurementLogger.Views.LogData
{
    public partial class LogDataView
    {
        private readonly LogDataViewModel _viewModel;

        private readonly IList<FileItem> _allFileItems = new List<FileItem>();

        public LogDataView()
        {
            this.InitializeComponent();

            this._viewModel = (LogDataViewModel)this.DataContext;

            EventBusManager.Register<LogDataView, BaseMessage>(this.BaseMessageEvent);

            this._viewModel.CommandSelectedFileItem = new SelectChangedCommandSelectedFileItem();
        }

        private void BaseMessageEvent(IMessageContainer obj)
        {
            this._allFileItems.Clear();

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
            var searchResult = new List<FileItem>();

            if(sender is TextBox tb && !string.IsNullOrEmpty(tb.Text))
            {
                searchResult.AddRange(
                    this._allFileItems
                        .Where(item => item
                            .Filename.ToLower()
                            .Contains(tb.Text.ToLower())));

                this._viewModel.Files = new ObservableCollection<FileItem>(searchResult);
                return;
            }

            this._viewModel.Files = new ObservableCollection<FileItem>(this._allFileItems);
        }
    }
}
