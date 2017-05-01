using System;
using Policy.Plugin.Isa.Policy.Operations.BaseTypes;

namespace Policy.Plugin.Isa.Policy.Operations.Commands
{
    public class SetPremiumAsReceivedCommand : IsaPolicyCommand
    {
        public SetPremiumAsReceivedCommand(string policyNumber, string premiumId, DateTime dateTimeReceived)
        {
            PolicyNumber = policyNumber;
            PremiumId = premiumId;
            DateTimeReceived = dateTimeReceived;
        }

        public string PolicyNumber { get; }

        public string PremiumId { get; }

        public DateTime DateTimeReceived { get; }
    }
}