using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VoltageMeasurementLogger.Views.MonitorLog
{
    /// <summary>
    /// Interaction logic for MonitorLogView.xaml
    /// </summary>
    public partial class MonitorLogView : UserControl
    {
        private MonitorLogViewModel _viewModel;

        public MonitorLogView()
        {
            this.InitializeComponent();

            this._viewModel = (MonitorLogViewModel)this.DataContext;
        }
    }
}
