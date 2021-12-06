using System.Collections;
using System.IO.Ports;
using System.Linq;
using VoltageMeasurementLogger.Components.Log;

namespace VoltageMeasurementLogger.Components.ArduinoConnection
{
    public class UartConnection
    {
        private static UartConnection _instance;
        private SerialPort _serialPort;
        private int _divisor;

        private UartConnection() { }

        public static UartConnection GetInstance()
        {
            if(_instance == null)
            {
                _instance = new UartConnection();
            }

            return _instance;
        }

        public UartConnectionResult ConnectTo(string portname, int baud = 9600, int divisor = 0)
        {
            this._divisor = divisor;

            if(this._serialPort != null)
            {
                return new UartConnectionResult("Serial Port is in used! Closed the connection before start new one!");
            }

            this._serialPort = new SerialPort(portname, baud);
            this._serialPort.DataReceived += this.Sp_DataReceived;
            this._serialPort.Open();

            return new UartConnectionResult();
        }

        public UartConnectionResult Close()
        {
            if (this._serialPort == null || !this._serialPort.IsOpen)
            {
                return new UartConnectionResult("Connection is not open");
            }

            this._serialPort.Close();
            this._serialPort = null;

            return new UartConnectionResult();
        }

        private void Sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] buffer = new byte[3];
            if (this._serialPort.Read(buffer, 0, 3) != 0)
            {
                var result = (buffer[0] << 8) | buffer[1];

                var b = new BitArray(new int[] { result });
                var bits = new bool[b.Count];
                b.CopyTo(bits, 0);

                if(bits.Sum(s => s ? 1:0) != buffer[2])
                {
                    return;
                }

                this.RawValue = result;

                // TODO Moved to eventhandler
                if(LogManager.GetInstance().IsOn)
                {
                    LogManager.GetInstance().WriteLine(this.RawValue, this._divisor);
                }
            }
        }

        public int RawValue { get; private set; }

        public bool IsOpen => _serialPort == null ? false : _serialPort.IsOpen;
    }
}
