using System;
using System.Collections.Generic;
using System.Linq;
using EventEngine.Events;
using EventEngine.Interfaces;
using EventEngine.Interfaces.Events;
using EventEngine.Interfaces.Queries;
using EventEngine.Interfaces.Repositories;
using EventEngine.Interfaces.Services;
using EventEngine.PropertyBags;

namespace EventEngine.Queries
{
    public class EventQuery<TView> : IEventQuery<TView>
        where TView : class, IView, new()
    {
        private readonly IEventStore _eventStore;
        private readonly IEventPlayer _eventPlayer;
        private readonly IEventDataDeserializationService _eventDataDeserializationService;

        public EventQuery(IEventStore eventStore, IEventPlayer eventPlayer,
            IEventDataDeserializationService eventDataDeserializationService)
        {
            _eventStore = eventStore;
            _eventPlayer = eventPlayer;
            _eventDataDeserializationService = eventDataDeserializationService;
        }

        public TView Get(Guid contextId, DateTime? to = null)
        {
            var view = new TView();
            var events = _eventStore
                .Get(contextId)
                .Where(@event => to == null || @event.CreatedDateTime <= to)
                .OrderByDescending(@event => @event.EffectiveDateTime);

           

            //// TODO: EffectivateDate order
            //var undoEventIds = events
            //    .Where(t => t.EventType.Name == "Undo")
            //    .SelectMany(t => ((UndoData)_eventDataDeserializationService.Deserialize(typeof(UndoData), t.EventData)).EventIds);
            //undoEventIds.ForEach(e =>
            //{
            //    var @event = (Event)events.Single(t => t.EventIds == e);
            //    @event.Undone = !@event.Undone;
            //});


            _eventPlayer.Play(events, view);
            return view;
        }
    }
}
