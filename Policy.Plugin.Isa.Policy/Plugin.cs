using Microsoft.Practices.Unity;
using Policy.Application;
using Policy.Application.Factories;
using Policy.Application.Interfaces;
using Policy.Application.Interfaces.Factories;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Interfaces.DataAccess;
using IContainer = Policy.Application.Interfaces.IContainer;

namespace Policy.Plugin.Isa.Policy
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
            container.RegisterType<IEventPlayer<IsaPolicyEvent>, EventPlayer<IsaPolicyEvent>>();
            container.RegisterType<ILogFactory, LogFactory>();
        }
    }
}
