using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Policy.Plugin.Isa.Policy.Operations.BaseTypes;
using Policy.Plugin.Isa.Policy.Operations.PropertyBags;

namespace Policy.Plugin.Isa.Policy.Operations.Commands
{
    [DataContract(Namespace = "http://codeconcepts.co.uk/commands")]
    public class AddPremiumCommand : IsaPolicyCommand
    {
        public AddPremiumCommand(string policyNumber, string premiumId, DateTime premiumDateTime, IEnumerable<FundPremiumDetail> fundPremiumDetail)
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
