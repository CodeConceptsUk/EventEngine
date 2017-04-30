using System;

namespace Policy.Plugin.Isa.Policy.Commands.Commands
{
    public class UnitAllocationCommand : IsaPolicyCommand
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
