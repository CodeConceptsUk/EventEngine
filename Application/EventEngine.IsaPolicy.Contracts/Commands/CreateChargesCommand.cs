using System;
using System.Runtime.Serialization;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Commands
{
    [DataContract(Namespace = "http://codeconcepts.co.uk/commands/isapolicy")]
    public class CreateChargesCommand : IsaPolicyCommand
    {
        public CreateChargesCommand(string policyNumber, DateTime chargeDate)
        {
            PolicyNumber = policyNumber;
            ChargeDate = chargeDate;
        }

        [DataMember]
        public string PolicyNumber { get; }

        [DataMember]
        public DateTime ChargeDate { get; }
    }
}
