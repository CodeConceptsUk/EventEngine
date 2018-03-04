using System;
using System.Linq;
using EventEngine.Interfaces;
using EventEngine.Interfaces.Events;
using EventEngine.Interfaces.Queries;
using EventEngine.Interfaces.Repositories;
using EventEngine.Interfaces.Services;

namespace EventEngine.Queries
{
    public class EventQuery<TView> : IEventQuery<TView>
        where TView : class, IView, new()
    {
        private readonly IEventStore _eventStore;
        private readonly IEventPlayer _eventPlayer;
        private readonly IUndoEventProcessingService _undoEventProcessingService;

        public EventQuery(IEventStore eventStore, IEventPlayer eventPlayer,
            IUndoEventProcessingService undoEventProcessingService)
        {
            _eventStore = eventStore;
            _eventPlayer = eventPlayer;
            _undoEventProcessingService = undoEventProcessingService;
        }

        public TView Get(Guid contextId, DateTime? to = null)
        {
            var view = new TView();
            var events = _eventStore.Get(contextId);
            if (to.HasValue)
                events = events.Where(@event => @event.CreatedDateTime <= to);

            events = _undoEventProcessingService.Execute(events);

            _eventPlayer.Play(events, view);
            return view;
        }
    }
}
