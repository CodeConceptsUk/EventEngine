using System;
using System.Collections.Generic;
using System.Linq;
using Application.Events;
using Application.Extensions;
using Application.Interfaces;
using Application.Interfaces.Domain;
using Application.Interfaces.Repositories;
using Application.Views;

namespace Application.Queries
{
    public class PolicyQuery : IQuery<PolicyView, IPolicyContext>        
    {
        private readonly IEventStoreRepository<IPolicyContext> _eventStore;
        private readonly IEventPlayer _player;

        public PolicyQuery (IEventStoreRepository<IPolicyContext> eventStore, IEventPlayer player)
        {
            _eventStore = eventStore;
            _player = player;
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

    public class PolicyFundsView : IView<IPolicyContext>
    {
        
    }
}