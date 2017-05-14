using System.Collections.Generic;
using CodeConcepts.EventEngine.Api.Contracts;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Views.PolicyView
{
    public class PoliciesView : IView
    {
        public IEnumerable<PolicyView> Policies { get; set; }
    }
}