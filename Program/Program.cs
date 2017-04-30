using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using FrameworkExtensions.LinqExtensions;
using log4net;
using Microsoft.Practices.Unity;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Commands;
using Policy.Plugin.Isa.Policy.Commands.Commands;
using Policy.Plugin.Isa.Policy.Commands.PropertyBags;
using Policy.Plugin.Isa.Policy.Interfaces.Queries;
using Policy.Plugin.Isa.Policy.Views.PolicyView;
using Policy.Plugin.Isa.Policy.Views.PolicyView.Domain;
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
            var addPremiumCommand = new AddPremiumCommand("1", Guid.NewGuid().ToString(), DateTime.Now, CreateRandomPremiumDetails());


            Thread.Sleep(300);
            var unitAllocationCommand = new UnitAllocationCommand("1", DateTime.Now);

            dispatcher.Apply(createCommand);
            dispatcher.Apply(addPremiumCommand);
            dispatcher.Apply(unitAllocationCommand);

            var premiumRandom = new Random(123456);
            var fundRandom = new Random(1236);
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var day = -1000;
            while (day < 0)
            {
                var date = DateTime.Now.AddDays(day);
                if (day % 7 == 0) // add a new premium every 7 days
                {
                    dispatcher.Apply(new AddPremiumCommand("1", Guid.NewGuid().ToString(), date,
                        CreateRandomPremiumDetails()));
                }
                dispatcher.Apply(new UnitAllocationCommand("1", date)); // alloate units daily
                dispatcher.Apply(new AddPolicyFundChargesCommand("1")); // make charges daily
                day++;
                if (day % 100 != 0)
                    continue;

                var time = stopwatch.Elapsed;
                stopwatch.Reset();
                Console.WriteLine($"At {day} last 1000 took {time}");
                var policyViewss = policyQuery.Read("1");
                SummarisePolicy(policyViewss);
                stopwatch.Start();
            }

            var timer = new Stopwatch();
            timer.Start();
            var policyView = policyQuery.Read("1");
            SummarisePolicy(policyView);
            timer.Stop();

            Console.WriteLine($"{timer.Elapsed}");
            Console.ReadLine();
        }

        private static List<FundPremiumDetail> CreateRandomPremiumDetails()
        {
            return new List<FundPremiumDetail>
            {
                new FundPremiumDetail("F1", 100),
                new FundPremiumDetail("F2", 50)
            };
        }

        private static void SummarisePolicy(PolicyView policy)
        {
            if (policy == null)
            {
                Console.WriteLine("NULL");
                return;
            }
            Console.WriteLine("-------------------------------------------------------------------------------------");
            Console.WriteLine($"Policy: {policy?.PolicyNumber}, Customer: {policy?.CustomerId}, {policy?.Premiums?.Count} totalling {policy?.Premiums?.Sum(t => t.Total)}");
            policy?.Premiums?.ForEach(p =>
            {
                var allocated = p.IsAllocated ? "is" : "is not";
                Console.WriteLine($"\tPremium: {p.PremiumId} {allocated} and was {p.Total:0.00}");
                p.Partitions.ForEach(a =>
                {
                    Console.WriteLine($"\t\tFundId: {a.FundId}, Amount: {a.Amount:0.00} ");
                });
            });
            policy?.Funds?.ForEach(f =>
            {
                Console.WriteLine($"\tAllocations of {f.FundId}:");
                f.Allocations.ForEach(a =>
                {
                    Console.WriteLine($"\t\t{a.PremiumPartition.Amount:0.00} was allocated to {a.Units:0.0000} units");
                });
            });
        }
    }
}
