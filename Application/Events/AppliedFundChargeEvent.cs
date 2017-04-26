using System;
using Application.Interfaces;
using Application.Interfaces.Domain;

namespace Application.Events
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