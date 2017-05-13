using System;
using System.Runtime.Serialization;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Commands
{
    [DataContract(Namespace = "http://codeconcepts.co.uk/commands/isapolicy")]
    public class SetPremiumAsReceivedCommand : IsaPolicyCommand
    {
        public SetPremiumAsReceivedCommand(string policyNumber, string premiumId, DateTime dateTimeReceived)
        {
            PolicyNumber = policyNumber;
            PremiumId = premiumId;
            DateTimeReceived = dateTimeReceived;
        }

        [DataMember]
        public string PolicyNumber { get; set; }

        [DataMember]
        public string PremiumId { get; set; }

        [DataMember]
        public DateTime DateTimeReceived { get; set; }
    }
}