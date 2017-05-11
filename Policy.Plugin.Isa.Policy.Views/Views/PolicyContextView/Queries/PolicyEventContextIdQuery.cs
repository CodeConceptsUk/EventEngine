using System;
using System.Collections.Generic;
using Policy.Plugin.Isa.Policy.Interfaces.DataAccess;
using Policy.Plugin.Isa.Policy.Views.Queries;

namespace Policy.Plugin.Isa.Policy.Views.Views.PolicyContextView.Queries
{
    public class PolicyEventContextIdQuery : IPolicyEventContextIdQuery
    {
        private readonly IIsaPolicyEventStoreRepository _eventStore;

        public PolicyEventContextIdQuery(IIsaPolicyEventStoreRepository eventStore)
        {
            _eventStore = eventStore;
        }

        public Guid? GeteventContextId(string policyNumber)
        {
            var contextIds = _eventStore.FindContextIds(policyNumber);
            return contextIds;
        }

        public IEnumerable<Guid> GeteventContextId(int clientId)
        {
            var contextIds = _eventStore.FindContextIds(clientId);
            return contextIds;
        }
    }
}