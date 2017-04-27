using Application.Interfaces;
using Application.Interfaces.Domain;

namespace Application.Commands
{
    public class UnitAllocationCommand : ICommand<IPolicyContext>
    {
        public UnitAllocationCommand(string policyNumber)
        {
            PolicyNumber = policyNumber;
        }

        public string PolicyNumber { get; }
    }
}
