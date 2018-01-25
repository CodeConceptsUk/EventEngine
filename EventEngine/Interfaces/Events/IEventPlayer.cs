using System.Collections.Generic;

namespace EventEngine.Interfaces.Events
{
    public interface IEventPlayer
    {
        void Play<TView>(IEnumerable<IEvent> events, TView view)
            where TView : class, IView;
    }
}