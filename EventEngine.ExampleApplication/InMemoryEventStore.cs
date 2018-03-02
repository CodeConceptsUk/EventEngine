using System;
using System.Collections.Generic;
using System.Linq;
using EventEngine.Interfaces.Events;
using EventEngine.Interfaces.Repositories;

namespace EventEngine.ExampleApplication
{
    public class InMemoryEventStore : IEventStore
    {
        private static readonly List<IEvent> Events = new List<IEvent>();

        public void Add(IEnumerable<IEvent> events)
        {
            Events.AddRange(events);
        }

        public IEnumerable<IEvent> Get(Guid? contextId = null, DateTime? @from = null, IEventType[] eventTypes = null)
        {
            var eventTypeNames = eventTypes?.Select(t => t.Name);
            return Events.Where(@event =>
                (contextId == null || @event.ContextId == contextId) &&
                (@from == null || @event.CreatedDateTime > @from) &&
                (eventTypeNames == null || eventTypeNames.Contains(@event.EventType.Name)));
        }
    }
}
