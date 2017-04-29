using System;
using System.Collections.Generic;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Interfaces.Domain;
using Policy.Plugin.Isa.Policy.Views;

namespace Policy.Plugin.Isa.Policy.Interfaces.Queries
{
    public interface IPolicyEventContextIdQuery : IQuery<PolicyContextView, IPolicyContext>
    {
        Guid? GetEventContextId(string policyNumber);

        IEnumerable<Guid> GetEventContextId(int clientId);
    }
}