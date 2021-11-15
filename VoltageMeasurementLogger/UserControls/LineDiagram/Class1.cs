using System;
using System.Collections.Generic;

using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace VoltageMeasurementLogger.UserControls.LineDiagram
{
    class Class1
    {
    }

    public class GenericPathLine2
    {
        private Path _Path = new Path();
        private LineSegment[] _LineSegments;
        private int _SegmentIndex = 0;

        private double _Width = 0.0;
        private double _Height = 0.0;
        private Brush _Brush = Brushes.Black;

        public GenericPathLine2(double width, double height, Brush brush)
        {
            _Width = width;
            _Height = height;
            _Brush = brush;

            CreatePathLine();
        }

        /// <summary>
        /// Erstellt die Linie
        /// </summary>
        public void CreatePathLine()
        {
            PathFigure pf = new PathFigure();
            pf.StartPoint = new Point(0, _Height / 2);

            _LineSegments = new LineSegment[Convert.ToInt32(_Width)];
            for (int i = 0; i < _LineSegments.Length; i++)
            {
                _LineSegments[i] = new LineSegment(
                    new Point(i * (_LineSegments.Length / _Width), _Height / 2),
                    true);
                pf.Segments.Add(_LineSegments[i]);
            }

            PathGeometry pg = new PathGeometry();
            pg.Figures.Add(pf);
            // Path Objekt ausrichten und dem Control hinzufügen.
            _Path.Width = _Width;
            _Path.Height = _Height;
            _Path.Margin = new Thickness(0);
            _Path.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            _Path.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            _Path.Stroke = _Brush;
            _Path.StrokeThickness = 1;
            _Path.Data = pg;
        }
        /// <summary>
        /// Legt den Wert für das aktuelle Segment fest.
        /// </summary>
        /// <param name="value"></param>
        public int SetSegmentValue(double value)
        {
            if (_SegmentIndex >= _LineSegments.Length)
            {
                _SegmentIndex = 0;
            }

            _LineSegments[_SegmentIndex].Point = new Point(
                _SegmentIndex * (_LineSegments.Length / _Width),
                (_Height / 2) + (value * -1));
            _SegmentIndex++;

            return _SegmentIndex - 1;
        }
        /// <summary>
        /// Ruft alle Werte ab und gibt dieses als Liste zurück
        /// </summary>
        /// <returns></returns>
        public List<double> GetValues()
        {
            List<double> list = new List<double>();
            for (int i = 0; i < _LineSegments.Length; i++)
            {
                list.Add(_LineSegments[i].Point.Y);
            }
            return list;
        }
        /// <summary>
        /// Ruft nach der Position den Wert des Segment ab.
        /// </summary>
        /// <param name="pos_x"></param>
        /// <returns></returns>
        public double GetSegmentValue(double pos_x)
        {
            if (pos_x >= _Width || pos_x < 0)
            {
                return 0.0;
            }

            // Es kann sein, dass beim Runden der Höhre Wert Konvertiert wird.
            int pos = Convert.ToInt32(pos_x);
            if (pos == _LineSegments.Length)
            {
                pos--;
            }
            // Die Hälfte der Höhe abziehen
            return (_Height / 2) - _LineSegments[pos].Point.Y;
        }
        /// <summary>
        /// Ruft das Path Control ab.
        /// </summary>
        public Path PathLinieObject
        {
            get { return _Path; }
        }
    }
}
