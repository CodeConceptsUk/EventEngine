using System;
using CodeConcepts.CliConsole;
using CodeConcepts.EventEngine.ClientLibrary.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Commands;

namespace CodeConcepts.EventEngine.ConsoleClient.ConsoleCommands
{
    public class AddUnitsToFundConsoleCommand : InlineConsoleCommand
    {
        private readonly ICommandChannelClientFactory _commandChannelClientFactory;
        private string _policyNumber;
        private string _fundId;
        private decimal _units;

        public AddUnitsToFundConsoleCommand(ICommandChannelClientFactory commandChannelClientFactory) 
            : base("AddUnitsToFund", "Add units to a known policies fund")
        {
            _commandChannelClientFactory = commandChannelClientFactory;

            HasRequiredOption<string>("PolicyNumber", "The policy number", p => _policyNumber = p);
            HasRequiredOption<string>("FundId", "The fund to invest into", p => _fundId = p);
            HasRequiredOption<decimal>("Units", "The total units to allocate", p => _units = p);
        }

        protected override void Execute()
        {
            var addUnitsToFundCommand = new AddUnitsToFundCommand(_policyNumber, _fundId, _units, DateTime.Now);
            var client = _commandChannelClientFactory.Create();
            client.DispatchCommand(addUnitsToFundCommand);
        }
    }
}