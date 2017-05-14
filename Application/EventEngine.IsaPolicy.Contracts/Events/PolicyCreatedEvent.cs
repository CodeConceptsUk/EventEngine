using System;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Events
{
    public class PolicyCreatedEvent : IsaPolicyEvent
    {
        public PolicyCreatedEvent(Guid contextId, string policyNumber, string customerId)
            : base (contextId)
        {
            PolicyNumber = policyNumber;
            CustomerId = customerId;
        }

        public string PolicyNumber { get; set; }

        public string CustomerId { get; set; }
    }
}