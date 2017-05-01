using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Xml.Schema;
using FrameworkExtensions.LinqExtensions;
using log4net;
using Microsoft.Practices.Unity;
using Policy.Application.Interfaces;
using Policy.Application.Interfaces.Repositories;
using Policy.Plugin.Isa.Policy.Operations.BaseTypes;
using Policy.Plugin.Isa.Policy.Operations.Commands;
using Policy.Plugin.Isa.Policy.Operations.PropertyBags;
using Policy.Plugin.Isa.Policy.Views.Queries;
using Policy.Plugin.Isa.Policy.Views.Views.PolicyView.Domain;
using Program.Factories;

[assembly: log4net.Config.XmlConfigurator()]

namespace Program
{
    internal class Program
    {

        //TODO: things we should do:
        // 1. split PolicyView into several views - because it has too much detailed information ?
        // 2. sql store for events
        // 3. no sql stores

        private static void Main()
        {
            var log = LogManager.GetLogger(nameof(Program));
            log.Debug("Logger Working");
            try
            {
                var container = new ContainerFactory().Create();
                var dispatcher = container.Resolve<ICommandDispatcher<IsaPolicyCommand>>();
                var policyQuery = container.Resolve<IPolicyQuery>();

                var createCommand = new CreatePolicyCommand(1);
                var addPremiumCommand = new AddPremiumCommand("1", Guid.NewGuid().ToString(), DateTime.Now,
                    CreateRandomPremiumDetails());


                Thread.Sleep(300);
                var unitAllocationCommand = new UnitAllocationCommand("1", DateTime.Now);

                dispatcher.Apply(createCommand);
                dispatcher.Apply(addPremiumCommand);
                dispatcher.Apply(unitAllocationCommand);

                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var day = -1000;
                var displayEvery = 100;
                while (day < 0)
                {
                    var date = DateTime.Now.AddDays(day);
                    if (day % 7 == 0) // add a new premium every 7 days
                    {
                        dispatcher.Apply(new AddPremiumCommand("1", Guid.NewGuid().ToString(), date,
                            CreateRandomPremiumDetails()));
                    }
                    dispatcher.Apply(new UnitAllocationCommand("1", date)); // alloate units daily
                    dispatcher.Apply(new AddPolicyFundChargesCommand("1", date)); // make charges daily
                    day++;
                    if (day % displayEvery != 0)
                        continue;

                    var time = stopwatch.Elapsed;
                    stopwatch.Reset();
                    Console.WriteLine($"At {day} last {displayEvery} took {time}");
                    var policyViewss = policyQuery.Read("1");
                    SummarisePolicy(policyViewss);
                    stopwatch.Start();
                }

                var snapshotStore = container.Resolve <ISnapshotStore<PolicyView>>();
                snapshotStore.ClearAllSnapshots(); // reset them all so we must calculate from scratch!!!

                var timer = new Stopwatch();
                timer.Start();
                var policyView = policyQuery.Read("1");
                SummarisePolicy(policyView, true);
                timer.Stop();
                Console.WriteLine($"{timer.Elapsed}");
            }
            catch (Exception e)
            {
                log.Fatal(e);
            }

            Console.ReadLine();
        }

        private static readonly Random Rng = new Random(123456);

        private static List<FundPremiumDetail> CreateRandomPremiumDetails()
        {
            return new List<FundPremiumDetail>
            {
                new FundPremiumDetail(Guid.NewGuid(), $"F{Rng.Next(1, 5)}", Rng.Next(10, 201)),
                new FundPremiumDetail(Guid.NewGuid(), $"F{Rng.Next(1, 5)}", Rng.Next(300, 500))
            };
        }

        private static void SanityCheck(PolicyView policyView)
        {
            policyView.Funds.ForEach(f =>
            {
                _callIndex = 0;
                AssertEquality(f.Allocations.Sum(a => a.Units), f.TotalUnits, $"{f.FundId} Unit Total Mismatch");
                AssertEquality(f.Allocations.Sum(a => a.ShadowUnits), f.TotalShadowUnits, $"{f.FundId} Shadow Total Mismatch");
                AssertEquality(0-f.Allocations.SelectMany(a => a.Charges).Sum(c => c.Units), f.TotalShadowUnits - f.TotalUnits, $"{f.FundId} Charge Total Vs Shadow Difference Mismatch");
            });
        }

        private static int _callIndex = 0;

        private static void AssertEquality(decimal val1, decimal val2, string message)
        {
            _callIndex++;
            if (val1 != val2)
            {
                throw new InsaneException(_callIndex, $"{message} {val1} != {val2}");
            }
        }

        private static void SummarisePolicy(PolicyView policy, bool detailed = false)
        {
            var sane = false;
            try
            {
                SanityCheck(policy);
                sane = true;
            }
            catch (InsaneException insaneException)
            {
                Console.WriteLine("Oh dear! it seems the policy is insane!");
                Console.WriteLine(insaneException);
            }

            if (policy == null)
            {
                Console.WriteLine("NULL");
                return;
            }
            if (Debugger.IsAttached)
            {
                Debugger.Break();
                return;
            }
            if (detailed)
            {
                DetailedSummary(policy);
            }
            else
            {
                BriefSummary(policy);
            }
            if (sane)
            {
                Console.WriteLine($"Hooray - This policy is sane!");
            }
        }

        private static void BriefSummary(PolicyView policy)
        {
            Console.WriteLine("-------------------------------------------------------------------------------------");
            Console.WriteLine($"Policy: {policy.PolicyNumber}");
            Console.WriteLine($"Customer: {policy.CustomerId}");
            Console.WriteLine($"Premiums: {policy.Premiums?.Count}");
            Console.WriteLine($"Premium Value: {policy.Premiums?.Sum(t => t.Total)}");
            foreach (var fund in policy.Funds)
            {
                Console.WriteLine($"\tFund {fund.FundId} Units: {fund.TotalUnits}");
                Console.WriteLine($"\tFund {fund.FundId} Shadow Units: {fund.TotalShadowUnits}");
            }

        }

        private static void DetailedSummary(PolicyView policy)
        {
            Console.WriteLine("-------------------------------------------------------------------------------------");
            Console.WriteLine(
                $"Policy: {policy?.PolicyNumber}, Customer: {policy?.CustomerId}, {policy?.Premiums?.Count} totalling {policy?.Premiums?.Sum(t => t.Total)}");
            policy?.Premiums?.ForEach(p =>
            {
                var allocated = p.IsAllocated ? "is" : "is not";
                Console.WriteLine($"\tPremium: {p.PremiumId} {allocated} and was {p.Total:0.00}");
                p.Partitions.ForEach(a => { Console.WriteLine($"\t\tFundId: {a.FundId}, Amount: {a.Amount:0.00} "); });
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

    internal class InsaneException : Exception
    {
        public InsaneException(int callIndex, string message) : base($"Insanity on call {callIndex} - {message}")
        {
        }
    }
}
