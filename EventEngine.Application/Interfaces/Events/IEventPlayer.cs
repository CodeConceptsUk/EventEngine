using System.Collections.Generic;

namespace EventEngine.Application.Interfaces.Events
{
    public interface IEventPlayer
    {
        TView Evaluate<TView, TEvent>(IEnumerable<TEvent> events, TView view)
            where TView : class, IView
            where TEvent : class, IEvent;
    }
}