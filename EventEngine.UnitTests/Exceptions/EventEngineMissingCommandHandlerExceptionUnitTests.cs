using EventEngine.Exceptions;
using EventEngine.Interfaces.Commands;
using NSubstitute;
using Xunit;

namespace EventEngine.UnitTests.Exceptions
{
    public class EventEngineMissingCommandHandlerExceptionUnitTests
    {
        [Fact]
        public void WhenICreateAnExceptionItIsCreated()
        {
            var expectedCommand = Substitute.For<ICommand>();
            var expectedMessage = $"No event handler for command '{expectedCommand.GetType()}'";
            var target = new EventEngineMissingCommandHandlerException(expectedCommand);

            Assert.Same(expectedCommand, target.Command);
            Assert.Equal(expectedMessage, target.Message);
        }
    }
}