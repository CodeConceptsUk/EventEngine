using Application.Interfaces;
using Application.Interfaces.Domain;

namespace Application.Commands
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
