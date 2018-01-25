using System;
using EventEngine.Exceptions;
using NUnit.Framework;

namespace EventEngine.UnitTests.Exceptions
{
    [TestFixture]
    public class EventEngineExceptionUnitTests
    {
        [Test]
        public void WhenICreateAnExceptionItIsCreated()
        {
            var expectedMessage = Guid.NewGuid().ToString();
            var target = new EventEngineException(expectedMessage);

            Assert.AreEqual(expectedMessage, target.Message);
        }
    }
}