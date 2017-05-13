using System;
using System.Runtime.Serialization;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Commands.PropertyBags
{
    [DataContract(Namespace = "http://codeconcepts.co.uk/commands/isapolicy")]
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