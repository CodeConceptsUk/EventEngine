using System;
using Policy.Application.Interfaces;

namespace Policy.Plugin.Isa.Policy.Events
{
    public class UnitsAllocatedEvent : IsaPolicyEvent
    {
        public UnitsAllocatedEvent(Guid eventContextId, string fundId, decimal units, decimal usedPremium, DateTime allocationDateTime)
            : base (eventContextId)
        {
            FundId = fundId;
            Units = units;
            UsedPremium = usedPremium;
            AllocationDateTime = allocationDateTime;
        }

        public string FundId { get; }

        public decimal Units { get; }

        public decimal UsedPremium { get; }

        public DateTime AllocationDateTime { get; }
    }
}