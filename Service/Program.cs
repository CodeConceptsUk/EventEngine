using System;
using Application.Commands;
using Application.Interfaces;
using Application.Interfaces.Domain;
using Application.Interfaces.Repositories;
using Application.PropertyBags;
using Application.Queries;
using Application.Views;
using Microsoft.Practices.ObjectBuilder2;
using Service.Factories;
using Microsoft.Practices.Unity;

namespace Service
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
