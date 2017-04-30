using System;
using Policy.Application.Interfaces;

namespace Policy.Plugin.Isa.Policy.Events
{
    public class PolicyCreatedEvent : IEvent
    {
        public PolicyCreatedEvent(string policyNumber, int customerId)
        {
            PolicyNumber = policyNumber;
            CustomerId = customerId;
            EventContextId = Guid.NewGuid();
            EventId = Guid.NewGuid();
            EventDateTime = DateTime.Now;
        }

        public Guid EventContextId { get; set; }

        public Guid EventId { get; set; }

        public DateTime EventDateTime { get; set; }

        public string PolicyNumber { get; set; }

        public int CustomerId { get; set; }
    }
}