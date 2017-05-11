using Microsoft.Practices.Unity;
using Program.Factories;

namespace Program.Services
{
    public class ClientContainerFactory : ContainerFactory
    {
        protected override void SetupSpecificRegistrations(IUnityContainer container)
        {
            container.RegisterType<ICommandChannelClientFactory, CommandChannelClientFactory>();
        }
    }
}