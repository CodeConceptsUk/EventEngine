using System;
using System.Collections.Generic;
using CodeConcepts.EventEngine.Contracts.Interfaces.Repositories;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.DataAccess
{
    public interface IIsaPolicyEventStoreRepository : IEventStoreRepository<IsaPolicyEvent>
    {
        Guid? FindContextId(string policyNumber);

        IEnumerable<Guid> FindContextIds(string customerId);

        IEnumerable<IsaPolicyEvent> Get(Guid eventContextId, Guid? afterEventId = null);
    }
}