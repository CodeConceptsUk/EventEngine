using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FrameworkExtensions.LinqExtensions;
using Policy.Application.Interfaces;
using Policy.Application.Interfaces.Repositories;

namespace Policy.Plugin.Isa.Policy.DataAccess.InMemory
{
    public class PolicyContextEventStoreInMemoryStore : IEventStoreRepository
    {
        private static readonly IList<IEvent> Events = new List<IEvent>();

        public IEnumerable<Guid> FindContextIds(Expression<Func<IEvent, bool>> @where)
        {
            var expression = @where.Compile();
            var events = Events.Where(t => expression(t)).Select(t => t.EventContextId);
            return events;
        }

        public IEnumerable<IEvent> Get(Guid eventContextId, Guid? eventId = null)
        {
            if (eventId.HasValue)
            {
                var eventIndex = Events.IndexOf(Events.FirstOrDefault(t => t.EventId == eventId));
                if (eventIndex != -1)
                    return Events.Skip(eventIndex + 1);

                throw new Exception($"Event {eventId} not found in the event store.");
            }

            return Events.Where(t => t.EventContextId == eventContextId);
        }

        public void Add(IEnumerable<IEvent> events)
        {
            events.ForEach(t => Events.Add(t));
        }
    }
}