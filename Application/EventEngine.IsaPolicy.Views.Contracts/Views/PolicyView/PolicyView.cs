using System.Collections.Generic;
using CodeConcepts.EventEngine.Api.Contracts;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Views.PolicyView
{
    public class PolicyView : IsaPolicyView
    {
        public string PolicyNumber { get; set; }

        public string CustomerId { get; set; }

        public IList<Premium> Premiums { get; set; } = new List<Premium>();

        public IList<Fund> Funds { get; set; } = new List<Fund>();
    }
}