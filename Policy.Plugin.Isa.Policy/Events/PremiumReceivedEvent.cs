using System;

namespace Policy.Plugin.Isa.Policy.Events
{
    public class PremiumReceivedEvent : IsaPolicyEvent
    {
        public PremiumReceivedEvent(Guid eventContextId, string premiumId, DateTime dateTimeReceived)
            : base(eventContextId)
        {
            PremiumId = premiumId;
            DateTimeReceived = dateTimeReceived;
        }

        public string PremiumId { get; set; }

        public DateTime DateTimeReceived { get; set; }
    }
}