using System;
using System.Runtime.Serialization;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Commands
{
    [DataContract(Namespace = "http://codeconcepts.co.uk/commands/isapolicy")]
    public class CreatePolicyCommand : IsaPolicyCommand
    {
        public CreatePolicyCommand(Guid contextId, string customerId)
        {
            ContextId = contextId;
            CustomerId = customerId;
        }

        [DataMember]
        public string CustomerId { get; set; }

        [DataMember]
        public Guid ContextId { get; set; }
    }
}