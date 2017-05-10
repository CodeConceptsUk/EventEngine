using System;
using System.Runtime.Serialization;
using Policy.Plugin.Isa.Policy.Operations.BaseTypes;

namespace Policy.Plugin.Isa.Policy.Operations.Commands
{
    [DataContract]
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
