using System;
using System.Collections.Generic;
using Policy.Application.Interfaces;

namespace Policy.Plugin.Isa.Policy.Interfaces.Queries
{
    public interface IPolicyeventContextIdQuery : IQuery
    {
        Guid? GeteventContextId(string policyNumber);

        IEnumerable<Guid> GeteventContextId(int clientId);
    }
}