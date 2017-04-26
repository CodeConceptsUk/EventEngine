using Application.Interfaces;
using Application.Interfaces.Domain;

namespace Application.Commands
{
    public class CreatePolicyCommand : ICommand<IPolicyContext>
    {
        public CreatePolicyCommand(int customerId)
        {
            CustomerId = customerId;
        }

        public int CustomerId { get; }
    }
}