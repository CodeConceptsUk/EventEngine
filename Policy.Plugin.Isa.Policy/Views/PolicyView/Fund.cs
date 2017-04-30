using System.Collections.Generic;

namespace Policy.Plugin.Isa.Policy.Views.PolicyView
{
    public class Fund 
   {
        public string FundId { get; set; }

        public IList<FundAllocation> Allocations { get; set; } = new List<FundAllocation>();
    }
}