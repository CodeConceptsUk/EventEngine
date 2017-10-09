using EventEngine.Application.Exceptions;
using NUnit.Framework;

namespace EventEngine.UnitTests.Exceptions
{
    [TestFixture]
    public class EventEngineExceptionUnitTests
    {
        [Test]
        public void WhenICreateAnExceptionItIsCreated()
        {
            var expectedMessage = $"This is a message";
            var target = new EventEngineException(expectedMessage);

            Assert.AreEqual(expectedMessage, target.Message);
        }
    }
}