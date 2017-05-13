using System;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Events
{
    public class UnitsAllocatedEvent : IsaPolicyEvent
    {
        public UnitsAllocatedEvent(Guid eventContextId, string premiumId, Guid portionId, string fundId, decimal units, DateTime allocationDateTime)
            : base (eventContextId)
        {
            PremiumId = premiumId;
            PortionId = portionId;
            FundId = fundId;
            Units = units;
            AllocationDateTime = allocationDateTime;
        }

        public string PremiumId { get; }

        public Guid PortionId { get; }

        public string FundId { get; }

        public decimal Units { get; }

        public DateTime AllocationDateTime { get; }
    }
}