using System.Collections.Generic;
using Application.Interfaces;
using Application.Interfaces.Domain;
using Application.PropertyBags;

namespace Application.Commands
{
    public class AddPremiumCommand : ICommand<IPolicyContext>
    {
        public AddPremiumCommand(string policyNumber, FundPremiumDetails fundPremiumDetails)
        {
            PolicyNumber = policyNumber;
            FundPremiumDetails = fundPremiumDetails;
        }

        public string PolicyNumber { get; }

        public FundPremiumDetails FundPremiumDetails { get; }
    }
}
