namespace VoltageMeasurementLogger.Components.Helpers
{
    public static class VoltageCalculateHelper
    {
        private static int _divisor;
        private static float _multiplicator;
        
        public static float RawToVoltage(int rawValue)
        {
            if (_multiplicator == 0)
            {
                throw new VoltageCalculateHelperException("multiplicator not set!");
            }
            
            if (_divisor == 0)
            {
                throw new VoltageCalculateHelperException("Divisor not set!");
            }
            
            var result = _divisor / (float)rawValue * _multiplicator;

            if(float.IsInfinity(result))
            {
                return 0.0f;
            }

            return result;
        }

        public static void SetDivisorAndMultiplicator(int resolution, float multiplicator)
        {
            _divisor = resolution;
            _multiplicator = multiplicator;
        }
    }
}