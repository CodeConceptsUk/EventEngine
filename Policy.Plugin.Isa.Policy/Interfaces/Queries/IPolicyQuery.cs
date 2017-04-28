using System.Collections.Generic;
using Policy.Plugin.Isa.Policy.Views;

namespace Policy.Plugin.Isa.Policy.Interfaces.Queries
{
    public interface IPolicyQuery
    {
        IEnumerable<PolicyView> Read(int customerId);
        IEnumerable<PolicyView> Read(string policyNumber);
    }
}