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

        public static readonly DependencyProperty ScaleProperty =
            DependencyProperty.RegisterAttached(
                "Scale",
                typeof(double),
                typeof(LineDiagramControl),
                new PropertyMetadata(1.0d, UpdateDiagram));

        public double MultiplyValue
        {
            get => (double)this.GetValue(MultiplyValueProperty);
            set => this.SetValue(MultiplyValueProperty, value);
        }

        public static readonly DependencyProperty MultiplyValueProperty =
            DependencyProperty.RegisterAttached(
                "MultiplyValue", 
                typeof(double), 
                typeof(LineDiagramControl), 
                new PropertyMetadata(1d));

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

        public string LevelLineText
        {
            get => (string)this.GetValue(LevelLineTextProperty);
            set => this.SetValue(LevelLineTextProperty, value);
        }

        public static readonly DependencyProperty LevelLineTextProperty =
            DependencyProperty.RegisterAttached(
                "LevelLineText", 
                typeof(string), 
                typeof(LineDiagramControl), 
                new PropertyMetadata("5.0", UpdateLevelLineText));

        private static void UpdateLevelLineText(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LineDiagramControl control)
            {
                control.TextBlockLevelLineText.Text = control.LevelLineText;
            }
        }

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

        internal PathFigure _pathFigure = new PathFigure();
        private readonly IList<LineItem> _barItems = new List<LineItem>();

        private static void SetValueByIndex(LineDiagramControl control)
        {
            if (control.DebugOn)
            {
                var sb = new StringBuilder();
                foreach (var item in control.DiagramLevelItemsSource)
                {
                    sb.Append($"{item.Value:N2}, ");
                }
                control.DebugInfo.Text = sb.ToString();
            }

            if(control._barItems.Count == 0)
            {
                return;
            }

            double widthPerResult = (control.ActualWidth - 20) / control.DiagramLevelItemsSource.Count;
            double heightScale = control.ActualHeight / 200d;
           
            var m = control._barItems[control.CheckIndex].Point.Margin;
            var sizeEllipse = control._barItems[control.CheckIndex].SizeEllipse;

            double heightValue = control.DiagramLevelItemsSource[control.CheckIndex].Value / control.Scale * heightScale * control.MultiplyValue;

            if (heightValue < 0)
            {
                heightValue = 0;
            }

            control._barItems[control.CheckIndex].SetPointMargin(heightValue, sizeEllipse);
            control._barItems[control.CheckIndex]
                .LineSegment
                .Point = new Point((widthPerResult * control.CheckIndex) + (sizeEllipse / 2), (heightValue * -1) - (sizeEllipse / 6));

            if (control.CheckIndex == 0)
            {
                control._pathFigure.StartPoint = new Point(0, heightValue * -1);
            }

            // set actual Level value and move from left to right
            control.GridActualPathValue.Margin = new Thickness(m.Left + 30, 10, 0, 0);
            control.TextBlockActualSetValue.Text = $"{control.DiagramLevelItemsSource[control.CheckIndex].Value:N1}V";
        }

        private static void SetValueToRects(LineDiagramControl control)
        {
            DebugInfoSetReset(control);

            if (control.DiagramLevelItemsSource == null)
            {
                DebugInfoAppendText(control, "No data");
                return;
            }

            control.UpdateLineDiagram = true;

            if (control.ActualWidth == 0d || control.ActualHeight == 0d)
            {
                if (double.IsNaN(control.Width))
                {
                    return;
                }

                DebugInfoAppendText(control, "Set RenderSize, ");
                control.RenderSize = new Size(control.Width, control.Height);
            }

            control._barItems.Clear();
            control.SimpleDiagram.Children.Clear();

            double heightScale = control.ActualHeight / 200d;
            SetLegendMarkPosition(control, heightScale);

            if (double.IsNaN(heightScale))
            {
                if (control.DebugOn)
                {
                    control.DebugInfo.Text += "Height is NaN";
                }
                return;
            }

            control.OneHundred.Margin = new Thickness(0, 0, 0, 100 / control.Scale * heightScale);
            control.TextBlockLevelLineText.Margin = new Thickness(0, 0, 0, 100 / control.Scale * heightScale);

            double widthPerResult = (control.ActualWidth - 20) / control.DiagramLevelItemsSource.Count;

            bool setStartPoint = true;
            double sizeEllipse = 5d;
            control._pathFigure = new PathFigure();
            var sb = new StringBuilder();
            foreach (var item in control.DiagramLevelItemsSource)
            {
                if (control.DebugOn) { sb.Append($"{item.Value:N2}, "); }

                double heightValue = item.Value / control.Scale * heightScale * control.MultiplyValue;

                if (heightValue < 0) { heightValue = 0; }

                double x = widthPerResult * control.DiagramLevelItemsSource.IndexOf(item);

                if (setStartPoint)
                {
                    control._pathFigure.StartPoint = new Point(0, heightValue * -1);
                    setStartPoint = false;
                }

                var lineItem = new LineItem(
                    x,
                    heightValue,
                    item.ToolTipText,
                    item.Value,
                    item.SetHighlightMark,
                    item.SetColor,
                    sizeEllipse,
                    control);

                control._barItems.Add(lineItem);
            }

            DebugInfoAppendText(control, sb.ToString());

            var pg = new PathGeometry();
            pg.Figures.Add(control._pathFigure);
            var path = CreatePath(control, pg);

            control.SimpleDiagram.Children.Add(path);

            control.UpdateLineDiagram = false;
        }

        private static void SetLegendMarkPosition(LineDiagramControl control, double heightScale)
        {
            var t = new Thickness(0, 0, 0, 100 / control.Scale * heightScale);
            control.OneHundred.Margin = t;
            control.TextBlockLevelLineText.Margin = t;
        }

        private static void DebugInfoAppendText(LineDiagramControl control, string text)
        {
            if (control.DebugOn)
            {
                control.DebugInfo.Text += text;
            }
        }

        private static void DebugInfoSetReset(LineDiagramControl control)
        {
            if (control.DebugOn)
            {
                control.DebugInfo.Text = string.Empty;
            }
        }

        private static Path CreatePath(LineDiagramControl control, PathGeometry pg) => new Path
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

        public LineDiagramControl() => this.InitializeComponent();

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e) => SetValueToRects(this);

    }
}
