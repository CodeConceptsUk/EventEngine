using Policy.Application.Interfaces;

namespace Policy.Plugin.Isa.Policy.Commands
{
    public class CreatePolicyCommand : ICommand
    {
        public CreatePolicyCommand(int customerId)
        {
            CustomerId = customerId;
        }

        public int CustomerId { get; }
    }
}