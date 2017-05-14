using System;
using System.Collections.Generic;
using System.Linq;
using CodeConcepts.CliConsole;
using CodeConcepts.EventEngine.ClientLibrary.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Queries;
using CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Views.PolicyView;
using CodeConcepts.FrameworkExtensions.LinqExtensions;

namespace CodeConcepts.EventEngine.ConsoleClient.ConsoleCommands
{
    public class GetCustomerPoliciesConsoleCliCommand : InlineConsoleCliCommand
    {
        private readonly ICommandChannelClientFactory _commandChannelClientFactory;
        private readonly ConsoleProxy _console;
        private string _customerId;

        public GetCustomerPoliciesConsoleCliCommand(ICommandChannelClientFactory commandChannelClientFactory, ConsoleProxy console)
            : base("GetPolicies", "Get the status of a policy")
        {
            _commandChannelClientFactory = commandChannelClientFactory;
            _console = console;

            HasRequiredOption<string>("CustomerId", "The customer id for the policies to retrieve", p => _customerId = p);
        }

        protected override void Execute()
        {
            try
            {
                var client = _commandChannelClientFactory.Create();
                var view = client.DispatchQuery(new GetPoliciesForCustomerIdQuery(_customerId)) as PoliciesView;
                //var policyViews = _policyQuery.Read(_customerId);
                var data = new List<Tuple<string, string, decimal, int, decimal>>();

                view.Policies.ForEach(policyView =>
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