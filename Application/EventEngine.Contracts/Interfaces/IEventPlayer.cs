using System.Collections.Generic;

namespace CodeConcepts.EventEngine.Contracts.Interfaces
{
    public interface IEventPlayer<in TEvent>
        where TEvent : class, IEvent
    {
        TView Handle<TView>(IEnumerable<TEvent> events, TView view)
            where TView : class, IView;
    }
}