using System;
using CliConsole;
using CliConsole.Interfaces;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Operations.BaseTypes;
using Policy.Plugin.Isa.Policy.Operations.Commands;
using Policy.Plugin.Isa.Policy.Operations.PropertyBags;
using Policy.Plugin.Isa.Policy.Views.Queries;

namespace Program.ConsoleCommands
{
    public class AddPremiumConsoleCommand : InlineConsoleCommand
    {
        private readonly ICommandDispatcher<IsaPolicyCommand> _dispatcher;
        private readonly IPolicyQuery _policyQuery;
        private readonly IConsoleProxy _console;
        private string _policyNumber;
        private string _fundId;
        private decimal _premiumAmount;

        public AddPremiumConsoleCommand(ICommandDispatcher<IsaPolicyCommand> dispatcher, IPolicyQuery policyQuery, IConsoleProxy console)
            : base("AddPremium", "Add a new premium to a policy")
        {
            _dispatcher = dispatcher;
            _policyQuery = policyQuery;
            _console = console;

            HasRequiredOption<string>("PolicyNumber", "The policy number", p => _policyNumber = p);
            HasRequiredOption<string>("FundId", "The fund to invest into", p => _fundId = p);
            HasRequiredOption<decimal>("Amount", "The total premium amount", p => _premiumAmount = p);
        }

        protected override void Execute()
        {
            try
            {
                if (_policyQuery.Read(_policyNumber) == null)
                    return;

                var addPremiumCommand = new AddPremiumCommand(
                    _policyNumber,
                    Guid.NewGuid().ToString(),
                    DateTime.Now,
                    new[] { new FundPremiumDetail(Guid.NewGuid(), _fundId, _premiumAmount) });
                _dispatcher.Apply(addPremiumCommand);
            }
            catch (Exception e)
            {
                _console.WriteLine($"There was an error retrieving policy {_policyNumber}, {e.Message}");
            }
        }
    }
}