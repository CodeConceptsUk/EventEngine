using System.Collections.Generic;
using CodeConcepts.EventEngine.Contracts.Interfaces;

namespace CodeConcepts.EventEngine.Application.Interfaces.Repositories
{
    public interface IEventStoreRepository<in TEvent>
        where TEvent : class, IEvent
    {
        void Add(IEnumerable<TEvent> events);
    }
}