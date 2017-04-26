using System;
using Application.Commands;
using Application.Interfaces;
using Application.Interfaces.Domain;
using Application.Interfaces.Repositories;
using Application.Queries;
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

            policyView.ForEach(policy =>
            {
                Console.WriteLine($"Policy: {policy.PolicyNumber}, Customer: {policy.CustomerId}");
            });

            policyView = policyQuery.Read(14);

            policyView.ForEach(policy =>
            {
                Console.WriteLine($"Policy: {policy.PolicyNumber}, Customer: {policy.CustomerId}");
            });

            Console.ReadLine();
        }
    }
}
