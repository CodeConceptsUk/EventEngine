using System;
using System.Collections.Generic;
using Policy.Plugin.Isa.Policy.Commands.PropertyBags;

namespace Policy.Plugin.Isa.Policy.Commands.Commands
{
    public class AddPremiumCommand : IsaPolicyCommand
    {
        public AddPremiumCommand(string policyNumber, string premiumId, DateTime premiumDateTime, IEnumerable<FundPremiumDetail> fundPremiumDetail)
        {
            PolicyNumber = policyNumber;
            PremiumId = premiumId;
            PremiumDateTime = premiumDateTime;
            FundPremiumDetail = fundPremiumDetail;
        }

        public string PolicyNumber { get; }

        public string PremiumId { get; }

        public DateTime PremiumDateTime { get; }

        public IEnumerable<FundPremiumDetail> FundPremiumDetail { get; }
    }
}
