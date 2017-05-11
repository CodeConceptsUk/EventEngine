using System;
using System.Runtime.Serialization;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Commands
{
    [DataContract(Namespace = "http://codeconcepts.co.uk/commands/isapolicy")]
    public class AllocateUnitsCommand : IsaPolicyCommand
    {
        public AllocateUnitsCommand(string policyNumber, DateTime dateOfAllocation)
        {
            PolicyNumber = policyNumber;
            DateOfAllocation = dateOfAllocation;
        }

        [DataMember]
        public string PolicyNumber { get; }

        [DataMember]
        public DateTime DateOfAllocation { get; }
    }
}
