using System;
using System.Collections.Generic;
using Policy.Application.Interfaces;

namespace Policy.Plugin.Isa.Policy.Views.Queries
{
    public interface IPolicyEventContextIdQuery : IQuery
    {
        Guid? GeteventContextId(string policyNumber);

        IEnumerable<Guid> GeteventContextId(int clientId);
    }
}