using System.Collections.Generic;

namespace CodeConcepts.EventEngine.Contracts.Interfaces.Repositories
{
    public interface IEventStoreRepository
    {
        void Add(IEnumerable<IEvent> events);
    }

    public interface IEventStoreRepository<in TEvent> : IEventStoreRepository
        where TEvent : class, IEvent
    {
        void Add(IEnumerable<TEvent> events);
    }
}