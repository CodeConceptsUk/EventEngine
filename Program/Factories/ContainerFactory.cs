using Microsoft.Practices.Unity;
using Policy.Application.Interfaces;

namespace Program.Factories
{
    public class ContainerFactory
    {
        public IUnityContainer Create()
        {
            var container = new UnityContainer();

            AddPlugin<Policy.Plugin.Isa.Policy.Plugin>(container);
            AddPlugin<Policy.Plugin.Isa.Policy.DataAccess.Plugin>(container);
            AddPlugin<Policy.Plugin.Isa.Policy.Operations.Plugin>(container);
            AddPlugin<Policy.Plugin.Isa.Policy.Views.Plugin>(container);

            return container;
        }

        private static void AddPlugin<TPlugin>(IUnityContainer container)
            where TPlugin : class, IContainer, new()
        {
            var plugin = new TPlugin();
            plugin.Setup(container);
        }
    }
}