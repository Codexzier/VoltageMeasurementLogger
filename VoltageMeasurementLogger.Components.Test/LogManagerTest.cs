using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VoltageMeasurementLogger.Components.Log;

namespace VoltageMeasurementLogger.Components.Test
{
    [TestClass]
    public class LogManagerTest
    {
        [TestInitialize]
        public void Init()
        {
            if (!Directory.Exists(LogManager.PathOfLogFiles)){
                return;
            }

            var files = Directory.GetFiles(LogManager.PathOfLogFiles);
            foreach (var item in files)
            {
                File.Delete(item);
            }

            Directory.Delete(LogManager.PathOfLogFiles);
        }

        [TestMethod]
        public void CreateFile()
        {
            // arrange
            string filename = "Test";

            // act
            LogManager.GetInstance().WriteToFile(filename);

            // assert
            string[] files = Directory.GetFiles(LogManager.PathOfLogFiles);
            Assert.AreEqual($"{LogManager.PathOfLogFiles}{filename}", files.First());
        }

        [TestMethod]
        public void FileExistAndIsEmpty()
        {
            // arrange
            string filename = "Test";
            var fullFilename = $"{LogManager.PathOfLogFiles}{filename}";
            Directory.CreateDirectory(LogManager.PathOfLogFiles);
            var stream = File.Create(fullFilename);
            using (var sw = new StreamWriter(stream))
            {
                sw.WriteLine("this text can be exist!");
                sw.Flush();
            }

            // act
            LogManager.GetInstance().WriteToFile(filename);

            // assert
            string[] files = Directory.GetFiles(LogManager.PathOfLogFiles);
            string fileContent = File.ReadAllText(files.First());
            Assert.AreEqual(string.Empty, fileContent);
        }

        [TestMethod]
        public void WriteAddLineStringValue()
        {
            // arrange
            string filename = "Test";
            LogManager.GetInstance().WriteToFile(filename);
            string lineText = "Add this information";

            // act
            LogManager.GetInstance().WriteLine(lineText);

            // assert
            string[] files = Directory.GetFiles(LogManager.PathOfLogFiles);
            string fileContent = File.ReadAllText(files.First());
            Assert.IsTrue(fileContent.Contains(lineText));
        }

        [TestMethod]
        public void WriteAddLineNumericValue()
        {
            // arrange
            string filename = "Test";
            LogManager.GetInstance().WriteToFile(filename);
            int lineValue = 123;
            int divisor = 0;

            // act
            LogManager.GetInstance().WriteLine(lineValue, divisor);

            // assert
            string[] files = Directory.GetFiles(LogManager.PathOfLogFiles);
            string fileContent = File.ReadAllText(files.First());
            Assert.IsTrue(fileContent.Contains($"{lineValue}:{divisor}"));
        }

        [TestMethod]
        public void WriteAddLineTwoTimes()
        {
            // arrange
            string filename = "Test";
            LogManager.GetInstance().WriteToFile(filename);
            string lineText1 = "Add 1. information";
            string lineText2 = "Add 2. information";

            // act
            LogManager.GetInstance().WriteLine(lineText1);
            LogManager.GetInstance().WriteLine(lineText2);

            // assert
            var files = Directory.GetFiles(LogManager.PathOfLogFiles);
            var fileContent = File.ReadAllText(files.First())
                .Split('\r', '\n')
                .Where(w => !string.IsNullOrEmpty(w))
                .ToArray();
            Assert.IsTrue(fileContent.Any(w => w.Contains(lineText1)));
            Assert.IsTrue(fileContent.Any(w => w.Contains(lineText2)));
        }
        
        [TestMethod]
        public void WriteAddLineWithFourValuesAndMultiplicatorAndDivisor()
        {
            // arrange
            string filename = "Test";
            LogManager.GetInstance().WriteToFile(filename);
            var value1 = 101;
            var value2 = 102;
            var value3 = 103;
            var value4 = 104;
            var divisor = 200;
            var multiplicator = 1.23f;

            // act
            LogManager.GetInstance().WriteValues(divisor, multiplicator, value1, value2, value3, value4);

            // assert
            var files = Directory.GetFiles(LogManager.PathOfLogFiles);
            var fileContent = File.ReadAllText(files.First())
                .Split('\r', '\n')
                .Where(w => !string.IsNullOrEmpty(w))
                .ToArray();
            Assert.IsTrue(fileContent.Any(w => w.Contains($"{value1}") &&
                                               w.Contains($"{value2}") &&
                                               w.Contains($"{value3}") &&
                                               w.Contains($"{value4}") &&
                                               w.Contains($"{divisor}") &&
                                               w.Contains($"{multiplicator}")));
            Assert.IsTrue(fileContent.Any(w => w.Split(';').Length == 5));
            Assert.AreEqual(1, fileContent.First().Split(';').Count(c => c.Contains($"{value1}")));
            Assert.AreEqual(1, fileContent.First().Split(';').Count(c => c.Contains($"{value2}")));
            Assert.AreEqual(1, fileContent.First().Split(';').Count(c => c.Contains($"{value3}")));
            Assert.AreEqual(1, fileContent.First().Split(';').Count(c => c.Contains($"{value4}")));
            Assert.AreEqual(4, fileContent.First().Split(';').Count(c => c.Contains($"{divisor}")));
            Assert.AreEqual(4, fileContent.First().Split(';').Count(c => c.Contains($"{multiplicator}")));
        }

        [TestMethod]
        public void ReadFile()
        {
            // arrange
            string filename = "Test";
            LogManager.GetInstance().WriteToFile(filename);
            string lineText1 = "Add 1. information";
            string lineText2 = "Add 2. information";
            LogManager.GetInstance().WriteLine(lineText1);
            LogManager.GetInstance().WriteLine(lineText2);

            // act
            var logItems = LogManager.GetInstance().GetLogs(filename);

            // assert
            var resultArray = logItems as LogItem[] ?? logItems.ToArray();
            Assert.AreEqual(2, resultArray.Count());
            Assert.IsTrue(resultArray.Any(w => w.Content.Contains(lineText1)));
            Assert.IsTrue(resultArray.Any(w => w.Content.Contains(lineText2)));
        }

        [TestMethod]
        public void ReadFileItemHasNumericValue()
        {
            // arrange
            string filename = "Test";
            LogManager.GetInstance().WriteToFile(filename);
            string lineText1 = "123";
            string lineText2 = "456";
            LogManager.GetInstance().WriteLine(lineText1);
            LogManager.GetInstance().WriteLine(lineText2);

            // act
            var logItems = LogManager.GetInstance().GetLogs(filename);

            // assert
            var resultArray = logItems as LogItem[] ?? logItems.ToArray();
            Assert.AreEqual(2, resultArray.Length);
            Assert.IsTrue(resultArray.Any(w => w.Content.Contains(lineText1)));
            Assert.IsTrue(resultArray.Any(w => w.Content.Contains(lineText2)));
        }

        [TestMethod]
        public void ReadFileItemHasNumericValueWithOffset()
        {
            // arrange
            string filename = "Test";
            LogManager.GetInstance().WriteToFile(filename);
            var lineValue1 = 123;
            var lineValue2 = 456;
            var divisor = 789;
            LogManager.GetInstance().WriteLine(lineValue1, divisor);
            LogManager.GetInstance().WriteLine(lineValue2, divisor);

            // act
            var logItems = LogManager.GetInstance().GetLogs(filename);

            // assert
            var resultArray = logItems as LogItem[] ?? logItems.ToArray();
            Assert.AreEqual(2, resultArray.Length);
            Assert.IsTrue(resultArray.Any(w => w.NumericContent.Equals(lineValue1)));
            Assert.IsTrue(resultArray.Any(w => w.NumericContent.Equals(lineValue2)));
        }
        
        [TestMethod]
        public void ReadFileItemHasNumericMultiplyValuesAndOneOffsetAndOneMultiplicator()
        {
            // arrange
            string filename = "Test";
            LogManager.GetInstance().WriteToFile(filename);
            const int value1 = 123;
            const int value2 = 456;
            const int value3 = 456;
            const int value4 = 456;
            
            const int divisor = 789;
            const float multiplicator = 1.23f;
            
            LogManager.GetInstance()
                .WriteValues(divisor, multiplicator, value1, value2, value3, value4);

            // act
            var logItems = LogManager.GetInstance().GetLogs(filename);

            // assert
            var resultArray = logItems as LogItem[] ?? logItems.ToArray();
            Assert.AreEqual(1, resultArray.Length);
            Assert.IsTrue(resultArray.Any(w => w.LogValues[0].Value.Equals(value1)));
            Assert.IsTrue(resultArray.Any(w => w.LogValues[1].Value.Equals(value2)));
            Assert.IsTrue(resultArray.Any(w => w.LogValues[2].Value.Equals(value3)));
            Assert.IsTrue(resultArray.Any(w => w.LogValues[3].Value.Equals(value4)));
            for (var i = 0; i < resultArray.Length; i++)
            {
                Assert.IsTrue(resultArray.Any(w => w.LogValues[i].Divisor.Equals(divisor)));
                Assert.IsTrue(resultArray.Any(w => w.LogValues[i].Multiplicator.Equals(multiplicator)));
            }
        }
    }
}
