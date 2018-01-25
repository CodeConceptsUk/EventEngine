using System;

namespace EventEngine.Interfaces.Events
{
    public interface IEvent
    {
        Guid ContextId { get; }

        DateTime EventDateTime { get; }

        IEventType EventType { get; }

        string EventData { get; }
    }
}