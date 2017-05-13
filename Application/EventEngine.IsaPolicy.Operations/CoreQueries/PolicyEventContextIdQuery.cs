using System;
using System.Collections.Generic;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueries;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.DataAccess;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreQueries
{
    public class PolicyEventContextIdQuery : IPolicyEventContextIdQuery //TODO: ISystemQuery ?
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