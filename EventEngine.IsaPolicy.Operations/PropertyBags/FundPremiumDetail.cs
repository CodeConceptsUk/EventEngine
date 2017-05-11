using System;
using System.Runtime.Serialization;

namespace Policy.Plugin.Isa.Policy.Operations.PropertyBags
{
    [DataContract(Namespace = "http://codeconcepts.co.uk/commands")]
    public class FundPremiumDetail
    {
        public FundPremiumDetail(Guid partitionId, string fundId, decimal amount)
        {
            PartitionId = partitionId;
            FundId = fundId;
            Amount = amount;
        }

        [DataMember]
        public string FundId { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public Guid PartitionId { get; set;  }
    }
}