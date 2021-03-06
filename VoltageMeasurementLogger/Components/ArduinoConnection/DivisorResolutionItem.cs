using System.Windows.Controls;

namespace VoltageMeasurementLogger.Components.ArduinoConnection
{
    public class DivisorResolutionItem
    {
        // TODO: eigentlich reichen nur die Anzahl der Bits
        public DivisorResolutionItem(int countOfBits, int resolution, string description)
        {
            this.CountOfBits = countOfBits;
            this.Resolution = resolution;
            this.Description = description;
        }

        public int CountOfBits { get; }
        public int Resolution { get; }
        public string Description { get; }

        public override string ToString() => $"{this.CountOfBits} Bit | {this.Resolution} ({this.Description})";

        public override int GetHashCode() => ToString().GetHashCode();
    }
}