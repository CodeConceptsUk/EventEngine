using System;

namespace EventEngine.Interfaces.Events
{
    public interface IEvent
    {
        Guid ContextId { get; }

        DateTime CreatedDateTime { get; }
        
        DateTime EffectiveDateTime { get; }

        IEventType EventType { get; }

        string EventData { get; }
    }
}
