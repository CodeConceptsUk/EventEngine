using System;
using System.Linq;
using Policy.Application.Interfaces;
using Policy.Application.Interfaces.Repositories;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Interfaces.DataAccess;
using Policy.Plugin.Isa.Policy.Views.Queries;

// ReSharper disable PossibleMultipleEnumeration

namespace Policy.Plugin.Isa.Policy.Views.Views.PolicyView.Queries
{
    public class SinglePolicyQuery : ISinglePolicyQuery
    {
        private readonly IIsaPolicyEventStoreRepository _eventStore;
        private readonly ISnapshotStore<Domain.PolicyView> _snapshotStoreStore;
        private readonly IEventPlayer<IsaPolicyEvent> _player;

        public SinglePolicyQuery(IIsaPolicyEventStoreRepository eventStore,
            ISnapshotStore<Domain.PolicyView> snapshotStore, IEventPlayer<IsaPolicyEvent> player)
        {
            _eventStore = eventStore;
            _snapshotStoreStore = snapshotStore;
            _player = player;
        }

        public Domain.PolicyView Read(Guid contextId)
        {
            var snapshot = _snapshotStoreStore.Get(contextId); //   snapshot => snapshot.)

            var events = _eventStore.Get(contextId, snapshot?.Event.EventId);
            if (!events.Any())
                return snapshot?.View;

            var view = _player.Handle(events, snapshot?.View ?? new Domain.PolicyView());

            _snapshotStoreStore.Add(view, events.Last());

            return view;
        }
    }
}