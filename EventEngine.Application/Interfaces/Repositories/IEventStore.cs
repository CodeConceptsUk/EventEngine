using System;
using System.Collections.Generic;
using EventEngine.Application.Interfaces.Events;

namespace EventEngine.Application.Interfaces.Repositories
{
    public interface IEventStore
    {
        void Add(IEnumerable<IEvent> events);

        IEnumerable<IEvent> Get(Guid? contextId = null, DateTime? from = null, IEventType[] eventType = null);
    }

    public interface IEventTypeService
    {
        IEventType Get(IEvent @event);
    }

    public interface IEventType
    {
        string Type { get; }
    }
}