using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Interfaces.Domain;

namespace Policy.Plugin.Isa.Policy.Commands
{
    public class AddFundChargeCommand : ICommand<IPolicyContext>
    {
        public AddFundChargeCommand(string policyNumber, string fundId)
        {
            PolicyNumber = policyNumber;
            FundId = fundId;
        }

        public string PolicyNumber { get; }

        public string FundId { get; }
    }
}
