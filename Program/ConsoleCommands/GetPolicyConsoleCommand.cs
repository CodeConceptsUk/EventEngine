using System;
using System.Collections.Generic;
using System.Linq;
using CliConsole;
using FrameworkExtensions.LinqExtensions;
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
            }
            catch (Exception e)
            {
                _console.WriteLine($"There was an error retrieving policy {_policyNumber}, {e.Message}");
            }
        }
    }

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
                var data = new List<Tuple<string, string, string, decimal>>();

                policyViews.ForEach(policyView =>
                {
                    data.Add(new Tuple<string, string, string, decimal>(
                        policyView.PolicyNumber, 
                        policyView.CustomerId.ToString(), 
                        policyView.Funds.Count.ToString(), 
                        policyView.Premiums.Sum(t => t.Total)));
                });

                Console.WriteLine(data.ToStringTable(new[] {"Policy Number", "Customer ID", "Premiums", "Funds"}, t => t.Item1, t => t.Item2, t => t.Item3, t => t.Item4.ToString("C")));
            }
            catch (Exception e)
            {
                _console.WriteLine($"There was an error retrieving policies for customer {_customerId}, {e.Message}");
            }
        }
    }
}