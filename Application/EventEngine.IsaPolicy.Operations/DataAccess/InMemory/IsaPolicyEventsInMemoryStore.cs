using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.DataAccess;
using CodeConcepts.FrameworkExtensions.LinqExtensions;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.DataAccess.InMemory
{
    public class IsaPolicyEventsInMemoryStore : IIsaPolicyEventStoreRepository
    {
        private static readonly IList<IsaPolicyEvent> Events = new List<IsaPolicyEvent>();

        public IEnumerable<Guid> FindContextIds(Expression<Func<IEvent, bool>> @where)
        {
            var expression = @where.Compile();
            var events = Events.Where(t => expression(t)).Select(t => t.EventContextId);
            return events;
        }

        public Guid? FindContextId(string policyNumber)
        {
            return Events.OfType<PolicyCreatedEvent>().FirstOrDefault(e => e.PolicyNumber == policyNumber)?.EventContextId;
        }

        public IEnumerable<Guid> FindContextIds(string customerId)
        {
            return Events.OfType<PolicyCreatedEvent>().Where(e => e.CustomerId == customerId).Select(e=> e.EventContextId);
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