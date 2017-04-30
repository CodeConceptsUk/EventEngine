using System;
using Policy.Application.Interfaces;

namespace Policy.Plugin.Isa.Policy.Events
{
    public class AppliedFundChargeEvent : IEvent
    {
        public AppliedFundChargeEvent(Guid eventContextId, string fundId, decimal units)
        {
            EventContextId = eventContextId;
            FundId = fundId;
            Units = units;
            EventId = Guid.NewGuid();
            EventDateTime = DateTime.Now;
        }

        public Guid EventContextId { get; set; }

        public Guid EventId { get; set; }

        public DateTime EventDateTime { get; set; }
        
        public string FundId { get; set; }

        public decimal Units { get; set; }
    }
}