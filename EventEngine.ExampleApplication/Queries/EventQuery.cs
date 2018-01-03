using System;
using System.Linq;
using EventEngine.Application.Interfaces;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Repositories;
using EventEngine.ExampleApplication.Interfaces.Queries;

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