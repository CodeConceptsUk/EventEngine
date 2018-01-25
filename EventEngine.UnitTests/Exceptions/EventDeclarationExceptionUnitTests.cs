using System;
using EventEngine.Exceptions;
using Xunit;

namespace EventEngine.UnitTests.Exceptions
{
    public class EventDeclarationExceptionUnitTests
    {
        [Fact]
        public void WhenICreateAnExceptionItIsCreated()
        {
            var expectedMessage = Guid.NewGuid().ToString();
            var target = new EventDeclarationException(expectedMessage);

            Assert.Equal(expectedMessage, target.Message);
        }
    }
}