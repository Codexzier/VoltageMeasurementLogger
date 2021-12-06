using System;
using System.Collections;
using System.IO.Ports;
using System.Linq;
using System.Threading;

namespace SimpleVoltageMeasurement
{
    class Program
    {
        private static int _rawValue;
        private static int _divisorValue = 511;
        private static float _voltage;

        static void Main(string[] args)
        {
            Console.WriteLine("Connect Serial with");
            string[] ports = SerialPort.GetPortNames();

            if(!ports.Any()) {
                Console.WriteLine("No Arduino connected!");
                return;
            }

            for (int index = 0; index < ports.Length; index++) {
                Console.WriteLine($"Index:{index}, Port: {ports[index]}");
            }

            string selectindex = Console.ReadLine();
            if (!int.TryParse(selectindex, out int indexResult)) {
                Console.WriteLine("You can only typ the index number!");
                return;
            }

            if(indexResult >= ports.Length) {
                Console.WriteLine("You can only typ the index number!");
                return;
            }

            var sp = new SerialPort(ports[indexResult], 115200);
            sp.DataReceived += Sp_DataReceived;
            sp.Open();

            while (true) {
                Console.Clear();
                Console.WriteLine($"{_rawValue} / {_divisorValue} * 5.0 = {_voltage:N2} V");
                Thread.Sleep(1000);
            }
        }

        private static void Sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var sp = (SerialPort)sender;

            byte[] buffer = new byte[3];
            if (sp.Read(buffer, 0, 3) != 0) {
                int result = (buffer[0] << 8) | buffer[1];

                var b = new BitArray(new int[] { result });
                bool[] bits = new bool[b.Count];
                b.CopyTo(bits, 0);

                if (bits.Sum(s => s ? 1 : 0) != buffer[2])
                {
                    return;
                }

                _rawValue = result;
                _voltage = result / (float)_divisorValue * 5.0f;
            }
        }
    }
}
