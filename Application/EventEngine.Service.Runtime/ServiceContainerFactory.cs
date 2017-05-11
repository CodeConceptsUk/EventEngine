using CodeConcepts.EventEngine.Contracts.Interfaces.Services;
using CodeConcepts.EventEngine.Services.Hosting;
using CodeConcepts.EventEngine.Shared.Runtime;
using Microsoft.Practices.Unity;

namespace CodeConcepts.EventEngine.Services
{
    public class ServiceContainerFactory : ContainerFactory
    {
        protected override void SetupSpecificRegistrations(IUnityContainer container)
        {
            container.RegisterType<IServiceHosting, ServiceHosting>();
            container.RegisterType<IEventEngineApiService, EventEngineApiService>();
        }
    }
}