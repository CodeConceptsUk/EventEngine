namespace Policy.Plugin.Isa.Policy.Commands.Commands
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