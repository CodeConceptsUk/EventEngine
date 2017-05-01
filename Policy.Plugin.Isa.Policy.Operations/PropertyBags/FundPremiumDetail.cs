using System;

namespace Policy.Plugin.Isa.Policy.Operations.PropertyBags
{
    public class FundPremiumDetail
    {
        public FundPremiumDetail(Guid partitionId, string fundId, decimal amount)
        {
            PartitionId = partitionId;
            FundId = fundId;
            Amount = amount;
        }

        public string FundId { get; }

        public decimal Amount { get; }

        public Guid PartitionId { get; }
    }
}