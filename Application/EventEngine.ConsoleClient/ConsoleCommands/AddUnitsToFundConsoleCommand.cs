using System;
using CliConsole;
using CliConsole.Interfaces;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Operations.BaseTypes;
using Policy.Plugin.Isa.Policy.Operations.Commands;
using Policy.Plugin.Isa.Policy.Views.Queries;

namespace Program.ConsoleCommands
{
    public class AddUnitsToFundConsoleCommand : InlineConsoleCommand
    {
        private readonly ICommandDispatcher<IsaPolicyCommand> _dispatcher;
        private readonly IPolicyQuery _policyQuery;
        private readonly IConsoleProxy _console;
        private string _policyNumber;
        private string _fundId;
        private decimal _units;

        public AddUnitsToFundConsoleCommand(ICommandDispatcher<IsaPolicyCommand> dispatcher, IPolicyQuery policyQuery, IConsoleProxy console) 
            : base("AddUnitsToFund", "Add units to a known policies fund")
        {
            _dispatcher = dispatcher;
            _policyQuery = policyQuery;
            _console = console;

            HasRequiredOption<string>("PolicyNumber", "The policy number", p => _policyNumber = p);
            HasRequiredOption<string>("FundId", "The fund to invest into", p => _fundId = p);
            HasRequiredOption<decimal>("Units", "The total units to allocate", p => _units = p);
        }

        protected override void Execute()
        {
            try
            {
                if (_policyQuery.Read(_policyNumber) == null)
                    return;

                var addUnitsToFundCommand = new AddUnitsToFundCommand(_policyNumber, _fundId, _units, DateTime.Now);
                _dispatcher.Apply(addUnitsToFundCommand);
            }
            catch (Exception e)
            {
                _console.WriteLine($"There was an error retrieving policy {_policyNumber}, {e.Message}");
            }
        }
    }
}