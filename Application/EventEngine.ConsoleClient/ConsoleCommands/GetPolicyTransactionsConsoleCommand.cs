using System;
using System.Collections.Generic;
using CodeConcepts.CliConsole;
using CodeConcepts.EventEngine.ClientLibrary.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Queries;
using CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Views.PolicyTransactionsView;
using CodeConcepts.FrameworkExtensions.LinqExtensions;

namespace CodeConcepts.EventEngine.ConsoleClient.ConsoleCommands
{
    public class GetPolicyTransactionsConsoleCommand : InlineConsoleCommand
    {
        private readonly ICommandChannelClientFactory _commandChannelClientFactory;
        private readonly ConsoleProxy _console;
        private string _policyNumber;

        public GetPolicyTransactionsConsoleCommand(ICommandChannelClientFactory commandChannelClientFactory, ConsoleProxy console)
            : base("GetPolicyTransactions", "Get the transactions relating to a policy")
        {
            _commandChannelClientFactory = commandChannelClientFactory;
            _console = console;

            HasRequiredOption<string>("PolicyNumber", "The policy number for the policy to retrieve", p => _policyNumber = p);
        }

        protected override void Execute()
        {
            try
            {
                var client = _commandChannelClientFactory.Create();
                var transactionView = client.DispatchQuery(new GetPolicyTransactionsForPolicyNumberQuery(_policyNumber)) as PolicyTransactionView;
                var data = new List<Tuple<string, decimal, DateTime>>();

                transactionView.Transactions.ForEach(transaction =>
                {
                    data.Add(new Tuple<string, decimal, DateTime>(
                        transaction.Type,
                        transaction.Value,
                        transaction.TransactionDateTime));
                });

                _console.WriteLine(data.ToStringTable(
                    new[] { "Transaction Type", "Value", "Date/Time" },
                    t => t.Item1,
                    t => t.Item2.ToString("0.000000000000000"),
                    t => t.Item3));
            }
            catch (Exception e)
            {
                _console.WriteLine($"There was an error retrieving policy {_policyNumber}, {e.Message}");
            }
        }
    }
}