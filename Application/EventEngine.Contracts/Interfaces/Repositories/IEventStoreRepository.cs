using System.Collections.Generic;

namespace CodeConcepts.EventEngine.Contracts.Interfaces.Repositories
{
    public interface IEventStoreRepository<in TEvent>
        where TEvent : class, IEvent
    {
        void Add(IEnumerable<TEvent> events);
    }
}