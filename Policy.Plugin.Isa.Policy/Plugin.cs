using Microsoft.Practices.Unity;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.CommandHandlers;
using Policy.Plugin.Isa.Policy.EventEvaluators;
using Policy.Plugin.Isa.Policy.Interfaces.Queries;
using Policy.Plugin.Isa.Policy.Queries;

namespace Policy.Plugin.Isa.Policy
{
    public class Plugin : IContainer
    {
        public void Setup(IUnityContainer container)
        {
            SetupCommands(container);
            SetupEvents(container);
            SetupQueries(container);
        }

        private static void SetupQueries(IUnityContainer container)
        {
            // Queries
            container.RegisterType<IPolicyEventContextIdQuery, PolicyEventContextIdQuery>();
            container.RegisterType<IPolicyQuery, PolicyQuery>();
        }

        private static void SetupEvents(IUnityContainer container)
        {
            // Events
            container.RegisterType<IEventEvaluator, AppliedFundChargeEventEvaluator>("AppliedFundChargeEventEvaluator");
            container.RegisterType<IEventEvaluator, PolicyCreatedEventEvaluator>("PolicyCreatedEventEvaluator");
            container.RegisterType<IEventEvaluator, AddPremiumEventEvaluator>("AddPremiumEventEvaluator");
            container.RegisterType<IEventEvaluator, UnitsAllocatedEventEvaluator>("UnitsAllocatedEventEvaluator");
        }

        private static void SetupCommands(IUnityContainer container)
        {
            // Commands 
            container.RegisterType<ICommandHandler, AddPremiumHandler>("AddPremiumHandler");
            container.RegisterType<ICommandHandler, CreatePolicyHandler>("CreatePolicyHandler");
            container.RegisterType<ICommandHandler, AddFundChargeHandler>("AddFundChargeHandler");
            container.RegisterType<ICommandHandler, UnitAllocationHandler>("UnitAllocationHandler");
        }
    }
}
