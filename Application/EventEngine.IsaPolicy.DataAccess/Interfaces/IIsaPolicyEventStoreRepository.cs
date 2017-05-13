using System;
using System.Collections.Generic;
using CodeConcepts.EventEngine.Contracts.Interfaces.Repositories;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;

namespace CodeConcepts.EventEngine.IsaPolicy.DataAccess.Interfaces
{
    public interface IIsaPolicyEventStoreRepository : IEventStoreRepository<IsaPolicyEvent>
    {
        Guid FindContextIds(string policyNumber);

        IEnumerable<Guid> FindContextIds(int customerId);

        IEnumerable<IsaPolicyEvent> Get(Guid eventContextId, Guid? afterEventId = null);
    }
}