using System.Collections.Generic;
using CodeConcepts.CliConsole.Factories;
using CodeConcepts.CliConsole.Interfaces;
using CodeConcepts.CliConsole.Interfaces.Factories;
using NUnit.Framework;

// ReSharper disable UnassignedGetOnlyAutoProperty

namespace CodeConcepts.CliConsole.UnitTests.Factories
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
            var result = _target.Create(typeof(TestCliCommand));
            Assert.IsInstanceOf<ICliCommand>(result);
        }
    }

    public class TestCliCommand : ICliCommand
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
