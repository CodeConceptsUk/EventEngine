using System.Collections.Generic;
using System.Linq;
using Application.Events;
using Application.Interfaces;
using Application.Interfaces.Domain;
using Application.Interfaces.Repositories;
using Application.Views;

namespace Application.Queries
{
    public abstract class PolicyQueryBase <TView>: IQuery<TView, IPolicyContext>
        where TView : class, IView<IPolicyContext>
    {
        protected readonly IEventStoreRepository<IPolicyContext> _eventStore;
        protected readonly IEventPlayer _player;

        protected PolicyQueryBase(IEventStoreRepository<IPolicyContext> eventStore, IEventPlayer player)
        {
            _eventStore = eventStore;
            _player = player;
        }
    }

    public class PolicyQuery : PolicyQueryBase<PolicyView>
    {
        public PolicyQuery(IEventStoreRepository<IPolicyContext> eventStore, IEventPlayer player)
            : base(eventStore, player)
        {
        }

        public IEnumerable<PolicyView> Read(int customerId)
        {
            var contextIds = _eventStore.FindContextIds(t => IsEventForCustomer(customerId, t));
            var view = contextIds.Select(policyContextId =>
            {
                var events = _eventStore.Get(policyContextId);
                return _player.Handle<IPolicyContext, PolicyView>(events);
            });
            return view.ToList();
        }

        private static bool IsEventForCustomer(int customerId, IEvent<IPolicyContext> t)
        {
            var @event = t as PolicyCreatedEvent;
            return @event?.CustomerId == customerId;
        }
    }
}