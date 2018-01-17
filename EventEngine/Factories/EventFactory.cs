using System;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Factories;
using EventEngine.Application.Interfaces.Providers;
using EventEngine.Application.Interfaces.Services;
using EventEngine.Application.PropertyBags;

namespace EventEngine.Application.Factories
{
    public class EventFactory : IEventFactory
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IEventDataSerializationService _eventDataSerializationService;
        private readonly IEventTypeService _eventTypeService;

        public EventFactory(IEventDataSerializationService eventDataSerializationService,
            IEventTypeService eventTypeService,
            IDateTimeProvider dateTimeProvider)
        {
            _eventDataSerializationService = eventDataSerializationService;
            _eventTypeService = eventTypeService;
            _dateTimeProvider = dateTimeProvider;
        }

        public IEvent Create<TEventData>(Guid contextId, TEventData eventData)
            where TEventData : IEventData
        {
            var serializedEventData = _eventDataSerializationService.Serialize(eventData);
            var eventType = _eventTypeService.Get<TEventData>();
            var eventDateTime = _dateTimeProvider.GetUtcTime();

            return new Event(contextId, eventType, serializedEventData, eventDateTime);
        }
    }
}