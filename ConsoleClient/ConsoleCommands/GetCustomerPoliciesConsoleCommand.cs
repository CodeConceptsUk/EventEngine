using System;
using System.Collections.Generic;
using System.Linq;
using CliConsole;
using FrameworkExtensions.LinqExtensions;
using Policy.Plugin.Isa.Policy.Views.Queries;
using Policy.Plugin.Isa.Policy.Views.Views.PolicyView.Domain;

namespace Program.ConsoleCommands
{
    public class GetCustomerPoliciesConsoleCommand : InlineConsoleCommand
    {
        private readonly IPolicyQuery _policyQuery;
        private readonly ConsoleProxy _console;
        private int _customerId;

        public GetCustomerPoliciesConsoleCommand(IPolicyQuery policyQuery, ConsoleProxy console)
            : base("GetPolicies", "Get the status of a policy")
        {
            _policyQuery = policyQuery;
            _console = console;

            HasRequiredOption<int>("CustomerId", "The customer id for the policies to retrieve", p => _customerId = p);
        }

        protected override void Execute()
        {
            try
            {
                var policyViews = _policyQuery.Read(_customerId);
                var data = new List<Tuple<string, string, decimal, int, decimal>>();

                policyViews.ForEach(policyView =>
                {
                    data.Add(new Tuple<string, string, decimal, int, decimal>(
                        policyView.PolicyNumber, 
                        policyView.CustomerId.ToString(), 
                        Enumerable.Sum<Premium>(policyView.Premiums, t => t.Total),
                        policyView.Funds.Count,
                        Enumerable.Sum<Fund>(policyView.Funds, t => t.TotalUnits)));
                });

                Console.WriteLine(data.ToStringTable(
                    new[] {"Policy Number", "Customer ID", "Premiums", "Funds", "Units"}, 
                    t => t.Item1, 
                    t => t.Item2, 
                    t => t.Item3.ToString("C"),
                    t => t.Item4,
                    t => t.Item5.ToString("0.000000")));
            }
            catch (Exception e)
            {
                _console.WriteLine($"There was an error retrieving policies for customer {_customerId}, {e.ToString()}");
            }
        }
    }
}