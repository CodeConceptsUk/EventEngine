using System;
using Coresian.Interfaces.Providers;
using EventEngine.Factories;
using EventEngine.Interfaces.Events;
using EventEngine.Interfaces.Factories;
using EventEngine.Interfaces.Services;
using NSubstitute;
using Xunit;

namespace EventEngine.UnitTests.PropertyBags
{
    public class EventUnitTests
    {
        public EventUnitTests()
        {
            _eventDataSerializationService = Substitute.For<IEventDataSerializationService>();
            _eventTypeService = Substitute.For<IEventTypeService>();
            _dateTimeProvider = Substitute.For<IDateTimeProvider>();
            _target = new EventFactory(_eventDataSerializationService, _eventTypeService,
                _dateTimeProvider);
        }

        private IEventDataSerializationService _eventDataSerializationService;
        private IEventTypeService _eventTypeService;
        private IDateTimeProvider _dateTimeProvider;
        private IEventFactory _target;

        [Fact]
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

            Assert.Equal(expectedContextId, result.ContextId);
            Assert.Equal(expectedEventDateTime, result.EventDateTime);
            Assert.Same(expectedEventType, result.EventType);
            Assert.Equal(expectedEventData, result.EventData);
        }
    }
}