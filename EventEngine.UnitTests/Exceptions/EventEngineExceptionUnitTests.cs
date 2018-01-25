using System;
using EventEngine.Exceptions;
using Xunit;

namespace EventEngine.UnitTests.Exceptions
{
    public class EventEngineExceptionUnitTests
    {
        [Fact]
        public void WhenICreateAnExceptionItIsCreated()
        {
            var expectedMessage = Guid.NewGuid().ToString();
            var target = new EventEngineException(expectedMessage);

            Assert.Equal(expectedMessage, target.Message);
        }
    }
}