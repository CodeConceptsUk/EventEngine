using System;
using System.IO;
using CliConsole.Interfaces;
using NUnit.Framework;

namespace CliConsole.UnitTests
{
    [TestFixture]
    public class ConsoleProxyUnitTests
    {
        private IConsoleProxy _target;

        [SetUp]
        public void SetUp()
        {
            _target = new ConsoleProxy();
        }

        [Test]
        public void WhenIWriteLineToTheConsoleItIsWritten()
        {
            var expectedOuput = Guid.NewGuid().ToString();
            var writer = new StringWriter();
            var stringBuilder = writer.GetStringBuilder();
            Console.SetOut(writer);

            _target.WriteLine(expectedOuput);

            Assert.AreEqual($"{expectedOuput}\r\n", stringBuilder.ToString());
        }

        [Test]
        public void WhenIWriteToTheConsoleItIsWritten()
        {
            var expectedOuput = Guid.NewGuid().ToString();
            var writer = new StringWriter();
            var stringBuilder = writer.GetStringBuilder();
            Console.SetOut(writer);

            _target.Write(expectedOuput);

            Assert.AreEqual($"{expectedOuput}", stringBuilder.ToString());
        }

        [Test]
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
