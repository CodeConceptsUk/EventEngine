using System;
using System.Diagnostics;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Policy.Application.Interfaces;
using Policy.Application.Interfaces.Repositories;
using Policy.Plugin.Isa.Policy.Commands;
using Policy.Plugin.Isa.Policy.Interfaces.Domain;
using Policy.Plugin.Isa.Policy.PropertyBags;
using Policy.Plugin.Isa.Policy.Queries;
using Policy.Plugin.Isa.Policy.Views;
using Program.Factories;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new ContainerFactory().Create();
            var bus = container.Resolve<ICommandBus>();
            var eventStore = container.Resolve<IEventStoreRepository<IPolicyContext>>();
            var eventPlayer = container.Resolve<IEventPlayer>();

            bus.Apply(new CreatePolicyCommand(14));
            bus.Apply(new CreatePolicyCommand(1234));
            bus.Apply(new CreatePolicyCommand(12332));
            bus.Apply(new CreatePolicyCommand(123));
            bus.Apply(new CreatePolicyCommand(14));

            var policyQuery = new PolicyQuery(eventStore, eventPlayer);

            var policyView = policyQuery.Read(12332);
            policyView.ForEach(SummarisePolicy);

            var premiumRandom = new Random(123456);
            var fundRandom = new Random(1236);
            for (var day = 0; day < 365; day++)
            {
                var date = DateTime.Now.AddDays(800 - day);
                bus.Apply(new AddPremiumCommand("3", date, new FundPremiumDetails($"fund{fundRandom.Next(1, 10)}", Math.Round((decimal)premiumRandom.NextDouble() * 100, 2))));
                bus.Apply(new UnitAllocationCommand("3", date));
            }

            //DateTime day;
            //day = DateTime.Now.AddDays(-7);

            //bus.Apply(new AddPremiumCommand("3", day, new FundPremiumDetails("fund1", 50.00m)));
            //bus.Apply(new AddPremiumCommand("3", day, new FundPremiumDetails("fund2", 23.32m)));
            //bus.Apply(new AddPremiumCommand("3", day, new FundPremiumDetails("fund3", 12.00m)));
            //bus.Apply(new UnitAllocationCommand("3", day));

            //policyView = policyQuery.Read(12332);
            //policyView.ForEach(SummarisePolicy);

            //day = DateTime.Now.AddDays(-6);
            //bus.Apply(new AddPremiumCommand("3", day, new FundPremiumDetails("fund2", 12.00m)));
            //bus.Apply(new UnitAllocationCommand("3", day));

            //policyView = policyQuery.Read(12332);
            //policyView.ForEach(SummarisePolicy);

            //day = DateTime.Now.AddDays(-3);
            //bus.Apply(new AddPremiumCommand("3", day, new FundPremiumDetails("fund3", 12.00m)));
            //bus.Apply(new AddPremiumCommand("3", day, new FundPremiumDetails("fund3", 12.00m)));
            //bus.Apply(new UnitAllocationCommand("3", day));

            var timer = new Stopwatch();
            timer.Start();
            policyView = policyQuery.Read("3");
            policyView.ForEach(SummarisePolicy);
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
                Console.WriteLine($"Fund: {fund.FundId}, premiums: {fund.UnallocatedPremiums}, units {fund.Units.ToString("0.00000")}");
            });
        }
    }
}
