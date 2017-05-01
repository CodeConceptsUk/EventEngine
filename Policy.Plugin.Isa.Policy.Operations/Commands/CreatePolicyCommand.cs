using Policy.Plugin.Isa.Policy.Operations.BaseTypes;

namespace Policy.Plugin.Isa.Policy.Operations.Commands
{
    public class CreatePolicyCommand : IsaPolicyCommand
    {
        public CreatePolicyCommand(int customerId)
        {
            CustomerId = customerId;
        }

        public int CustomerId { get; }
    }
}