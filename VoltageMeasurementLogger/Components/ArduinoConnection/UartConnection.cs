using System.IO.Ports;

namespace VoltageMeasurementLogger.Components.ArduinoConnection
{
    public class UartConnection
    {
        public UartConnection()
        {

        }

        public void ConnectTo(string portname, int baud = 9600)
        {
            var sp = new SerialPort(portname, baud);
            sp.DataReceived += this.Sp_DataReceived;
            sp.Open();
        }

        private void Sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var sp = (SerialPort)sender; 
            byte[] buffer = new byte[2];
            if (sp.Read(buffer, 0, 2) != 0)
            {
                this.RawValue = (buffer[0] << 8) | buffer[1];
            }
        }

        public int RawValue { get; private set; }
    }
}
