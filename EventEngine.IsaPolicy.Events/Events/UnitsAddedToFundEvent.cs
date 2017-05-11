using System;

namespace Policy.Plugin.Isa.Policy.Events
{
    public class UnitsAddedToFundEvent : IsaPolicyEvent
    {
        public UnitsAddedToFundEvent(Guid eventContextId, Guid portionId, string fundId, decimal units, DateTime dateTimeAdded) 
            : base(eventContextId)
        {
            PortionId = portionId;
            FundId = fundId;
            Units = units;
            DateTimeAdded = dateTimeAdded;
        }

        public Guid PortionId { get; set;  }

        public string FundId { get; set; }

        public decimal Units { get; set; }

        public DateTime DateTimeAdded { get; set; }
    }
}