using Codexzier.Wpf.ApplicationFramework.Controls.Diagram;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VoltageMeasurementLogger.UserControls.LineDiagram
{
    /// <summary>
    /// TODO: Line Diagram kann mit dem Bar Diagramm zusammengelegt werden.
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

        public int CheckIndex
        {
            get => (int)this.GetValue(CheckIndexProperty);
            set => this.SetValue(CheckIndexProperty, value);
        }

        public static readonly DependencyProperty CheckIndexProperty =
            DependencyProperty.RegisterAttached("CheckIndex",
                typeof(int),
                typeof(LineDiagramControl),
                new PropertyMetadata(0, UpdateDiagramOnlyValueByIndex));

        private static void UpdateDiagram(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LineDiagramControl control)
            {
                SetValueToRects(control);
            }
        }

        private static void UpdateDiagramOnlyValueByIndex(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LineDiagramControl control)
            {
                if (control.UpdateLineDiagram)
                {
                    return;
                }

                SetValueByIndex(control);
            }
        }

        private bool UpdateLineDiagram = false;

        private bool DebugOn = false;

        private PathFigure _pathFigure = new PathFigure();
        // TODO: Not sure, but I have an idea.
        private readonly IList<LineItem> _barItems = new List<LineItem>();

        private static void SetValueByIndex(LineDiagramControl control)
        {
            if(control.DebugOn)
            {
                var sb = new StringBuilder();
                foreach (var item in control.DiagramLevelItemsSource)
                {
                    sb.Append($"{item.Value:N2}, ");
                }
                control.DebugInfo.Text = sb.ToString();
            }
    
            double widthPerResult = (control.ActualWidth - 20) / control.DiagramLevelItemsSource.Count;
            double heightScale = control.ActualHeight / 200d;
           
            var m = control._barItems[control.CheckIndex].Point.Margin;
            var sizeEllipse = control._barItems[control.CheckIndex].SizeEllipse;

            double heightValue = control.DiagramLevelItemsSource[control.CheckIndex].Value / control.Scale * heightScale;

            if (heightValue < 0)
            {
                heightValue = 0;
            }

            control._barItems[control.CheckIndex].Point.Margin = new Thickness(m.Left, 0, 0, heightValue - (sizeEllipse / 2));
            control._barItems[control.CheckIndex]
                .LineSegment
                .Point = new Point((widthPerResult * control.CheckIndex) + (sizeEllipse / 2), (heightValue * -1) - (sizeEllipse / 6));

            if(control.CheckIndex == 0)
            {
                control._pathFigure.StartPoint = new Point(0, heightValue * -1);
            }
        }

        private static void SetValueToRects(LineDiagramControl control)
        {
            control.UpdateLineDiagram = true;
            control.DebugInfo.Text = string.Empty;

            if (control.DiagramLevelItemsSource == null)
            {
                if (control.DebugOn)
                {
                    control.DebugInfo.Text = "No data";
                }
                    
                return;
            }

            if (control.ActualWidth == 0d || control.ActualHeight == 0d)
            {
                if (control.DebugOn)
                {
                    control.DebugInfo.Text += "Set RenderSize, ";
                }
                control.RenderSize = new Size(control.Width, control.Height);
            }

            control._barItems.Clear();
            control.SimpleDiagram.Children.Clear();

            double heightScale = control.ActualHeight / 200d;

            if (double.IsNaN(heightScale))
            {
                if (control.DebugOn)
                {
                    control.DebugInfo.Text += "Height is NaN";
                }
                return;
            }

            control.OneHundred.Margin = new Thickness(0, 0, 0, 100 / control.Scale * heightScale);
            control.OneHundredText.Margin = new Thickness(0, 0, 0, 100 / control.Scale * heightScale);

            double widthPerResult = (control.ActualWidth - 20) / control.DiagramLevelItemsSource.Count;
            if (double.IsNaN(widthPerResult))
            {
                if (control.DebugOn)
                {
                    control.DebugInfo.Text += "Width is NaN";
                }
                return;
            }

            bool setStartPoint = true;
            double sizeEllipse = 5d;
            var sb = new StringBuilder();
            foreach (var item in control.DiagramLevelItemsSource)
            {
                if (control.DebugOn)
                {
                    sb.Append($"{item.Value:N2}, ");
                }

                double heightValue = item.Value / control.Scale * heightScale;

                if (heightValue < 0)
                {
                    heightValue = 0;
                }

                double x = widthPerResult * control.DiagramLevelItemsSource.IndexOf(item);

                if (setStartPoint)
                {
                    control._pathFigure.StartPoint = new Point(0, heightValue * -1);
                    setStartPoint = false;
                }

                var lineSegment = new LineSegment(
                    new Point((widthPerResult * control.DiagramLevelItemsSource.IndexOf(item)) + (sizeEllipse / 2), (heightValue * -1) - .5d), true);

                var barItem = new LineItem(
                    x,
                    heightValue,
                    item.ToolTipText,
                    item.Value,
                    item.SetHighlightMark,
                    item.SetColor,
                    sizeEllipse,
                    lineSegment);

                control._pathFigure.Segments.Add(lineSegment);

                control.SimpleDiagram.Children.Add(barItem.Point);
                control._barItems.Add(barItem);
            }
            if (control.DebugOn)
            {
                control.DebugInfo.Text = sb.ToString();
            }

            var pg = new PathGeometry();
            pg.Figures.Add(control._pathFigure);

            var path = new Path
            {
                Width = control.ActualWidth - 20,
                Height = control.ActualHeight,
                Margin = new Thickness(0, 0, 0, (control.ActualHeight * -1) - 1),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom,
                Stroke = new SolidColorBrush(Color.FromArgb(255, 160, 200, 219)),
                StrokeThickness = 1,
                Data = pg
            };

            control.SimpleDiagram.Children.Add(path);
            control.UpdateLineDiagram = false;
        }

        public LineDiagramControl() => this.InitializeComponent();

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e) => SetValueToRects(this);

    }
}
