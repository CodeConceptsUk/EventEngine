using System.Collections.Generic;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Views.PoliciesView
{
    public class PoliciesView : IsaPolicyView
    {
        public IEnumerable<PolicyView.PolicyView> Policies { get; set; }
    }
}