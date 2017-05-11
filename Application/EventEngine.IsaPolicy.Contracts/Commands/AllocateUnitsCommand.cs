using System;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Commands
{
    public class AllocateUnitsCommand : IsaPolicyCommand
    {
        public AllocateUnitsCommand(string policyNumber, DateTime dateOfAllocation)
        {
            PolicyNumber = policyNumber;
            DateOfAllocation = dateOfAllocation;
        }

        public string PolicyNumber { get; }

        public DateTime DateOfAllocation { get; }
    }
}
