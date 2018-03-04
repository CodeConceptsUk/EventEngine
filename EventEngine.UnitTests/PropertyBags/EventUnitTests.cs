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
            _guidProvider = Substitute.For<IGuidProvider>();
            _eventDataSerializationService = Substitute.For<IEventDataSerializationService>();
            _eventTypeService = Substitute.For<IEventTypeService>();
            _dateTimeProvider = Substitute.For<IDateTimeProvider>();
            _target = new EventFactory(_eventDataSerializationService, _guidProvider, _eventTypeService,
                _dateTimeProvider);
        }

        private readonly IGuidProvider _guidProvider;
        private readonly IEventDataSerializationService _eventDataSerializationService;
        private readonly IEventTypeService _eventTypeService;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IEventFactory _target;

        [Fact]
        public void WhenICreateAnEventItIsCreated()
        {
            var expectedEventId = Guid.NewGuid();
            var expectedContextId = Guid.NewGuid();
            var expectedEventData = Guid.NewGuid().ToString();
            var expectedEventType = Substitute.For<IEventType>();
            var expectedEventDateTime = DateTime.Now;
            var expectedEffectiveDateTime = DateTime.Now.AddDays(-2);
            var eventData = Substitute.For<IEventData>();

            _guidProvider.Create().Returns(expectedEventId);
            _dateTimeProvider.GetUtc().Returns(expectedEventDateTime);
            _eventDataSerializationService.Serialize(eventData).Returns(expectedEventData);
            _eventTypeService.Get<IEventData>().Returns(expectedEventType);

            var result = _target.Create(expectedContextId, eventData, expectedEffectiveDateTime);

            Assert.Equal(expectedEventId, result.EventId);
            Assert.Equal(expectedContextId, result.ContextId);
            Assert.Equal(expectedEventDateTime, result.CreatedDateTime);
            Assert.Equal(expectedEffectiveDateTime, result.EffectiveDateTime);
            Assert.Same(expectedEventType, result.EventType);
            Assert.Equal(expectedEventData, result.EventData);
            Assert.False(result.Undone);
        }
        
        [Fact]
        public void WhenICreateAnEventWithDefaultEffectiveDateItIsCreated()
        {
            var expectedEventId = Guid.NewGuid();
            var expectedContextId = Guid.NewGuid();
            var expectedEventData = Guid.NewGuid().ToString();
            var expectedEventType = Substitute.For<IEventType>();
            var expectedEventDateTime = DateTime.Now;
            var expectedEffectiveDateTime = expectedEventDateTime;
            var eventData = Substitute.For<IEventData>();

            _guidProvider.Create().Returns(expectedEventId);
            _dateTimeProvider.GetUtc().Returns(expectedEventDateTime);
            _eventDataSerializationService.Serialize(eventData).Returns(expectedEventData);
            _eventTypeService.Get<IEventData>().Returns(expectedEventType);

            var result = _target.Create(expectedContextId, eventData);

            Assert.Equal(expectedEventId, result.EventId);
            Assert.Equal(expectedContextId, result.ContextId);
            Assert.Equal(expectedEventDateTime, result.CreatedDateTime);
            Assert.Equal(expectedEffectiveDateTime, result.EffectiveDateTime);
            Assert.Same(expectedEventType, result.EventType);
            Assert.Equal(expectedEventData, result.EventData);
            Assert.False(result.Undone);
        }
    }
}
