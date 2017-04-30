using System;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.PropertyBags;

namespace Policy.Plugin.Isa.Policy.Commands
{
    public class AddPremiumCommand : ICommand
    {
        public AddPremiumCommand(string policyNumber, DateTime premiumDateTime,  FundPremiumDetails fundPremiumDetails)
        {
            PolicyNumber = policyNumber;
            PremiumDateTime = premiumDateTime;
            FundPremiumDetails = fundPremiumDetails;
        }

        public string PolicyNumber { get; }
        public DateTime PremiumDateTime { get; }

        public FundPremiumDetails FundPremiumDetails { get; }
    }
}
