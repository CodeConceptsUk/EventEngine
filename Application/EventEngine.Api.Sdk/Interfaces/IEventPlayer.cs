using System.Collections.Generic;
using CodeConcepts.EventEngine.Api.Contracts;

namespace CodeConcepts.EventEngine.Contracts.Interfaces
{
    public interface IEventPlayer
    {
        TView Handle<TView, TEvent>(IEnumerable<TEvent> events, TView view)
            where TView : class, IView
        where TEvent : class, IEvent;
    }
}