using System.Collections.Generic;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Interfaces.Domain;
using Policy.Plugin.Isa.Policy.PropertyBags;

namespace Policy.Plugin.Isa.Policy.Views.PolicyView
{
    public class PolicyView : IView
    {
        public string PolicyNumber { get; set; }

        public int CustomerId { get; set; }

        public IList<Premium> Premiums { get; set; } = new List<Premium>();

        public IList<Fund> Funds { get; set; } = new List<Fund>();
    }
}