using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VoltageMeasurementLogger.UserControls.LineDiagram
{
    internal class LineItem
    {
        public Ellipse Point { get; }
        public LineSegment LineSegment { get; }
        public double SizeEllipse { get; }

        // TODO: Min Max Werten ausgaben. Umstellung von Ellipse auf Border erforderlich
        // Durch die Vergabe von min Max können zusammengefasst mehrere Messungen zusammengeführt werden
        // und dessen minimalen und maximalen Wert zu einem Strich dargestellt werden.
        public double MaxValue { get; set; }
        public double MinValue { get; set; }

        public LineItem(
            double widthPerResultDistanceToLeft, 
            double heightValue, 
            string toolTipText, 
            double value, 
            bool setHighlightMark, 
            int setColor,
            double sizeEllipse,
            LineDiagramControl control)
        {
            this.LineSegment = new LineSegment(new Point(widthPerResultDistanceToLeft + (sizeEllipse / 2), (heightValue * -1) - .5d), true);
            control._pathFigure.Segments.Add(this.LineSegment);

            this.SizeEllipse = sizeEllipse;

            // TODO: Ist nun doppelt vorhanden. Bereits in BarItem implementiert
            var barColorNormal = new SolidColorBrush(this.SetUpValueIfOverHundred(value, setColor));
            var barColorHighlighted = new SolidColorBrush(Color.FromArgb(255, 160, 200, 219));


            // TODO: Ist fast doppelt vorhanden.
            // Wegen Ellipse. Soll aber nur in der Höhe des Werts angezeigt werden.
            this.Point = new Ellipse
            {
                Fill = barColorNormal,
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = sizeEllipse,
                Height = sizeEllipse,
                MaxWidth = sizeEllipse,
                MaxHeight = sizeEllipse,
                ToolTip = toolTipText,
                Margin = new Thickness(widthPerResultDistanceToLeft, 0, 0, heightValue - (sizeEllipse / 6)), 
                RenderSize = new Size(sizeEllipse, sizeEllipse)
        };

            //this.Point.RenderSize = new Size(sizeEllipse, sizeEllipse);
            this.Point.Measure(new Size(sizeEllipse, sizeEllipse));

            var ttt = (UIElement)this.Point;
            Debug.WriteLine($"DesiredSize={ttt.DesiredSize}, RenderSize={ttt.RenderSize}");
            

            // TODO: Ist nun doppelt vorhanden. Bereits in BarItem implementiert
            if (setHighlightMark)
            {
                this.Point.StrokeThickness = 2;
                this.Point.Stroke = new SolidColorBrush(Color.FromArgb(255, 200, 250, 219));
            }

            control.SimpleDiagram.Children.Add(this.Point);



            // TODO: Ist nun doppelt vorhanden. Bereits in BarItem implementiert
            //this.Point.MouseEnter += (e, r) =>
            //{
            //    if (!(e is Rectangle subRect))
            //    {
            //        return;
            //    }

            //    subRect.Fill = barColorHighlighted;
            //};

            //// TODO: Ist nun doppelt vorhanden. Bereits in BarItem implementiert
            //this.Point.MouseLeave += (e, r) =>
            //{
            //    if (!(e is Ellipse subRect))
            //    {
            //        return;
            //    }

            //    subRect.Fill = barColorNormal;
            //};
        }

        internal void SetPointMargin(double heightValue, double sizeEllipse)
        {
            var m = this.Point.Margin;
            this.Point.Margin = new Thickness(m.Left, 0, 0, heightValue - (sizeEllipse / 2));
        }

        // TODO: Ist nun doppelt vorhanden. Bereits in BarItem implementiert
        private Color SetUpValueIfOverHundred(double value, int setColor)
        {
            if (value < 100)
            {
                return ColorSetup(colorNr: setColor);
            }

            var red = value - 100 + 138;
            if (red >= 256)
            {
                red = 255;
            }

            return ColorSetup((byte)red, setColor);

            static Color ColorSetup(byte red = 138, int colorNr = 0)
            {
                return colorNr switch
                {
                    1 => Color.FromArgb(255, 0, 255, 120),
                    2 => Color.FromArgb(255, 128, 255, 255),
                    3 => Color.FromArgb(255, 255, 128, 192),
                    4 => Color.FromArgb(255, 255, 128, 255),
                    _ => Color.FromArgb(255, red, 187, 219)
                };
            }
        }
    }
}