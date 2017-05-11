using System.Collections.Generic;
using CodeConcepts.EventEngine.IsaPolicy.Views.Views.PolicyView.Domain;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Queries.Interfaces
{
    public interface IPolicyQuery
    {
        PolicyView Read(string policyNumber);

        IEnumerable<PolicyView> Read(int customerId);
    }
}