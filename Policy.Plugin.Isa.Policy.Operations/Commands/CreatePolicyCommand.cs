using System.Runtime.Serialization;
using Policy.Plugin.Isa.Policy.Operations.BaseTypes;

namespace Policy.Plugin.Isa.Policy.Operations.Commands
{
    [DataContract(Namespace = "http://codeconcepts.co.uk/commands")]
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