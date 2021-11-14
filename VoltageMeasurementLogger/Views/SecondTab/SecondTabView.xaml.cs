using Codexzier.Wpf.ApplicationFramework.Commands;
using Codexzier.Wpf.ApplicationFramework.Components.Ui.EventBus;
using Codexzier.Wpf.ApplicationFramework.Controls.GameTree;
using Codexzier.Wpf.ApplicationFramework.Views.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace VoltageMeasurementLogger.Views.SecondTab
{
    /// <summary>
    /// Interaction logic for SecondTabView.xaml
    /// </summary>
    public partial class SecondTabView : UserControl
    {
        private readonly SecondTabViewModel _viewModel;
        public SecondTabView()
        {
            InitializeComponent();

            this._viewModel = (SecondTabViewModel)this.DataContext;

            EventBusManager.Register<SecondTabView, BaseMessage>(this.BaseMessageReceiver);
        }

        private async void BaseMessageReceiver(IMessageContainer obj)
        {
            Debug.WriteLine($"ActivityOn, Channel {SimpleStatusOverlays.ActivityChannel}");
            SimpleStatusOverlays.ActivityOn();

            await Task.Delay(500);

            Debug.WriteLine($"ActivityOff, Channel {SimpleStatusOverlays.ActivityChannel}");
            SimpleStatusOverlays.ActivityOff();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this._viewModel == null)
            {
                return;
            }

            var list = new List<GameTreeItem>();
            for (int i = 0; i < Math.Round(e.NewValue); i++)
            {
                list.Add(new GameTreeItem()
                {
                    GameId = 1,
                    ItemPositionHorizontal = i,
                    ItemPositionVertikal = 0,
                    Player1 = new PlayerItem { Name = $"Player {i}", Score = i * 7 },
                    Player2 = new PlayerItem { Name = $"Player {i * 2}", Score = i * 17 }
                });
            }

            this._viewModel.GameTreeItems = new ObservableCollection<GameTreeItem>(list);
        }
    }
}
