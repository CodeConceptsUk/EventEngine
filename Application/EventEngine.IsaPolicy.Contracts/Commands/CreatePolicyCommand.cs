using System.Runtime.Serialization;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Commands
{
    [DataContract(Namespace = "http://codeconcepts.co.uk/commands/isapolicy")]
    public class CreatePolicyCommand : IsaPolicyCommand
    {
        public CreatePolicyCommand(int customerId)
        {
            CustomerId = customerId;
        }

        [DataMember]
        public int CustomerId { get; set; }
    }
}