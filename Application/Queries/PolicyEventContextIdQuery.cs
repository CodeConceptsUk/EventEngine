using System.Linq;
using Application.Events;
using Application.Interfaces;
using Application.Interfaces.Domain;
using Application.Interfaces.Queries;
using Application.Interfaces.Repositories;
using Application.Views;

namespace Application.Queries
{
    public class PolicyEventContextIdQuery : PolicyQueryBase<PolicyContextView>, IPolicyEventContextIdQuery
    {
        public PolicyEventContextIdQuery(IEventStoreRepository<IPolicyContext> eventStore, IEventPlayer player)
            : base(eventStore, player)
        {
        }

        public PolicyContextView Read(string policyNumber)
        {
            var contextIds = _eventStore.FindContextIds(t => IsEventForPolicyNumber(policyNumber, t));
            return new PolicyContextView {EventContextId = contextIds.First()};
        }

        private static bool IsEventForPolicyNumber(string policyNumber, IEvent<IPolicyContext> t)
        {
            var @event = t as PolicyCreatedEvent;
            return @event?.PolicyNumber == policyNumber;
        }
    }
}