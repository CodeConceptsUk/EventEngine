using System;
using System.Collections.Generic;
using System.Linq;
using Coresian.Extensions;
using EventEngine.Events;
using EventEngine.Interfaces.Events;
using EventEngine.Interfaces.Services;
using EventEngine.PropertyBags;
using EventEngine.Queries;

namespace EventEngine.Services
{
    public class UndoEventProcessingService : IUndoEventProcessingService
    {
        private readonly IEventDataDeserializationService _eventDataDeserializationService;

        public UndoEventProcessingService(IEventDataDeserializationService eventDataDeserializationService)
        {
            _eventDataDeserializationService = eventDataDeserializationService;
        }

        public IEnumerable<IEvent> Execute(IEnumerable<IEvent> events)
        {
            //TODO: Should the repository do this to be efficant
            var orderedEvents = events.OrderByDescending(@event => @event.EffectiveDateTime);
            var eventIdToUndo = new List<Guid>();
            orderedEvents.ForEach(@event =>
            {
                if (eventIdToUndo.Contains(@event.EventId))
                {
                    @event.Undone = true;
                    return;
                }

                if (@event.EventType.Name == "Undo")
                    eventIdToUndo.AddRange(((UndoData)_eventDataDeserializationService.Deserialize(typeof(UndoData), @event.EventData)).EventIds);
            });
            return orderedEvents.Reverse();
        }
    }
}