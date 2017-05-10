using System.Linq;
using Microsoft.Practices.Unity;
using Policy.Application;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Operations.BaseTypes;
using IContainer = Policy.Application.Interfaces.IContainer;

namespace Policy.Plugin.Isa.Policy.Operations
{
    public class Plugin : IContainer
    {
        public void Setup(IUnityContainer container)
        {
            //SetupQueries(container);
            container.RegisterTypes(
                AllClasses.FromLoadedAssemblies(),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.ContainerControlled);
            RegisterNamedTypes<ICommandHandler>(container);
            RegisterNamedTypes<ICommand>(container);
            container.RegisterType<ICommandDispatcher<IsaPolicyCommand>, CommandDispatcher<IsaPolicyCommand, IsaPolicyEvent>>();

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
