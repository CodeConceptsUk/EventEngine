using System;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Interfaces.Domain;

namespace Policy.Plugin.Isa.Policy.Events
{
    public class UnitsAllocatedEvent : IEvent<IPolicyContext>
    {
        public UnitsAllocatedEvent(Guid eventContextId, string fundId, decimal units, decimal usedPremium, DateTime allocationDateTime)
        {
            EventContextId = eventContextId;
            FundId = fundId;
            Units = units;
            UsedPremium = usedPremium;
            AllocationDateTime = allocationDateTime;
            EventId = Guid.NewGuid();
            EventDateTime = DateTime.Now;
        }

        public Guid EventContextId { get; }

        public string FundId { get; }

        public decimal Units { get; }

        public decimal UsedPremium { get; }

        public DateTime AllocationDateTime { get; }

        public Guid EventId { get; }

        public DateTime EventDateTime { get;  }
    }
}