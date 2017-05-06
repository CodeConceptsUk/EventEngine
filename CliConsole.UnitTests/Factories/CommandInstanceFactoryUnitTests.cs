using System.Collections.Generic;
using CliConsole.Factories;
using CliConsole.Interfaces;
using CliConsole.Interfaces.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable UnassignedGetOnlyAutoProperty

namespace CliConsole.UnitTests.Factories
{
    [TestClass]
    public class CommandInstanceFactoryUnitTests
    {
        private ICommandInstanceFactory _target;

        [TestInitialize]
        public void TestInitialize()
        {
            _target = new CommandInstanceFactory();
        }

        [TestMethod]
        public void WhenICreateANewInstanceOfTheCommandItIsCreated()
        {
            var result = _target.Create(typeof(TestCommand));
            Assert.IsInstanceOfType(result, typeof(ICommand));
        }
    }

    public class TestCommand : ICommand
    {
        public string CommandName { get; }
        public string Description { get; }
        public IEnumerable<CommandArgument> Arguments { get; }
        public void Run()
        {
            throw new System.NotImplementedException();
        }
    }
}
