using System.Linq;
using Microsoft.Practices.Unity;
using Policy.Application.Interfaces;
using IContainer = Policy.Application.Interfaces.IContainer;

namespace Policy.Plugin.Isa.Policy.Views
{
    public class Plugin : IContainer
    {
        public void Setup(IUnityContainer container)
        {
            //SetupQueries(container);
            container.RegisterTypes(
                AllClasses.FromLoadedAssemblies(),
                WithMappings.FromMatchingInterface,
                WithName.TypeName,
                WithLifetime.ContainerControlled);
            RegisterNamedTypes<IEventEvaluator>(container);

        }

        private void RegisterNamedTypes<TType>(IUnityContainer container)
        {
            var types = GetType().Assembly.GetTypes().Where(t => t.IsAbstract == false && typeof(TType).IsAssignableFrom(t)).ToList();
            types.ForEach(t =>
            {
                container.RegisterType(typeof(TType), t, t.FullName, new ContainerControlledLifetimeManager());
            });
        }
    }
}
