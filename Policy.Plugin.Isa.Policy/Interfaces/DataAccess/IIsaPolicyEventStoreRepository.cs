using System;
using System.Collections.Generic;
using Policy.Application.Interfaces.Repositories;
using Policy.Plugin.Isa.Policy.Events;

namespace Policy.Plugin.Isa.Policy.Interfaces.DataAccess
{
    public interface IIsaPolicyEventStoreRepository : IEventStoreRepository<IsaPolicyEvent>
    {
        Guid FindContextIds(string policyNumber);

        IEnumerable<Guid> FindContextIds(int customerId);

        IEnumerable<IsaPolicyEvent> Get(Guid eventContextId, Guid? afterEventId = null);
    }
}