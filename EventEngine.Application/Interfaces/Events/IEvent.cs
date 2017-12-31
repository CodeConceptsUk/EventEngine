using System;
using EventEngine.Application.Interfaces.Repositories;

namespace EventEngine.Application.Interfaces.Events
{
    public interface IEvent
    {
        Guid ContextId { get; }

        DateTime EventDateTime { get; }

        IEventType EventType { get; }

        string EventData { get; }
    }
}