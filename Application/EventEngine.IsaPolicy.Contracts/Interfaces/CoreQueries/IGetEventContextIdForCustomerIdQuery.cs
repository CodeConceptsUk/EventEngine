using System;
using System.Collections.Generic;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueries
{
    public interface IGetEventContextIdForCustomerIdQuery
    {
        IEnumerable<Guid> GetEventContextIds(int customerId);
    }
}