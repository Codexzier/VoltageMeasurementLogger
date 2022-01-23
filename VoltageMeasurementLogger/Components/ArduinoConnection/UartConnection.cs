using System;
using System.Collections;
using System.Diagnostics;
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

        public static DivisorResolutionItem[] GetDivisorValueResolutions() => new[] {
            new DivisorResolutionItem(10, 1024, "Arduino"),
            new DivisorResolutionItem(16, 65535, "ADS1115")
        };

        internal static DivisorResolutionItem GetDivisorValueResolution(string divisorValueResolution)
        {
            foreach (var item in GetDivisorValueResolutions())
            {
                if(item.ToString().Equals(divisorValueResolution))
                {
                    return item;
                }
            }

            return GetDivisorValueResolutions()[0];
        }

        public static UartConnection GetInstance()
        {
            if(_instance == null)
            {
                _instance = new UartConnection();
            }

            return _instance;
        }

        public UartConnectionResult ConnectTo(string portName, int baud = 9600, int divisor = 0)
        {
            this._divisor = divisor;

            if(this._serialPort != null)
            {
                return new UartConnectionResult("Serial Port is in used! Closed the connection before start new one!");
            }

            this._serialPort = new SerialPort(portName, baud);
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
            if(this._serialPort == null || !this._serialPort.IsOpen)
            {
                return;
            }

            try
            {
                byte[] buffer = new byte[9];
                if (this._serialPort.Read(buffer, 0, 9) == 0)
                {
                    return;
                }

                var result1 = (buffer[0] << 8) | buffer[1];
                var bits = GetBits(result1);
                // var b = new BitArray(new [] { result1 });
                // var bits = new bool[b.Count];
                // b.CopyTo(bits, 0);

                var result2 = (buffer[2] << 8) | buffer[3];
                bits += GetBits(result2);
                var result3 = (buffer[4] << 8) | buffer[5];
                bits += GetBits(result3);
                var result4 = (buffer[6] << 8) | buffer[7];
                bits += GetBits(result4);

                //Debug.WriteLine($"{bits.Sum(s => s ? 1 : 0)} != {buffer[8]}");
                if (bits != buffer[8])
                {
                    return;
                }

                this.RawValue1 = result1;
                this.RawValue2 = result2;
                this.RawValue3 = result3;
                this.RawValue4 = result4;

                // TODO Moved to eventhandler
                if (LogManager.GetInstance().IsOn)
                {
                    // TODO: Log all four values and add multiplikator
                    LogManager.GetInstance().WriteLine(this.RawValue1, this._divisor);
                }

                this._lastUpdate = DateTime.Now;
            }
            catch(Exception ex)
            {
                this.UartConnectionErrorEvent?.Invoke(ex.Message);
            }
        }

        private static byte GetBits(int result)
        {
            var b = new BitArray(new [] { result });
            var bits = new bool[b.Count];
            b.CopyTo(bits, 0);
            return (byte)bits.Sum(s => s ? 1 : 0);
        }

        public int RawValue1 { get; private set; } = 0;
        public int RawValue2 { get; private set; } = 0;
        public int RawValue3 { get; private set; } = 0;
        public int RawValue4 { get; private set; } = 0;

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

        public delegate void UartConnectionErrorEventHandler(string message);
        public event UartConnectionErrorEventHandler UartConnectionErrorEvent;
    }
}
