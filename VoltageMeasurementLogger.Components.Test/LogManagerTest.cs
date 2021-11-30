using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using VoltageMeasurementLogger.Components.Log;

namespace VoltageMeasurementLogger.Test.Components
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
        public void WriteAddLine()
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
            string[] files = Directory.GetFiles(LogManager.PathOfLogFiles);
            var fileContent = File.ReadAllText(files.First())
                .Split('\r', '\n')
                .Where(w => !string.IsNullOrEmpty(w));
            Assert.IsTrue(fileContent.Any(w => w.Contains(lineText1)));
            Assert.IsTrue(fileContent.Any(w => w.Contains(lineText2)));
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
            Assert.AreEqual(2, logItems.Count());
            Assert.IsTrue(logItems.Any(w => w.Content.Contains(lineText1)));
            Assert.IsTrue(logItems.Any(w => w.Content.Contains(lineText2)));
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
            Assert.AreEqual(2, logItems.Count());
            Assert.IsTrue(logItems.Any(w => w.Content.Contains(lineText1)));
            Assert.IsTrue(logItems.Any(w => w.Content.Contains(lineText2)));
        }
    }
}
