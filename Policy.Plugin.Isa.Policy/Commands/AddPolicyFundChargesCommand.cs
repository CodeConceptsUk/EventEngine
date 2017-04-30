using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Interfaces.Domain;

namespace Policy.Plugin.Isa.Policy.Commands
{
    public class AddPolicyFundChargesCommand : ICommand<IPolicyContext>
    {
        public AddPolicyFundChargesCommand(string policyNumber)
        {
            PolicyNumber = policyNumber;
        }

        public string PolicyNumber { get; }
    }
}
