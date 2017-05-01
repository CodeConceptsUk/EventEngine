using System.Collections.Generic;

namespace Policy.Application.Interfaces.Repositories
{
    public interface IEventStoreRepository<in TEvent>
        where TEvent : class, IEvent
    {
        void Add(IEnumerable<TEvent> events);
    }
}