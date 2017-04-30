using System;
using System.Diagnostics;
using System.Threading;
using FrameworkExtensions.LinqExtensions;
using log4net;
using Microsoft.Practices.Unity;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Commands;
using Policy.Plugin.Isa.Policy.Interfaces.Queries;
using Policy.Plugin.Isa.Policy.PropertyBags;
using Policy.Plugin.Isa.Policy.Views.PolicyView;
using Program.Factories;

[assembly: log4net.Config.XmlConfigurator()]
namespace Program
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            LogManager.GetLogger(nameof(Program)).Debug("Logger Working");
            var container = new ContainerFactory().Create();
            var dispatcher = container.Resolve<ICommandDispatcher<IsaPolicyCommand>>();
            var policyQuery = container.Resolve<IPolicyQuery>();

            var createCommand = new CreatePolicyCommand(1);
            var addPremiumCommand = new AddPremiumCommand("1", DateTime.Now, new FundPremiumDetails($"F1", 1000m));
            Thread.Sleep(300);
            var unitAllocationCommand = new UnitAllocationCommand("1", DateTime.Now);

            dispatcher.Apply(createCommand);
            dispatcher.Apply(addPremiumCommand);
            dispatcher.Apply(unitAllocationCommand);
            
            dispatcher.Apply(new CreatePolicyCommand(14));
            dispatcher.Apply(new CreatePolicyCommand(1234));
            dispatcher.Apply(new CreatePolicyCommand(12332));
            dispatcher.Apply(new CreatePolicyCommand(123));
            dispatcher.Apply(new CreatePolicyCommand(14));

            var policy = policyQuery.Read("3");

            var premiumRandom = new Random(123456);
            var fundRandom = new Random(1236);
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var day = -10;
            while ( day < 0)
            {
                var date = DateTime.Now.AddDays(day);
                dispatcher.Apply(new AddPremiumCommand("3", date, new FundPremiumDetails($"fund{fundRandom.Next(1, 10)}", ((decimal)premiumRandom.NextDouble())*10m)));
                dispatcher.Apply(new AddPremiumCommand("3", date, new FundPremiumDetails($"fund{fundRandom.Next(1, 10)}", ((decimal)premiumRandom.NextDouble())*10m)));
                dispatcher.Apply(new AddPremiumCommand("3", date, new FundPremiumDetails($"fund{fundRandom.Next(1, 10)}", ((decimal)premiumRandom.NextDouble())*10m)));
                dispatcher.Apply(new UnitAllocationCommand("3", date));
                day++;
                if (day % 1000 != 0)
                    continue;

                var time = stopwatch.Elapsed;
                stopwatch.Reset();
                Console.WriteLine($"At {day} last 1000 took {time}");
                var policyViewss = policyQuery.Read("3");
                SummarisePolicy(policyViewss);
                stopwatch.Start();
            }

            var timer = new Stopwatch();
            timer.Start();
            var policyView = policyQuery.Read("3");
            SummarisePolicy(policyView);
            timer.Stop();

            Console.WriteLine($"{timer.Elapsed}");
            Console.ReadLine();
        }

        private static void SummarisePolicy(PolicyView policy)
        {
            Console.WriteLine("-------------------------------------------------------------------------------------");
            Console.WriteLine($"Policy: {policy.PolicyNumber}, Customer: {policy.CustomerId}");

            policy.Funds?.ForEach(fund =>
            {
                Console.WriteLine($"Fund: {fund.FundId}, premiums: {fund.UnallocatedPremiums:0.00}, units {fund.Units:0.00000}");
            });
        }
    }
}
