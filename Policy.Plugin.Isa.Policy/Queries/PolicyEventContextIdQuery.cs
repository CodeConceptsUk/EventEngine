using System;
using System.Collections.Generic;
using System.Linq;
using Policy.Application.Interfaces;
using Policy.Application.Interfaces.Repositories;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Interfaces.Domain;
using Policy.Plugin.Isa.Policy.Interfaces.Queries;

namespace Policy.Plugin.Isa.Policy.Queries
{
    public class PolicyEventContextIdQuery : IPolicyEventContextIdQuery
    {
        private readonly IEventStoreRepository<IPolicyContext> _eventStore;

        public PolicyEventContextIdQuery(IEventStoreRepository<IPolicyContext> eventStore)
        {
            _eventStore = eventStore;
        }

        public Guid? GetEventContextId(string policyNumber)
        {
            var contextIds = _eventStore.FindContextIds(t => IsEventForPolicyNumber(policyNumber, t));
            return contextIds.FirstOrDefault();
        }

        public IEnumerable<Guid> GetEventContextId(int clientId)
        {
            var contextIds = _eventStore.FindContextIds(t => IsEventForCustomer(clientId, t));
            return contextIds;
        }

        private static bool IsEventForCustomer(int customerId, IEvent<IPolicyContext> t)
        {
            var @event = t as PolicyCreatedEvent;
            return @event?.CustomerId == customerId;
        }

        private static bool IsEventForPolicyNumber(string policyNumber, IEvent<IPolicyContext> t)
        {
            var @event = t as PolicyCreatedEvent;
            return @event?.PolicyNumber == policyNumber;
        }
    }
}