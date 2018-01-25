using System;
using System.Linq;
using EventEngine.ExampleApplication.Interfaces.Queries;
using EventEngine.Interfaces;
using EventEngine.Interfaces.Events;
using EventEngine.Interfaces.Repositories;

namespace EventEngine.ExampleApplication.Queries
{
    public class EventQuery<TView> : IEventQuery<TView>
        where TView : class, IView, new()
    {
        private readonly IEventStore _eventStore;
        private readonly IEventPlayer _eventPlayer;

        public EventQuery(IEventStore eventStore, IEventPlayer eventPlayer)
        {
            _eventStore = eventStore;
            _eventPlayer = eventPlayer;
        }

        public TView Get(Guid contextId, DateTime? to = null)
        {
            var view = new TView();
            var events = _eventStore.Get(contextId).Where(@event => to == null || @event.EventDateTime <= to);
            _eventPlayer.Play(events, view);
            return view;
        }
    }
}