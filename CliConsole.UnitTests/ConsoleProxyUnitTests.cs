using System;
using System.IO;
using System.Text;
using CliConsole.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CliConsole.UnitTests
{
    [TestClass]
    public class ConsoleProxyUnitTests
    {
        private IConsoleProxy _target;

        [TestInitialize]
        public void TestInitialize()
        {
            _target = new ConsoleProxy();
        }

        [TestMethod]
        public void WhenIWriteLineToTheConsoleItIsWritten()
        {
            var expectedOuput = Guid.NewGuid().ToString();
            var writer = new StringWriter();
            var stringBuilder = writer.GetStringBuilder();
            Console.SetOut(writer);

            _target.WriteLine(expectedOuput);

            Assert.AreEqual($"{expectedOuput}\r\n", stringBuilder.ToString());
        }

        [TestMethod]
        public void WhenIWriteToTheConsoleItIsWritten()
        {
            var expectedOuput = Guid.NewGuid().ToString();
            var writer = new StringWriter();
            var stringBuilder = writer.GetStringBuilder();
            Console.SetOut(writer);

            _target.Write(expectedOuput);

            Assert.AreEqual($"{expectedOuput}", stringBuilder.ToString());
        }

        [TestMethod]
        public void WhenIReadFromTheConsoleItIsAccessible()
        {
            var expectedInput = Guid.NewGuid().ToString();
            var writer = new StringReader(expectedInput);
            Console.SetIn(writer);

            var result = _target.ReadLine();

            Assert.AreEqual(expectedInput, result);
        }
    }
}
