
using System;
using System.Collections.Generic;
using System.Linq;
using CodeConcepts.CliConsole;
using CodeConcepts.EventEngine.ClientLibrary.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Queries;
using CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Views.PolicyView.Domain;
using CodeConcepts.FrameworkExtensions.LinqExtensions;

namespace Program.ConsoleCommands
{
    public class GetPolicyConsoleCommand : InlineConsoleCommand
    {
        private readonly ICommandChannelClientFactory _commandChannelClientFactory;
        private readonly ConsoleProxy _console;
        private string _policyNumber;

        public GetPolicyConsoleCommand(ICommandChannelClientFactory commandChannelClientFactory, ConsoleProxy console)
            : base("GetPolicy", "Get the status of a policy")
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
                var policyView = client.DispatchQuery(new GetPolicyForPolicyNumberQuery(_policyNumber)) as PolicyView;

                DisplayBasicPolicyViewData(policyView);
                DisplayFundData(policyView);
            }
            catch (Exception e)
            {
                _console.WriteLine($"There was an error retrieving policy {_policyNumber}, {e.Message}");
            }
        }

        private void DisplayFundData(PolicyView policyView)
        {
            var data = new List<Tuple<string, decimal, decimal>>();
            policyView.Funds.ForEach(fund =>
            {
                data.Add(new Tuple<string, decimal, decimal>(
                    fund.FundId,
                    fund.TotalUnits,
                    fund.TotalShadowUnits));
            });

            _console.WriteLine(data.ToStringTable(
                new[] {"FundId", "Total Units", "Shadow Units"},
                t => t.Item1,
                t => t.Item2,
                t => t.Item3));
        }

        private void DisplayBasicPolicyViewData(PolicyView policyView)
        {
            var policyData = new Dictionary<string, string>
            {
                {"PolicyNumber", policyView.PolicyNumber},
                {"CustomerId", policyView.CustomerId.ToString()},
                {"Funds", policyView.Funds.Count.ToString()},
                {"Premiums", policyView.Premiums.Sum(p => p.Total).ToString("C")},
                {"Units", policyView.Funds.Sum(p => p.TotalUnits).ToString("0.000000")},
                {"Shadow Units", policyView.Funds.Sum(p => p.TotalShadowUnits).ToString("0.000000")},
            };

            _console.WriteLine(policyData.ToStringTable());
        }
    }
}