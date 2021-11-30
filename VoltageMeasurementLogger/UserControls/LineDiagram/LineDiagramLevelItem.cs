using Codexzier.Wpf.ApplicationFramework.Controls.Diagram;
using System;

namespace VoltageMeasurementLogger.UserControls.LineDiagram
{
    public class LineDiagramLevelItem : DiagramLevelItem
    {
        public long Nr { get; set; }
        public double MaxValue { get; set; }
        public double MinValue { get; set; }
        public DateTime Date { get; set; }
    }
}
