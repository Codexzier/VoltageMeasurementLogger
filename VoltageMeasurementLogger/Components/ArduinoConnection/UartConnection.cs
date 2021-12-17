using System;
using System.Collections;
using System.IO.Ports;
using System.Linq;
using System.Timers;
using VoltageMeasurementLogger.Components.Log;

namespace VoltageMeasurementLogger.Components.ArduinoConnection
{
    public class UartConnection : IDisposable
    {
        private static UartConnection _instance;
        private SerialPort _serialPort;
        private int _divisor;
        private readonly Timer _activity;
        private DateTime _lastUpdate;

        private UartConnection() {
            this._activity = new();
            this._activity.Interval = 1000;
            this._activity.Elapsed += this.Activity_Elapsed;
        }

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
            this._serialPort.DiscardInBuffer();

            this._activity.Start();

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

            this._activity.Stop();

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

                if (bits.Sum(s => s ? 1 : 0) != buffer[2])
                {
                    return;
                }

                this.RawValue = result;

                // TODO Moved to eventhandler
                if(LogManager.GetInstance().IsOn)
                {
                    LogManager.GetInstance().WriteLine(this.RawValue, this._divisor);
                }

                this._lastUpdate = DateTime.Now;
            }
        }

        public int RawValue { get; private set; }

        public bool IsOpen => _serialPort == null ? false : _serialPort.IsOpen;

        public void Dispose() => this.Close();

        public bool IsIncomingDataActive { get; private set; }

        private void Activity_Elapsed(object sender, ElapsedEventArgs e)
        {
            if(this._lastUpdate < DateTime.Now.AddSeconds(-2))
            {
                this.NoIncomingDataEvent?.Invoke();
                this.IsIncomingDataActive = false;
                return;
            }

            this.IsIncomingDataActive = true;
        }

        public delegate void NoIncomingDataEventHandler();
        public event NoIncomingDataEventHandler NoIncomingDataEvent;
    }
}
