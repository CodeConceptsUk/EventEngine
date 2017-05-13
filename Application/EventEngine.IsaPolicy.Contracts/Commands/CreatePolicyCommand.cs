using System;
using System.Runtime.Serialization;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Commands
{
    [DataContract(Namespace = "http://codeconcepts.co.uk/commands/isapolicy")]
    public class CreatePolicyCommand : IsaPolicyCommand
    {
        public CreatePolicyCommand(Guid contextId, int customerId, string policyNumber)
        {
            ContextId = contextId;
            CustomerId = customerId;
            PolicyNumber = policyNumber;
        }

        [DataMember]
        public int CustomerId { get; set; }

        public Guid ContextId { get; set; }

        public string PolicyNumber { get; set; }
    }
}