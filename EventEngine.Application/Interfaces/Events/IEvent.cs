using System;
using EventEngine.Application.Interfaces.Repositories;

namespace EventEngine.Application.Interfaces.Events
{
    public interface IEvent
    {
        Guid ContextId { get; }

        DateTime EventDateTime { get; }

        IEventType EventType { get; }

        IEventData EventData { get; }
    }

    public interface IEvent <out TEventData> : IEvent
        where TEventData : IEventData
    {
        new TEventData EventData { get; }
    }
}