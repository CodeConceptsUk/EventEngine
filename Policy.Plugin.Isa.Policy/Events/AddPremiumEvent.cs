using System;

namespace Policy.Plugin.Isa.Policy.Events
{
    public class AddPremiumEvent : IsaPolicyEvent
    {
        public AddPremiumEvent(Guid eventContextId, string fundId, decimal premium, DateTime  premiumDateTime)
            :base(eventContextId)
        {
            FundId = fundId;
            Premium = premium;
            PremiumDateTime = premiumDateTime;
        }

        public string FundId { get; set; }

        public decimal Premium { get; set; }

        public DateTime PremiumDateTime { get; set; }
    }
}