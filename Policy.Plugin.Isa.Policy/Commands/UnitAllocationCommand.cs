using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Interfaces.Domain;

namespace Policy.Plugin.Isa.Policy.Commands
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
