using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FrameworkExtensions.LinqExtensions;
using Policy.Application.Interfaces;
using Policy.Application.Interfaces.Repositories;
using Policy.Plugin.Isa.Policy.Events;

namespace Policy.Plugin.Isa.Policy.DataAccess.InMemory
{
    public class PolicyContextEventStoreInMemoryStore : IEventStoreRepository<IsaPolicyEvent>
    {
        private static readonly IList<IsaPolicyEvent> Events = new List<IsaPolicyEvent>();

        public IEnumerable<Guid> FindContextIds(Expression<Func<IEvent, bool>> @where)
        {
            var expression = @where.Compile();
            var events = Events.Where(t => expression(t)).Select(t => t.EventContextId);
            return events;
        }

        public IEnumerable<IsaPolicyEvent> Get(Guid eventContextId, Guid? eventId = null)
        {
            if (eventId.HasValue)
            {
                var events = Events.Where(t => t.EventContextId == eventContextId).ToList();
                var eventIndex = events.IndexOf(Events.FirstOrDefault(t => t.EventId == eventId));
                if (eventIndex != -1)
                    return events.Skip(eventIndex + 1);

                throw new Exception($"Event {eventId} not found in the event store.");
            }

            return Events.Where(t => t.EventContextId == eventContextId);
        }

        public void Add(IEnumerable<IsaPolicyEvent> events)
        {
            events.ForEach(t => Events.Add(t));
        }
    }
}