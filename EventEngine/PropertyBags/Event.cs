using System;
using EventEngine.Interfaces.Events;

namespace EventEngine.PropertyBags
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