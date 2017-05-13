using System;
using System.Collections.Generic;
using CodeConcepts.EventEngine.Api.Contracts;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueries
{
    public interface IPolicyEventContextIdQuery 
    {
        Guid? GeteventContextId(string policyNumber);

        IEnumerable<Guid> GeteventContextId(int clientId);
    } //TODO refactor the name of property above
}