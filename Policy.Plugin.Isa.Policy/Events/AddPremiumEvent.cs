using System;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Interfaces.Domain;

namespace Policy.Plugin.Isa.Policy.Events
{
    public class AddPremiumEvent : IEvent<IPolicyContext>
    {
        public AddPremiumEvent(Guid eventContextId, string fundId, decimal premium, DateTime  premiumDateTime)
        {
            EventContextId = eventContextId;
            FundId = fundId;
            Premium = premium;
            PremiumDateTime = premiumDateTime;
            EventId = Guid.NewGuid();
            EventDateTime = DateTime.Now;
        }

        public string FundId { get; set; }

        public decimal Premium { get; set; }

        public DateTime PremiumDateTime { get; set; }

        public Guid EventContextId { get; set; }

        public Guid EventId { get; set; }

        public DateTime EventDateTime { get; set; }
    }
}