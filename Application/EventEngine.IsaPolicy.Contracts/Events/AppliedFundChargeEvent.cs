using System;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Events
{
    public class AppliedFundChargeEvent : IsaPolicyEvent
    {
        public AppliedFundChargeEvent(Guid eventContextId, string fundId, Guid portionId, decimal units, DateTime chargeDate)
            : base (eventContextId)
        {
            FundId = fundId;
            PortionId = portionId;
            Units = units;
            ChargeDate = chargeDate;
        }
        
        public string FundId { get; set; }

        public Guid PortionId { get; set; }

        public decimal Units { get; set; }

        public DateTime ChargeDate { get; set; }
    }
}