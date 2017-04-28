using System;

namespace Policy.Plugin.Isa.Policy.Interfaces.Queries
{
    public interface IPolicyEventContextIdQuery
    {
        Guid GetEventContextId(string policyNumber);
    }
}