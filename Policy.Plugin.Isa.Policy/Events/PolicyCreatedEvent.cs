using System;

namespace Policy.Plugin.Isa.Policy.Events
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