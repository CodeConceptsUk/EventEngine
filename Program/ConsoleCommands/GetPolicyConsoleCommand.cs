using System;
using System.Collections.Generic;
using System.Linq;
using CliConsole;
using FrameworkExtensions.LinqExtensions;
using Policy.Plugin.Isa.Policy.Operations.Commands;
using Policy.Plugin.Isa.Policy.Views.Queries;
using Program.Extensions;

namespace Program.ConsoleCommands
{
    public class GetPolicyConsoleCommand : InlineConsoleCommand
    {
        private readonly IPolicyQuery _policyQuery;
        private readonly ConsoleProxy _console;
        private string _policyNumber;

        public GetPolicyConsoleCommand(IPolicyQuery policyQuery, ConsoleProxy console)
            : base("GetPolicy", "Get the status of a policy")
        {
            _policyQuery = policyQuery;
            _console = console;

            HasRequiredOption<string>("PolicyNumber", "The policy number for the policy to retrieve", p => _policyNumber = p);
        }

        protected override void Execute()
        {
            try
            {
                var policyView = _policyQuery.Read(_policyNumber);

                var fixedSpace = "".ToFixedWidth(5);
                _console.WriteLine($"{"PolicyNumber:".ToFixedWidth(30)}{policyView.PolicyNumber}{fixedSpace}{"CustomerId:".ToFixedWidth(15)}{policyView.CustomerId}");
                _console.WriteLine($"{"Funds Used:".ToFixedWidth(30)}{policyView.Funds.Count}");

                var data = new List<Tuple<string, decimal, decimal>>();


                policyView.Funds.ForEach(fund =>
                {

                    data.Add(new Tuple<string, decimal, decimal>(
                        fund.FundId,
                        fund.TotalUnits,
                        fund.TotalShadowUnits));
                });

                _console.WriteLine(data.ToStringTable(
                    new[] { "FundId", "Total Units", "Shadow Units" },
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