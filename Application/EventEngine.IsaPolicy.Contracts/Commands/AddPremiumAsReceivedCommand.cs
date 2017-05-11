using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.PropertyBags;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Commands
{
    [DataContract(Namespace = "http://codeconcepts.co.uk/commands/isapolicy")]
    public class AddPremiumAsReceivedCommand : IsaPolicyCommand
    {
        public AddPremiumAsReceivedCommand(string policyNumber, string premiumId, DateTime premiumDateTime, IEnumerable<FundPremiumDetail> fundPremiumDetail)
        {
            PolicyNumber = policyNumber;
            PremiumId = premiumId;
            PremiumDateTime = premiumDateTime;
            FundPremiumDetail = fundPremiumDetail;
        }

        [DataMember]
        public string PolicyNumber { get; set; }

        [DataMember]
        public string PremiumId { get; set; }

        [DataMember]
        public DateTime PremiumDateTime { get; set; }

        [DataMember]
        public IEnumerable<FundPremiumDetail> FundPremiumDetail { get; set; }
    }
}