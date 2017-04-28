using Microsoft.Practices.Unity;
using Policy.Application;
using Policy.Application.Interfaces;
using Policy.Application.Interfaces.Repositories;
using Policy.Plugin.Isa.Policy.CommandHandlers;
using Policy.Plugin.Isa.Policy.DataAccess;
using Policy.Plugin.Isa.Policy.EventEvaluators;
using Policy.Plugin.Isa.Policy.Interfaces.DataAccess;
using Policy.Plugin.Isa.Policy.Interfaces.Domain;
using Policy.Plugin.Isa.Policy.Interfaces.Queries;
using Policy.Plugin.Isa.Policy.Queries;

namespace Program.Factories
{
    public class ContainerFactory
    {
        public IUnityContainer Create()
        {
            var container = new UnityContainer();
            RegisterHandlers(container);

            container.RegisterType<IEventPlayer, EventPlayer>();
            container.RegisterType<ICommandBus, CommandBus>();
            container.RegisterType<IEventStoreRepository<IPolicyContext>, PolicyContextEventStoreRepository>();
            container.RegisterType<ISequencingRepository, SequencingRepository>();

            return container;
        }

        private static void RegisterHandlers(IUnityContainer container)
        {
            // Commands 
            container.RegisterType<ICommandHandler, AddPremiumHandler>("AddPremiumHandler");
            container.RegisterType<ICommandHandler, CreatePolicyHandler>("CreatePolicyHandler");
            container.RegisterType<ICommandHandler, AddFundChargeHandler>("AddFundChargeHandler");
            // Events
            container.RegisterType<IEventEvaluator, AppliedFundChargeEventEvaluator>("AppliedFundChargeEventEvaluator");
            container.RegisterType<IEventEvaluator, PolicyCreatedEventEvaluator>("PolicyCreatedEventEvaluator");
            container.RegisterType<IEventEvaluator, AddPremiumEventEvaluator>("AddPremiumEventEvaluator");
            // Queries
            container.RegisterType<IPolicyEventContextIdQuery, PolicyEventContextIdQuery>();
        }
    }
}