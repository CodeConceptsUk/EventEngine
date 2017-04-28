using System;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Interfaces.Domain;

namespace Policy.Plugin.Isa.Policy.Events
{
    public class PolicyCreatedEvent : IEvent<IPolicyContext>
    {
        public PolicyCreatedEvent(string policyNumber, int customerId)
        {
            PolicyNumber = policyNumber;
            CustomerId = customerId;
            EventContextId = Guid.NewGuid();
        }

        public Guid EventContextId { get; set; }

        public Guid EventId { get; set; }

        public DateTime EventDateTime { get; set; }

        public string PolicyNumber { get; set; }

        public int CustomerId { get; set; }
    }
}