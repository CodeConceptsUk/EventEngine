using System;
using System.Collections.Generic;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueries;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.DataAccess;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreQueries
{
    public class GetEventContextIdForCustomerIdQuery : IGetEventContextIdForCustomerIdQuery
    {
        private readonly IIsaPolicyEventStoreRepository _eventStore;

        public GetEventContextIdForCustomerIdQuery(IIsaPolicyEventStoreRepository eventStore)
        {
            _eventStore = eventStore;
        }

        public IEnumerable<Guid> GetEventContextIds(int customerId)
        {
            var contextIds = _eventStore.FindContextIds(customerId);
            return contextIds;
        }
    }
}