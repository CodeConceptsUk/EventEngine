using System;
using CodeConcepts.CliConsole;
using CodeConcepts.EventEngine.ClientLibrary.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Commands;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.PropertyBags;

namespace CodeConcepts.EventEngine.ConsoleClient.ConsoleCommands
{
    public class AddPremiumConsoleCommand : InlineConsoleCommand
    {
        private readonly ICommandChannelClientFactory _commandChannelClientFactory;
        private string _policyNumber;
        private string _fundId;
        private decimal _premiumAmount;

        public AddPremiumConsoleCommand(ICommandChannelClientFactory commandChannelClientFactory)
            : base("AddPremium", "Add a new premium to a policy")
        {
            _commandChannelClientFactory = commandChannelClientFactory;

            HasRequiredOption<string>("PolicyNumber", "The policy number", p => _policyNumber = p);
            HasRequiredOption<string>("FundId", "The fund to invest into", p => _fundId = p);
            HasRequiredOption<decimal>("Amount", "The total premium amount", p => _premiumAmount = p);
        }

        protected override void Execute()
        {
            var addPremiumCommand = new AddPremiumCommand(
                _policyNumber,
                Guid.NewGuid().ToString(),
                DateTime.Now,
                new[] {new FundPremiumDetail(Guid.NewGuid(), _fundId, _premiumAmount)});

            var client = _commandChannelClientFactory.Create();
            client.DispatchCommand(addPremiumCommand);
        }
    }
}