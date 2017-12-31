using System;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Repositories;

namespace EventEngine.Application.PropertyBags
{
    public class Event<TEventData> : IEvent<TEventData> 
        where TEventData : IEventData
    {
        public Event(Guid contextId, IEventType eventType, TEventData eventData, DateTime eventDateTime)
        {
            ContextId = contextId;
            EventType = eventType;
            EventData = eventData;
            EventDateTime = eventDateTime;
        }
        
        public Guid ContextId { get; }

        public DateTime EventDateTime { get; }

        public IEventType EventType { get; }

        public TEventData EventData { get; }

        IEventData IEvent.EventData => EventData;
    }
}