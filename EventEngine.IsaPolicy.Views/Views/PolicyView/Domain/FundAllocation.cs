using System;
using System.Collections.Generic;

namespace Policy.Plugin.Isa.Policy.Views.Views.PolicyView.Domain
{
    public class FundAllocation
    {
        public Guid PortionId { get; set; }

        public decimal Units { get; set; }

        public decimal ShadowUnits { get; set; }

        public PremiumPartition PremiumPartition { get; set; }

        public IList<Charge> Charges { get; set; } = new List<Charge>();
    }
}