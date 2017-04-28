using System;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Interfaces.Domain;

namespace Policy.Plugin.Isa.Policy.Events
{
    public class AppliedFundChargeEvent : IEvent<IPolicyContext>
    {
        public AppliedFundChargeEvent(Guid eventContextId, string fundId )
        {
            EventContextId = eventContextId;
            FundId = fundId;
            EventId = Guid.NewGuid();
            EventDateTime = DateTime.Now;
        }

        public Guid EventContextId { get; set; }

        public Guid EventId { get; set; }

        public DateTime EventDateTime { get; set; }
        
        public string FundId { get; }
    }
}