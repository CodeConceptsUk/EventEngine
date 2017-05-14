using System.Runtime.Serialization;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Commands
{
    [DataContract(Namespace = "http://codeconcepts.co.uk/commands/isapolicy")]
    public class ProcessChargesCommand : IsaPolicyCommand
    {
        public ProcessChargesCommand(string policyNumber)
        {
            PolicyNumber = policyNumber;
        }

        [DataMember]
        public string PolicyNumber { get; set; }
    }
}