using System;
using EventEngine.Interfaces.Events;

namespace EventEngine.PropertyBags
{
    public class Event : IEvent
    {
        internal Event(Guid contextId, IEventType eventType, string eventData, DateTime createdDateTime, DateTime effectiveDateTime)
        {
            ContextId = contextId;
            EventType = eventType;
            EventData = eventData;
            CreatedDateTime = createdDateTime;
            EffectiveDateTime = effectiveDateTime;
        }

        public Guid ContextId { get; }

        public DateTime CreatedDateTime { get; }
        
        public DateTime EffectiveDateTime { get; }

        public IEventType EventType { get; }

        public string EventData { get; }
    }
}
