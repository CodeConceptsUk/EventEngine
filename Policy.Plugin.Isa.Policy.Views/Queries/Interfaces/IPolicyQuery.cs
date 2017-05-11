using System.Collections.Generic;
using Policy.Plugin.Isa.Policy.Views.Views.PolicyView.Domain;

namespace Policy.Plugin.Isa.Policy.Views.Queries
{
    public interface IPolicyQuery
    {
        PolicyView Read(string policyNumber);

        IEnumerable<PolicyView> Read(int customerId);
    }
}