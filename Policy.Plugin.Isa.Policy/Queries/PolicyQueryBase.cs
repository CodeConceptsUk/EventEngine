using System;
using System.Collections.Generic;
using System.Linq;
using Policy.Application.Interfaces;
using Policy.Application.Interfaces.Repositories;
using Policy.Plugin.Isa.Policy.Interfaces.Domain;
using Policy.Plugin.Isa.Policy.Views;

namespace Policy.Plugin.Isa.Policy.Queries
{
    public abstract class PolicyQueryBase <TView>: IQuery<TView, IPolicyContext>
        where TView : class, IView<IPolicyContext>
    {
        protected readonly IEventStoreRepository<IPolicyContext> EventStore;
        protected readonly IEventPlayer Player;

        protected PolicyQueryBase(IEventStoreRepository<IPolicyContext> eventStore, IEventPlayer player)
        {
            EventStore = eventStore;
            Player = player;
        }

        protected IEnumerable<PolicyView> GetContextEvents(IEnumerable<Guid> contextIds)
        {
            var view = contextIds.Select(policyContextId =>
            {
                var events = EventStore.Get(policyContextId);
                return Player.Handle<IPolicyContext, PolicyView>(events);
            });
            return view.ToList();
        }
    }
}