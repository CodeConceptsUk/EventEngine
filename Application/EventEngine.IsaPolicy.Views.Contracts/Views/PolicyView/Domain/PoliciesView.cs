using System.Collections.Generic;
using CodeConcepts.EventEngine.Contracts.Interfaces;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Views.PolicyView.Domain
{
    public class PoliciesView : IView
    {
        public IEnumerable<PolicyView> Policies { get; set; }
    }
}