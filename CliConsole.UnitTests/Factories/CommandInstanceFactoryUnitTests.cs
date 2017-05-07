using System.Collections.Generic;
using CliConsole.Factories;
using CliConsole.Interfaces;
using CliConsole.Interfaces.Factories;
using NUnit.Framework;
// ReSharper disable UnassignedGetOnlyAutoProperty

namespace CliConsole.UnitTests.Factories
{
    [TestFixture]
    public class CommandInstanceFactoryUnitTests
    {
        private ICommandInstanceFactory _target;

        [SetUp]
        public void SetUp()
        {
            _target = new CommandInstanceFactory();
        }

        [Test]
        public void WhenICreateANewInstanceOfTheCommandItIsCreated()
        {
            var result = _target.Create(typeof(TestCommand));
            Assert.IsInstanceOf<ICommand>(result);
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
