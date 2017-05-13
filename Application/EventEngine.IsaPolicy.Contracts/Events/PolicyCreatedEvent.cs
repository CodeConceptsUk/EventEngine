using System;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Events
{
    public class PolicyCreatedEvent : IsaPolicyEvent
    {
        public PolicyCreatedEvent(string policyNumber, int customerId)
            : base (Guid.NewGuid())
        {
            PolicyNumber = policyNumber;
            CustomerId = customerId;
        }

        public string PolicyNumber { get; set; }

        public int CustomerId { get; set; }
    }
}