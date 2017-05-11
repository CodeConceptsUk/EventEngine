using Microsoft.Practices.Unity;
using Policy.Contracts.Services;
using Program.Factories;

namespace Program.Services
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