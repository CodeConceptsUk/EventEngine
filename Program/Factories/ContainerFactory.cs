using Microsoft.Practices.Unity;
using Policy.Application;
using Policy.Application.Interfaces;
using Policy.Application.Interfaces.Repositories;
using Policy.Plugin.Isa.Policy.DataAccess;
using Policy.Plugin.Isa.Policy.DataAccess.InMemory;

namespace Program.Factories
{
    public class ContainerFactory
    {
        public IUnityContainer Create()
        {
            var container = new UnityContainer();
            container.RegisterType<IEventPlayer, EventPlayer>();
            container.RegisterType<ICommandBus, CommandBus>();
            container.RegisterType<IEventStoreRepository, PolicyContextEventStoreInMemoryStore>();

            SetupPolicyPlugin(container);
            SetupPolicyPluginDataAccess(container);

            return container;
        }

        private static void SetupPolicyPlugin(IUnityContainer container)
        {
            var containerSetup = new Policy.Plugin.Isa.Policy.Plugin();
            containerSetup.Setup(container);
        }

        private static void SetupPolicyPluginDataAccess(IUnityContainer container)
        {
            var containerSetup = new Plugin();
            containerSetup.Setup(container);
        }
    }
}