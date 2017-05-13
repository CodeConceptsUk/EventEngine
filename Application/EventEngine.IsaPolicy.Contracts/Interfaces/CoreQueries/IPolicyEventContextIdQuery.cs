using System;
using System.Collections.Generic;
using CodeConcepts.EventEngine.Contracts.Interfaces;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueries
{
    public interface IPolicyEventContextIdQuery : IQuery
    {
        Guid? GeteventContextId(string policyNumber);

        IEnumerable<Guid> GeteventContextId(int clientId);
    } //TODO refactor the name of property above
}