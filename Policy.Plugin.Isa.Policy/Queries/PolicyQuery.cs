using System.Collections.Generic;
using Policy.Application.Interfaces;
using Policy.Application.Interfaces.Repositories;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Interfaces.Domain;
using Policy.Plugin.Isa.Policy.Interfaces.Queries;
using Policy.Plugin.Isa.Policy.Views;

namespace Policy.Plugin.Isa.Policy.Queries
{
    public class PolicyQuery : PolicyQueryBase<PolicyView>, IPolicyQuery
    {
        public PolicyQuery(IEventStoreRepository<IPolicyContext> eventStore, IEventPlayer player)
            : base(eventStore, player)
        {
        }

        public IEnumerable<PolicyView> Read(string policyNumber)
        {
            var contextIds = EventStore.FindContextIds(t => IsEventForPolicyNumber(policyNumber, t));
            return GetContextEvents(contextIds);
        }

        public IEnumerable<PolicyView> Read(int customerId)
        {
            var contextIds = EventStore.FindContextIds(t => IsEventForCustomer(customerId, t));
            return GetContextEvents(contextIds);
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