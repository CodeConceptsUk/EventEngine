using System.Collections.Generic;

namespace Policy.Application.Interfaces
{
    public interface IEventPlayer<in TEvent>
        where TEvent : class, IEvent
    {
        TView Handle<TView>(IEnumerable<TEvent> events, TView view)
            where TView : class, IView;
    }
}