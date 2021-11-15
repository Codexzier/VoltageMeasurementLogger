using Codexzier.Wpf.ApplicationFramework.Controls.Diagram;
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

namespace VoltageMeasurementLogger.UserControls.LineDiagram
{
    /// <summary>
    /// Interaction logic for LineDiagramControl.xaml
    /// </summary>
    public partial class LineDiagramControl : UserControl
    {
        public double Scale
        {
            get => (double)this.GetValue(ScaleProperty);
            set => this.SetValue(ScaleProperty, value);
        }


        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScaleProperty =
            DependencyProperty.RegisterAttached(
                "Scale",
                typeof(double),
                typeof(LineDiagramControl),
                new PropertyMetadata(1.0d, UpdateDiagram));

        public List<DiagramLevelItem> DiagramLevelItemsSource
        {
            get => (List<DiagramLevelItem>)this.GetValue(DiagramLevelItemsSourceProperty);
            set => this.SetValue(DiagramLevelItemsSourceProperty, value);
        }

        public static readonly DependencyProperty DiagramLevelItemsSourceProperty =
            DependencyProperty.RegisterAttached("DiagramLevelItemsSource",
                typeof(List<DiagramLevelItem>),
                typeof(LineDiagramControl),
                new PropertyMetadata(new List<DiagramLevelItem>(), UpdateDiagram));

        private static void UpdateDiagram(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LineDiagramControl control)
            {
                SetValueToRects(control);
            }
        }

        public LineDiagramControl()
        {
            InitializeComponent();
        }

        // TODO: Not sure, but I have an idea.
        private readonly IList<LineItem> _barItems = new List<LineItem>();

        private static void SetValueToRects(LineDiagramControl control)
        {
            if (control.DiagramLevelItemsSource == null)
            {
                return;
            }

            if (control.ActualWidth == 0d || control.ActualHeight == 0d)
            {
                control.RenderSize = new Size(control.Width, control.Height);
            }

            control._barItems.Clear();
            control.SimpleDiagram.Children.Clear();

            var heightScale = control.ActualHeight / 200d;

            if(double.IsNaN(heightScale))
            {
                heightScale = 100;
            }

            control.OneHundred.Margin = new Thickness(0, 0, 0, 100 / control.Scale * heightScale);
            control.OneHundredText.Margin = new Thickness(0, 0, 0, 100 / control.Scale * heightScale);

            var widthPerResult = (control.ActualWidth - 20) / control.DiagramLevelItemsSource.Count;
            if(double.IsNaN(widthPerResult))
            {
                widthPerResult = (200 - 20) / control.DiagramLevelItemsSource.Count;
            }

            var path = new Path();
            path.Width = widthPerResult * control.DiagramLevelItemsSource.Count;
            path.Height = heightScale;
            path.Margin = new Thickness(0);
            path.HorizontalAlignment = HorizontalAlignment.Left;
            path.VerticalAlignment = VerticalAlignment.Bottom;
            path.Stroke = new SolidColorBrush(Color.FromArgb(255, 160, 200, 219));
            path.StrokeThickness = 1;
            

            var pathFigure = new PathFigure();
            pathFigure.StartPoint = new Point(0, control.ActualHeight);

            foreach (var item in control.DiagramLevelItemsSource)
            {
                var heightValue = item.Value / control.Scale * heightScale;

                if (heightValue < 0)
                {
                    heightValue = 0;
                }

                var barItem = new LineItem(
                    widthPerResult * control.DiagramLevelItemsSource.IndexOf(item),
                    heightValue,
                    item.ToolTipText,
                    item.Value,
                    item.SetHighlightMark,
                    item.SetColor);


                var lineSegment = new LineSegment(
                    new Point(widthPerResult * control.DiagramLevelItemsSource.IndexOf(item), item.Value), true);

                pathFigure.Segments.Add(lineSegment);

                control.SimpleDiagram.Children.Add(barItem.Point);
                control._barItems.Add(barItem);
            }

            var pg = new PathGeometry();
            pg.Figures.Add(pathFigure);

            path.Data = pg;
            control.SimpleDiagram.Children.Add(path);
        }
    }
}
