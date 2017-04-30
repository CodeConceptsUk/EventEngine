using System;
using Policy.Application.Interfaces;

namespace Policy.Plugin.Isa.Policy.Commands
{
    public class UnitAllocationCommand : ICommand
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
