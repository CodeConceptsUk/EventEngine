using System;
using System.Runtime.Serialization;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Commands
{
    [DataContract(Namespace = "http://codeconcepts.co.uk/commands/isapolicy")]
    public class CreateChargeCommand : IsaPolicyCommand
    {
        public CreateChargeCommand(string policyNumber, string fundId, Guid portionId,  DateTime chargeDate)
        {
            PolicyNumber = policyNumber;
            FundId = fundId;
            PortionId = portionId;
            ChargeDate = chargeDate;
        }

        [DataMember]
        public string PolicyNumber { get; set; }

        [DataMember]
        public string FundId { get; set; }

        [DataMember]
        public Guid PortionId { get; set; }

        [DataMember]
        public DateTime ChargeDate { get; set; }
    }
}
