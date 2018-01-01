using System;
using EventEngine.Application.Interfaces.Events;

namespace EventEngine.Application.PropertyBags
{
    public class Event : IEvent 
    {
        internal Event(Guid contextId, IEventType eventType, string eventData, DateTime eventDateTime)
        {
            ContextId = contextId;
            EventType = eventType;
            EventData = eventData;
            EventDateTime = eventDateTime;
        }
        
        public Guid ContextId { get; }

        public DateTime EventDateTime { get; }

        public IEventType EventType { get; }

        public string EventData { get; }
    }
}