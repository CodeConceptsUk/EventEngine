using System;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Interfaces.Domain;

namespace Policy.Plugin.Isa.Policy.Commands
{
    public class UnitAllocationCommand : ICommand<IPolicyContext>
    {
        public UnitAllocationCommand(string policyNumber, DateTime dateOfAllocation)
        {
            PolicyNumber = policyNumber;
            DateOfAllocation = dateOfAllocation;
        }

        public string PolicyNumber { get; }

        public DateTime DateOfAllocation { get; }
    }
}
