using System;

namespace EventEngine.Interfaces.Events
{
    public interface IEvent
    {
        Guid ContextId { get; }

        Guid EventId { get; }

        DateTime CreatedDateTime { get; }
        
        DateTime EffectiveDateTime { get; }

        IEventType EventType { get; }

        string EventData { get; }

        bool Undone { get; set; }
    }
}
