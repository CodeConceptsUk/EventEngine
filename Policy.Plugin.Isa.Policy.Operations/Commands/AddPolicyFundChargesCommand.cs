namespace Policy.Plugin.Isa.Policy.Commands.Commands
{
    public class AddPolicyFundChargesCommand : IsaPolicyCommand
    {
        public AddPolicyFundChargesCommand(string policyNumber)
        {
            PolicyNumber = policyNumber;
        }

        public string PolicyNumber { get; }
    }
}
