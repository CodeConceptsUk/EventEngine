using System.Collections.Generic;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Interfaces.Domain;

namespace Policy.Plugin.Isa.Policy.Views.PolicyView
{
    public class PolicyView : IView
    {
        public PolicyView()
        {
            Funds = new List<IFund>();
        }

        public string PolicyNumber { get; set; }

        public int CustomerId { get; set; }

        public IList<IFund> Funds { get; set; }
    }
}