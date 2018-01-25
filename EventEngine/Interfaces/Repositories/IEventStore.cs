using System;
using System.Collections.Generic;
using EventEngine.Interfaces.Events;

namespace EventEngine.Interfaces.Repositories
{
    public interface IEventStore
    {
        void Add(IEnumerable<IEvent> events);

        IEnumerable<IEvent> Get(Guid? contextId = null, DateTime? from = null, IEventType[] eventTypes = null);
    }
}