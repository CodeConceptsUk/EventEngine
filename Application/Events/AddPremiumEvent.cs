using System;
using Application.Interfaces;
using Application.Interfaces.Domain;

namespace Application.Events
{
    public class AddPremiumEvent : IEvent<IPolicyContext>
    {
        public AddPremiumEvent(Guid eventContextId, string fundId, decimal premium)
        {
            EventContextId = eventContextId;
            FundId = fundId;
            Premium = premium;
            EventId = Guid.NewGuid();
            EventDateTime = DateTime.Now;
        }

        public string FundId { get; set; }

        public decimal Premium { get; set; }

        public Guid EventContextId { get; set; }

        public Guid EventId { get; set; }

        public DateTime EventDateTime { get; set; }
    }
}