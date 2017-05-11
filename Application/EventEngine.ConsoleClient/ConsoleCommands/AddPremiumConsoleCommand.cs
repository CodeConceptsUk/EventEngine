using System;
using CliConsole;
using CliConsole.Interfaces;
using Policy.Plugin.Isa.Policy.Operations.Commands;
using Policy.Plugin.Isa.Policy.Operations.PropertyBags;
using Policy.Plugin.Isa.Policy.Views.Queries;
using Program.Services;

namespace Program.ConsoleCommands
{
    public class AddPremiumConsoleCommand : InlineConsoleCommand
    {
        private readonly IPolicyQuery _policyQuery;
        private readonly IConsoleProxy _console;
        private readonly ICommandChannelClientFactory _commandChannelClientFactory;
        private string _policyNumber;
        private string _fundId;
        private decimal _premiumAmount;

        public AddPremiumConsoleCommand(IPolicyQuery policyQuery, IConsoleProxy console, ICommandChannelClientFactory commandChannelClientFactory)
            : base("AddPremium", "Add a new premium to a policy")
        {
            _policyQuery = policyQuery;
            _console = console;
            _commandChannelClientFactory = commandChannelClientFactory;

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

                var client = _commandChannelClientFactory.Create();
                client.DispatchCommand(addPremiumCommand);
            }
            catch (Exception e)
            {
                _console.WriteLine($"There was an error retrieving policy {_policyNumber}, {e.Message}");
            }
        }
    }
}