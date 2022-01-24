namespace VoltageMeasurementLogger.Components.Log
{
    public class LogValue
    {
        public int Value { get; private set; }
        
        public int Divisor { get; private set; }
        
        public float Multiplicator { get; private set; }

        internal static LogValue Create(string v)
        {
            var val = 0;
            var divisor = 0;
            var mul = 0f;
            if (v.Contains(":"))
            {
                var sa = v.Split(':');

                if (int.TryParse(sa[0], out var s1))
                {
                    val = s1;
                }

                if (int.TryParse(sa[1], out var s2))
                {
                    divisor = s2;
                }

                if (float.TryParse(sa[2], out var s3))
                {
                    mul = s3;
                }
            }

            return new LogValue { Value =  val, Divisor = divisor, Multiplicator = mul };
        }
    }
}
