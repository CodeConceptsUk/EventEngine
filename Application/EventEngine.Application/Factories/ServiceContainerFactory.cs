using CodeConcepts.EventEngine.Api.Contracts.Services;
using CodeConcepts.EventEngine.Application.Hosting;
using CodeConcepts.EventEngine.Application.Interfaces.Factories;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.Shared.Runtime;
using CodeConcepts.FrameworkExtensions.Factories;
using CodeConcepts.FrameworkExtensions.Interfaces.Factories;
using SimpleInjector;

namespace CodeConcepts.EventEngine.Application.Factories
{
    public class ServiceContainerFactory : ContainerFactory
    {
        protected override void SetupSpecificRegistrations(Container container)
        {
            container.Register<ILogFactory, LogFactory>();
            container.Register<IStopwatchFactory, StopwatchFactory>();
            container.Register<ICommandDispatcherFactory, CommandDispatcherFactory>();
            container.Register<IQueryDispatcherFactory, QueryDispatcherFactory>();
            container.Register<IEventPlayerFactory, EventPlayerFactory>();
            container.Register<IServiceHosting, ServiceHosting>();
            container.Register<IEventEngineApiService, EventEngineApiService>();

            container.RegisterConditional(typeof(IEventPlayer<>), typeof(EventPlayer<>), c=> !c.Handled);
        }
    }
}