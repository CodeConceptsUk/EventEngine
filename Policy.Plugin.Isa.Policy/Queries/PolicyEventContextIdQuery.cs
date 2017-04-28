using System;
using System.Linq;
using Policy.Application.Interfaces;
using Policy.Application.Interfaces.Repositories;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Interfaces.Domain;
using Policy.Plugin.Isa.Policy.Interfaces.Queries;
using Policy.Plugin.Isa.Policy.Views;

namespace Policy.Plugin.Isa.Policy.Queries
{
    public class PolicyEventContextIdQuery : PolicyQueryBase<PolicyContextView>, IPolicyEventContextIdQuery
    {
        public PolicyEventContextIdQuery(IEventStoreRepository<IPolicyContext> eventStore, IEventPlayer player)
            : base(eventStore, player)
        {
        }

        public Guid GetEventContextId(string policyNumber)
        {
            var contextIds = EventStore.FindContextIds(t => IsEventForPolicyNumber(policyNumber, t));
            return new PolicyContextView { EventContextId = contextIds.First() }.EventContextId;
        }

        private static bool IsEventForPolicyNumber(string policyNumber, IEvent<IPolicyContext> t)
        {
            var @event = t as PolicyCreatedEvent;
            return @event?.PolicyNumber == policyNumber;
        }
    }
}