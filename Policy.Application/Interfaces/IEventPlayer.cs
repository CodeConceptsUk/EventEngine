using System.Collections.Generic;

namespace Policy.Application.Interfaces
{
    public interface IEventPlayer
    {
        TView Handle<TView>(IEnumerable<IEvent> events, TView view)
            where TView : class, IView;
    }
}