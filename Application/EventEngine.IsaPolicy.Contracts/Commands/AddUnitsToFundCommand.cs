using System;
using System.Runtime.Serialization;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Commands
{
    [DataContract(Namespace = "http://codeconcepts.co.uk/commands/isapolicy")]
    public class AddUnitsToFundCommand : IsaPolicyCommand
    {
        public AddUnitsToFundCommand(string policyNumber, string fundId, decimal units, DateTime dateTimeAdded)
        {
            PolicyNumber = policyNumber;
            FundId = fundId;
            Units = units;
            DateTimeAdded = dateTimeAdded;
        }

        [DataMember]
        public string PolicyNumber { get; set; }

        [DataMember]
        public string FundId { get; set; }

        [DataMember]
        public decimal Units { get; set; }

        [DataMember]
        public DateTime DateTimeAdded { get; set; }
    }
}