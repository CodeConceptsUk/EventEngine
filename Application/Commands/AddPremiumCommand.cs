using System.Collections.Generic;
using Application.Interfaces;
using Application.Interfaces.Domain;
using Application.PropertyBags;

namespace Application.Commands
{
    public class AddPremiumCommand : ICommand<IPolicyContext>
    {
        public AddPremiumCommand(string policyNumber, IEnumerable<FundPremiumDetails> fundPremiums)
        {
            PolicyNumber = policyNumber;
            FundPremiums = fundPremiums;
        }

        public string PolicyNumber { get; }

        public IEnumerable<FundPremiumDetails> FundPremiums { get; }
    }
}
