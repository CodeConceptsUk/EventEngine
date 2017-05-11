using System;
using System.Collections.Generic;
using CliConsole;
using FrameworkExtensions.LinqExtensions;
using Policy.Plugin.Isa.Policy.Views.Queries;

namespace Program.ConsoleCommands
{
    public class GetPolicyTransactionsConsoleCommand : InlineConsoleCommand
    {
        private readonly ITransactionQuery _transactionQuery;
        private readonly ConsoleProxy _console;
        private string _policyNumber;

        public GetPolicyTransactionsConsoleCommand(ITransactionQuery transactionQuery, ConsoleProxy console)
            : base("GetPolicyTransactions", "Get the transactions relating to a policy")
        {
            _transactionQuery = transactionQuery;
            _console = console;

            HasRequiredOption<string>("PolicyNumber", "The policy number for the policy to retrieve", p => _policyNumber = p);
        }

        protected override void Execute()
        {
            try
            {
                var transactionView = _transactionQuery.Read(_policyNumber);
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
                    t => t.Item2,
                    t => t.Item3));
            }
            catch (Exception e)
            {
                _console.WriteLine($"There was an error retrieving policy {_policyNumber}, {e.Message}");
            }
        }
    }
}