using System;
using EventEngine.Application.Factories;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Factories;
using EventEngine.Application.Interfaces.Providers;
using EventEngine.Application.Interfaces.Services;
using NSubstitute;
using NUnit.Framework;

namespace EventEngine.UnitTests.PropertyBags
{
    [TestFixture]
    public class EventUnitTests
    {

        private IEventDataSerializationService _eventDataSerializationService;
        private IEventTypeService _eventTypeService;
        private IDateTimeProvider _dateTimeProvider;
        private IEventFactory _target;

        [SetUp]
        public void SetUp()
        {
            _eventDataSerializationService = Substitute.For<IEventDataSerializationService>();
            _eventTypeService = Substitute.For<IEventTypeService>();
            _dateTimeProvider = Substitute.For<IDateTimeProvider>();
            _target = new EventFactory(_eventDataSerializationService, _eventTypeService,
                _dateTimeProvider);
        }

        [Test]
        public void WhenICreateAnEventItIsCreated()
        {
            var expectedContextId = Guid.NewGuid();
            var expectedEventData = Guid.NewGuid().ToString();
            var expectedEventType = Substitute.For<IEventType>();
            var expectedEventDateTime = DateTime.Now;
            var eventData = Substitute.For<IEventData>();

            _dateTimeProvider.GetUtcTime().Returns(expectedEventDateTime);
            _eventDataSerializationService.Serialize(eventData).Returns(expectedEventData);
            _eventTypeService.Get<IEventData>().Returns(expectedEventType);

            var result = _target.Create(expectedContextId, eventData);

            Assert.AreEqual(expectedContextId, result.ContextId);
            Assert.AreEqual(expectedEventDateTime, result.EventDateTime);
            Assert.AreSame(expectedEventType, result.EventType);
            Assert.AreEqual(expectedEventData, result.EventData);
        }
    }
}