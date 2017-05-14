using System.Collections.Generic;
using CodeConcepts.EventEngine.Api.Contracts;

namespace CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Views.PolicyView
{
    public class PolicyView : IView
    {
        public string PolicyNumber { get; set; }

        public string CustomerId { get; set; }

        public IList<Premium> Premiums { get; set; } = new List<Premium>();

        public IList<Fund> Funds { get; set; } = new List<Fund>();
    }
}