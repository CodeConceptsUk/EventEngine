using System;
using System.Linq;
using Policy.Application.Interfaces;
using Policy.Application.Interfaces.Repositories;
using Policy.Plugin.Isa.Policy.Interfaces.Queries;
using Policy.Plugin.Isa.Policy.Views.PolicyView;
// ReSharper disable PossibleMultipleEnumeration

namespace Policy.Plugin.Isa.Policy.Queries
{
    public class SinglePolicyQuery : ISinglePolicyQuery
    {
        private readonly IEventStoreRepository _eventStore;
        private readonly ISnapshotStore<PolicyView> _snapshotStoreStore;
        private readonly IEventPlayer _player;

        public SinglePolicyQuery(IEventStoreRepository eventStore,
            ISnapshotStore<PolicyView> snapshotStore, IEventPlayer player)
        {
            _eventStore = eventStore;
            _snapshotStoreStore = snapshotStore;
            _player = player;
        }

        public PolicyView Build(Guid contextId)
        {
            var snapshot = _snapshotStoreStore.Get(contextId); //   snapshot => snapshot.)

            var events = _eventStore.Get(contextId, snapshot?.Event.EventId);
            if (!events.Any())
                return snapshot?.View;

            var view = _player.Handle(events, snapshot?.View ?? new PolicyView());

            _snapshotStoreStore.Add(view, events.Last());

            return view;
        }
    }
}