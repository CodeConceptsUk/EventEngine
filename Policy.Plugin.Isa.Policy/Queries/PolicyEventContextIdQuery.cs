using System;
using System.Collections.Generic;
using System.Linq;
using Policy.Application.Interfaces;
using Policy.Application.Interfaces.Repositories;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Interfaces.Queries;

namespace Policy.Plugin.Isa.Policy.Queries
{
    public class PolicyeventContextIdQuery : IPolicyeventContextIdQuery
    {
        private readonly IEventStoreRepository<IsaPolicyEvent> _eventStore;

        public PolicyeventContextIdQuery(IEventStoreRepository<IsaPolicyEvent> eventStore)
        {
            _eventStore = eventStore;
        }

        public Guid? GeteventContextId(string policyNumber)
        {
            var contextIds = _eventStore.FindContextIds(t => IsEventForPolicyNumber(policyNumber, t));
            return contextIds.FirstOrDefault();
        }

        public IEnumerable<Guid> GeteventContextId(int clientId)
        {
            var contextIds = _eventStore.FindContextIds(t => IsEventForCustomer(clientId, t));
            return contextIds;
        }

        private static bool IsEventForCustomer(int customerId, IEvent t)
        {
            var @event = t as PolicyCreatedEvent;
            return @event?.CustomerId == customerId;
        }

        private static bool IsEventForPolicyNumber(string policyNumber, IEvent t)
        {
            var @event = t as PolicyCreatedEvent;
            return @event?.PolicyNumber == policyNumber;
        }
    }
}