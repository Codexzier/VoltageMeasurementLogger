using Codexzier.Wpf.ApplicationFramework.Controls.Diagram;

namespace VoltageMeasurementLogger.UserControls.LineDiagram
{
    public class LineDiagramLevelItem : DiagramLevelItem
    {
        public double MaxValue { get; set; }
        public double MinValue { get; set; }
    }
}
