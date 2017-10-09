using System.Collections.Generic;
using EventEngine.Application.Interfaces.Events;

namespace EventEngine.Application.Interfaces.Repositories
{
    public interface IEventStore
    {
        void Add<TEvent>(IEnumerable<TEvent> events)
            where TEvent : class, IEvent;
    }
}