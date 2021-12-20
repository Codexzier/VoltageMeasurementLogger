using System.Windows.Controls;

namespace VoltageMeasurementLogger.Components.ArduinoConnection
{
    public class DivisiorResolutionItem
    {
        // TODO: eigentlich reichen nur die Anzahl der Bits
        public DivisiorResolutionItem(int countOfBits, int resolution, string description): base()
        {
            this.CountOfBits = countOfBits;
            this.Resolution = resolution;
            this.Description = description;
        }

        public int CountOfBits { get; }
        public int Resolution { get; }
        public string Description { get; }

        public override string ToString() => $"{this.CountOfBits} Bit | {this.Resolution} ({this.Description})";
    }
}