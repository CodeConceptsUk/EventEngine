using System;
using Coresian.Interfaces.Providers;
using EventEngine.Interfaces.Events;
using EventEngine.Interfaces.Factories;
using EventEngine.Interfaces.Services;
using EventEngine.PropertyBags;

namespace EventEngine.Factories
{
    public class EventFactory : IEventFactory
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IEventDataSerializationService _eventDataSerializationService;
        private readonly IGuidProvider _guidProvider;
        private readonly IEventTypeService _eventTypeService;

        public EventFactory(IEventDataSerializationService eventDataSerializationService,
            IGuidProvider guidProvider,
            IEventTypeService eventTypeService,
            IDateTimeProvider dateTimeProvider)
        {
            _eventDataSerializationService = eventDataSerializationService;
            _guidProvider = guidProvider;
            _eventTypeService = eventTypeService;
            _dateTimeProvider = dateTimeProvider;
        }

        public IEvent Create<TEventData>(Guid contextId, TEventData eventData, DateTime? effectiveDateTime = null)
            where TEventData : IEventData
        {
            var eventId = _guidProvider.Create();
            var serializedEventData = _eventDataSerializationService.Serialize(eventData);
            var eventType = _eventTypeService.Get<TEventData>();
            var createdDateTime = _dateTimeProvider.GetUtc();
            var eventEffectiveDateTime = effectiveDateTime ?? createdDateTime;

            return new Event(eventId, contextId, eventType, serializedEventData, createdDateTime, eventEffectiveDateTime);
        }
    }
}
