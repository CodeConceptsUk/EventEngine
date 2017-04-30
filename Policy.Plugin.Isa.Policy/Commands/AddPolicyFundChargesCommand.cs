using Policy.Application.Interfaces;

namespace Policy.Plugin.Isa.Policy.Commands
{
    public class AddPolicyFundChargesCommand : ICommand
    {
        public AddPolicyFundChargesCommand(string policyNumber)
        {
            PolicyNumber = policyNumber;
        }

        public string PolicyNumber { get; }
    }
}
