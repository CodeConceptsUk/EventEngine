using System;
using System.Collections.Generic;
using CodeConcepts.EventEngine.Application.Interfaces;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Queries.Interfaces
{
    public interface IPolicyEventContextIdQuery : IQuery
    {
        Guid? GeteventContextId(string policyNumber);

        IEnumerable<Guid> GeteventContextId(int clientId);
    }
}