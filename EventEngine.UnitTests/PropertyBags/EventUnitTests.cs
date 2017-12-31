using System;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Repositories;
using EventEngine.Application.PropertyBags;
using NSubstitute;
using NUnit.Framework;

namespace EventEngine.UnitTests.PropertyBags
{
    [TestFixture]
    public class EventUnitTests
    {
        [Test]
        public void WhenICreateAnEventItIsCreated()
        {
            var expectedContextId = Guid.NewGuid();
            var expectedEventData = Substitute.For<IEventData>();
            var expectedEventType = Substitute.For<IEventType>();
            var expectedEventDateTime = DateTime.Now;

            var target = new Event<IEventData>(expectedContextId, expectedEventType, expectedEventData, expectedEventDateTime);

            Assert.AreEqual(expectedContextId, target.ContextId);
            Assert.AreEqual(expectedEventDateTime, target.EventDateTime);
            Assert.AreSame(expectedEventType, target.EventType);
            Assert.AreSame(expectedEventData, target.EventData);
        }
    }
}