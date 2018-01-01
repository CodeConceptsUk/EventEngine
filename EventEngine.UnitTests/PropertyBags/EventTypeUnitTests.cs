using System;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.PropertyBags;
using NUnit.Framework;

namespace EventEngine.UnitTests.PropertyBags
{
    [TestFixture]
    public class EventTypeUnitTests
    {
        [SetUp]
        public void TestInitialize()
        {
        }

        private IEventType _target;

        [Test]
        public void WhenTheEventTypeIsConstructedItIsPopulatedAsExpected()
        {
            var expectedVersion = new Version(4, 2, 1, 2);
            var expectedType = Guid.NewGuid().ToString();

            _target = new EventType(expectedType, expectedVersion);

            Assert.AreSame(expectedVersion, _target.Version);
            Assert.AreEqual(expectedType, _target.Name);
        }
    }
}