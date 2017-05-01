using System;

namespace Policy.Plugin.Isa.Policy.Views.Views.PolicyView.Domain
{
    public class PremiumPartition
    {
        public string FundId { get; set; }

        public decimal Amount { get; set; }

        public Guid PortionId { get; set; }
    }
}