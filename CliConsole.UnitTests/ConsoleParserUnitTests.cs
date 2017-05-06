using System;
using System.Linq;
using CliConsole.Interfaces;
using CliConsole.Interfaces.Convertors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.Core;
// ReSharper disable UnusedVariable

namespace CliConsole.UnitTests
{
    [TestClass]
    public class ConsoleParserUnitTests
    {
        private IValueConvertor _valueConvertor;
        private IConsoleParser _target;
        private IConsoleProxy _consoleProxy;

        [TestInitialize]
        public void TestInitialize()
        {
            _valueConvertor = Substitute.For<IValueConvertor>();
            _consoleProxy = Substitute.For<IConsoleProxy>();
            _target = new ConsoleParser(_valueConvertor, _consoleProxy);
        }

        [TestMethod]
        public void WhenIParseTheCommandWithNoArgsThenAllMandatoryFieldsAreRequested()
        {
            const int expectedValue1 = 1;
            var expectedValue3 = Guid.NewGuid().ToString();
            var argument1Value = 0;
            var argument2Value = (string)null;
            var argument3Value = string.Empty;
            var argument1 = new CommandArgument<int>(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), i => argument1Value = i, true);
            var argument2 = new CommandArgument<string>(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), i => argument2Value = Guid.NewGuid().ToString());
            var argument3 = new CommandArgument<string>(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), i => argument3Value = i, true);
            var arguments = new CommandArgument[] { argument1, argument2, argument3 };
            var command = Substitute.For<ICommand>();
            var callCount = 0;

            command.Arguments.Returns(arguments);

            _valueConvertor.TryConvert(typeof(int), $"{expectedValue1}", out object parsedValue1).Returns(c => SetValueFactoryCallInfo(c, expectedValue1));
            _valueConvertor.TryConvert(typeof(string), expectedValue3, out object parsedValue3).Returns(c => SetValueFactoryCallInfo(c, expectedValue3));
            _consoleProxy.ReadLine().Returns(c => SetValueOnConsoleReadLine(callCount++, $"{expectedValue1}", expectedValue3));

            _target.Parse(command, new string[] { });

            Assert.AreEqual(expectedValue1, argument1Value);
            Assert.IsNull(argument2Value);
            Assert.AreEqual(expectedValue3, argument3Value);
            _consoleProxy.Received(2).ReadLine();
        }

        [TestMethod]
        public void WhenIParseTheCommandWithNoArgsThenAllMandatoryFieldsAreRequestedButTheFirstArgumentIsRerequestedAsItCannotBeConverted()
        {
            const int expectedValue1 = 1;
            var expectedValue3 = Guid.NewGuid().ToString();
            var argument1Value = 0;
            var argument2Value = (string)null;
            var argument3Value = string.Empty;
            var argument1 = new CommandArgument<int>(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), i => argument1Value = i, true);
            var argument2 = new CommandArgument<string>(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), i => argument2Value = Guid.NewGuid().ToString());
            var argument3 = new CommandArgument<string>(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), i => argument3Value = i, true);
            var arguments = new CommandArgument[] { argument1, argument2, argument3 };
            var command = Substitute.For<ICommand>();
            var callCount = 0;

            command.Arguments.Returns(arguments);

            _valueConvertor.TryConvert(typeof(int), Arg.Any<string>(), out object emptyValue).Returns(false);
            _valueConvertor.TryConvert(typeof(int), $"{expectedValue1}", out object parsedValue1).Returns(c => SetValueFactoryCallInfo(c, expectedValue1));
            _valueConvertor.TryConvert(typeof(string), expectedValue3, out object parsedValue3).Returns(c => SetValueFactoryCallInfo(c, expectedValue3));
            _consoleProxy.ReadLine().Returns(c => SetValueOnConsoleReadLine(callCount++, Guid.NewGuid().ToString(), $"{expectedValue1}", expectedValue3));

            _target.Parse(command, new string[] { });

            Assert.AreEqual(expectedValue1, argument1Value);
            Assert.IsNull(argument2Value);
            Assert.AreEqual(expectedValue3, argument3Value);
            _consoleProxy.Received(3).ReadLine();
        }

        [TestMethod]
        public void WhenIParseTheCommandWithAllMandatoryArgumentPassedNotOtherFieldsAreRequested()
        {
            const int expectedValue1 = 1;
            var expectedValue2 = Guid.NewGuid().ToString();
            var expectedValue3 = Guid.NewGuid().ToString();
            var argument1Value = 0;
            var argument2Value = string.Empty;
            var argument3Value = string.Empty;
            var argument1 = new CommandArgument<int>(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), i => argument1Value = i, true);
            var argument2 = new CommandArgument<string>(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), i => argument2Value = i);
            var argument3 = new CommandArgument<string>(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), i => argument3Value = i, true);
            var arguments = new CommandArgument[] { argument1, argument2, argument3 };
            var command = Substitute.For<ICommand>();
            var callCount = 0;

            command.Arguments.Returns(arguments);

            _valueConvertor.TryConvert(typeof(int), $"{expectedValue1}", out object parsedValue1).Returns(c => SetValueFactoryCallInfo(c, expectedValue1));
            _valueConvertor.TryConvert(typeof(string), expectedValue2, out object parsedValue2).Returns(c => SetValueFactoryCallInfo(c, expectedValue2));
            _valueConvertor.TryConvert(typeof(string), expectedValue3, out object parsedValue3).Returns(c => SetValueFactoryCallInfo(c, expectedValue3));
            _consoleProxy.ReadLine().Returns(c => SetValueOnConsoleReadLine(callCount++, new string[] { }));

            var args = new[] { argument1.Name, $"{expectedValue1}", argument2.Name, expectedValue2, argument3.Name, expectedValue3 };
            _target.Parse(command, args);

            Assert.AreEqual(expectedValue1, argument1Value);
            Assert.AreEqual(expectedValue2, argument2Value);
            Assert.AreEqual(expectedValue3, argument3Value);
            _consoleProxy.Received(0).ReadLine();
        }

        [TestMethod]
        public void WhenIParseTheCommandWithNoArgsThenAllMandatoryFieldsAreRequestedWhenCtlCThenIExitTheCommand()
        {
            const int expectedValue1 = 1;
            var expectedValue3 = Guid.NewGuid().ToString();
            var argument1Value = 0;
            var argument2Value = (string)null;
            var argument3Value = string.Empty;
            var argument1 = new CommandArgument<int>(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), i => argument1Value = i, true);
            var argument2 = new CommandArgument<string>(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), i => argument2Value = Guid.NewGuid().ToString());
            var argument3 = new CommandArgument<string>(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), i => argument3Value = i, true);
            var arguments = new CommandArgument[] { argument1, argument2, argument3 };
            var command = Substitute.For<ICommand>();
            var callCount = 0;

            command.Arguments.Returns(arguments);

            _valueConvertor.TryConvert(typeof(int), $"{expectedValue1}", out object parsedValue1).Returns(c => SetValueFactoryCallInfo(c, expectedValue1));
            _valueConvertor.TryConvert(typeof(string), expectedValue3, out object parsedValue3).Returns(c => SetValueFactoryCallInfo(c, expectedValue3));
            _consoleProxy.ReadLine().Returns(c => SetValueOnConsoleReadLine(callCount++, $"{expectedValue1}", (string)null));

            var result = _target.Parse(command, new string[] { });

            Assert.AreEqual(false, result);
            _consoleProxy.WriteLine($"Exiting command {command.CommandName}");
            _consoleProxy.Received(2).ReadLine();
        }

        private static string SetValueOnConsoleReadLine(int callCount, params string[] values)
        {
            if (!(callCount < values.Length))
                throw new IndexOutOfRangeException();
            return values[callCount];
        }

        private static bool SetValueFactoryCallInfo(CallInfo c, object expectedValue1)
        {
            c[2] = expectedValue1;
            return true;
        }
    }
}
