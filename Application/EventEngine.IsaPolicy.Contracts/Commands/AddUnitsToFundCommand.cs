using System;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Commands
{
    public class AddUnitsToFundCommand : IsaPolicyCommand
    {
        public AddUnitsToFundCommand(string policyNumber, string fundId, decimal units, DateTime dateTimeAdded)
        {
            PolicyNumber = policyNumber;
            FundId = fundId;
            Units = units;
            DateTimeAdded = dateTimeAdded;
        }

        public string PolicyNumber { get; }

        public string FundId { get; }

        public decimal Units { get; }

        public DateTime DateTimeAdded { get; }
    }
}