using System;
using Policy.Application.Interfaces;

namespace Policy.Plugin.Isa.Policy.Events
{
    public class AppliedFundChargeEvent : IsaPolicyEvent
    {
        public AppliedFundChargeEvent(Guid eventContextId, string fundId, decimal units)
            : base (eventContextId)
        {
            FundId = fundId;
            Units = units;
        }
        
        public string FundId { get; set; }

        public decimal Units { get; set; }
    }
}