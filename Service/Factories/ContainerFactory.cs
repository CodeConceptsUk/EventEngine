using Application;
using Application.CommandHandlers;
using Application.EventEvaluators;
using Application.Interfaces;
using Application.Interfaces.Domain;
using Application.Interfaces.Repositories;
using DataAccess;
using Microsoft.Practices.Unity;

namespace Service.Factories
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
        }
    }
}