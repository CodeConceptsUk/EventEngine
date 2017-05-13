using CodeConcepts.EventEngine.Api.Contracts.Services;
using CodeConcepts.EventEngine.Application.Factories;
using CodeConcepts.EventEngine.Application.Hosting;
using CodeConcepts.EventEngine.Application.Interfaces.Factories;
using CodeConcepts.EventEngine.Shared.Runtime;
using CodeConcepts.FrameworkExtensions.Factories;
using CodeConcepts.FrameworkExtensions.Interfaces.Factories;
using Microsoft.Practices.Unity;

namespace CodeConcepts.EventEngine.Application
{
    public class ServiceContainerFactory : ContainerFactory
    {
        protected override void SetupSpecificRegistrations(IUnityContainer container)
        {
            RegisterByConvention(container);
            container.RegisterType<ILogFactory, LogFactory>();
            container.RegisterType<IStopwatchFactory, StopwatchFactory>();
            container.RegisterType<ICommandDispatcherFactory, CommandDispatcherFactory>();
            container.RegisterType<IQueryDispatcherFactory, QueryDispatcherFactory>();
            container.RegisterType<IEventPlayerFactory, EventPlayerFactory>();
            container.RegisterType<IServiceHosting, ServiceHosting>();
            container.RegisterType<IEventEngineApiService, EventEngineApiService>();
        }

        private static void RegisterByConvention(IUnityContainer container)
        {
            container.RegisterTypes(
                AllClasses.FromLoadedAssemblies(),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.ContainerControlled);
        }
    }
}