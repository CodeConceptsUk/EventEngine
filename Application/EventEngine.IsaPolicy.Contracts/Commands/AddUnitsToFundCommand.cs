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
        public string PolicyNumber { get; }

        [DataMember]
        public string FundId { get; }

        [DataMember]
        public decimal Units { get; }

        [DataMember]
        public DateTime DateTimeAdded { get; }
    }
}