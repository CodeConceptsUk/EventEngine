using EventEngine.Application.Exceptions;
using EventEngine.Application.Interfaces.Commands;
using NSubstitute;
using NUnit.Framework;

namespace EventEngine.UnitTests.Exceptions
{
    [TestFixture]
    public class EventEngineMissingCommandHandlerExceptionUnitTests
    {
        [Test]
        public void WhenICreateAnExceptionItIsCreated()
        {
            var expectedCommand = Substitute.For<ICommand>();
            var expectedMessage = $"No event handler for command '{expectedCommand.GetType()}'";
            var target = new EventEngineMissingCommandHandlerException(expectedCommand);

            Assert.AreSame(expectedCommand, target.Command);
            Assert.AreEqual(expectedMessage, target.Message);
        }
    }
}