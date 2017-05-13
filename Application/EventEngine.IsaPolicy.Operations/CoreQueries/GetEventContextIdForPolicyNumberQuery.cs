using System;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueries;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.DataAccess;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreQueries
{
    public partial class GetEventContextIdForPolicyNumberQuery : IGetEventContextIdForPolicyNumberQuery
    {
        private readonly IIsaPolicyEventStoreRepository _eventStore;

        public GetEventContextIdForPolicyNumberQuery(IIsaPolicyEventStoreRepository eventStore)
        {
            _eventStore = eventStore;
        }

        public Guid? GetEventContextId(string policyNumber)
        {
            var contextIds = _eventStore.FindContextIds(policyNumber);
            return contextIds;
        }
    }
}