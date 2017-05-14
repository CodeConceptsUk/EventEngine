using System.Collections.Generic;
using CodeConcepts.EventEngine.Api.Contracts;

namespace CodeConcepts.EventEngine.Contracts.Interfaces
{
    public interface IEventPlayer<in TEvent>
        where TEvent : class, IEvent
    {
        TView Handle<TView>(IEnumerable<TEvent> events, TView view)
            where TView : class, IView;
    }
}