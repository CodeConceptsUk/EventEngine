using System;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Commands
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