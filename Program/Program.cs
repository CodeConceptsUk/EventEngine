using System;
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

            bus.Apply(new AddPremiumCommand("3", new FundPremiumDetails("fund1", 50.00m)));
            bus.Apply(new AddPremiumCommand("3", new FundPremiumDetails("fund1", 23.32m)));
            bus.Apply(new AddPremiumCommand("3", new FundPremiumDetails("fund1", 12.00m)));

            bus.Apply(new AddPremiumCommand("3", new FundPremiumDetails("fund2", 12.00m)));

            bus.Apply(new AddPremiumCommand("3", new FundPremiumDetails("fund3", 12.00m)));
            bus.Apply(new AddPremiumCommand("3", new FundPremiumDetails("fund3", 12.00m)));

            policyView = policyQuery.Read(12332);
            policyView.ForEach(SummarisePolicy);

            Console.ReadLine();
        }

        private static void SummarisePolicy(PolicyView policy)
        {
            Console.WriteLine("-------------------------------------------------------------------------------------");
            Console.WriteLine($"Policy: {policy.PolicyNumber}, Customer: {policy.CustomerId}");

            policy.Funds?.ForEach(fund =>
            {
                Console.WriteLine($"Fund: {fund.FundId}, premiums: {fund.UnallocatedPremiums}");
            });
        }
    }
}
