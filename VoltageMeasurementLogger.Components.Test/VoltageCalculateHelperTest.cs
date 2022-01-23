using Microsoft.VisualStudio.TestTools.UnitTesting;
using VoltageMeasurementLogger.Components.Helpers;

namespace VoltageMeasurementLogger.Components.Test
{
    [TestClass]
    public class VoltageCalculateHelperTest
    {
        [TestInitialize]
        public void Init()
        {
            // Reset to init, to simulate started application
            VoltageCalculateHelper.SetDivisorAndMultiplicator(0, 0);
        }
        
        [TestMethod]
        public void CalculateGradesNumbers()
        {
            // arrange
            const int rawValue = 120;
            const int divisor = 12;
            const float multiplicator = 1f;
            VoltageCalculateHelper.SetDivisorAndMultiplicator(divisor, multiplicator);

            // act
            var result = VoltageCalculateHelper.RawToVoltage(rawValue);

            // assert
            Assert.AreEqual(0.1f, result);
        }
        
        [TestMethod]
        public void CalculateDecimalNumbers()
        {
            // arrange
            const int rawValue = 120;
            const int divisor = 12;
            const float multiplicator = 1.1f;
            VoltageCalculateHelper.SetDivisorAndMultiplicator(divisor, multiplicator);

            // act
            var result = VoltageCalculateHelper.RawToVoltage(rawValue);

            // assert
            Assert.AreEqual(0.11000001f, result);
        }
        
        [TestMethod]
        public void CalculateErrorDivisorAndMultiplicatorNotSet()
        {
            // arrange
            const int rawValue = 120;

            // act
            Assert.ThrowsException<VoltageCalculateHelperException>(() =>
                VoltageCalculateHelper.RawToVoltage(rawValue));

            // assert
        }
    }
}