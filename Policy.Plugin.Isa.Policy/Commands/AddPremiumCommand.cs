using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Interfaces.Domain;
using Policy.Plugin.Isa.Policy.PropertyBags;

namespace Policy.Plugin.Isa.Policy.Commands
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
