using CodeConcepts.EventEngine.ClientLibrary.Interfaces;
using CodeConcepts.EventEngine.Shared.Runtime;
using Microsoft.Practices.Unity;

namespace CodeConcepts.EventEngine.ClientLibrary
{
    public class ClientContainerFactory : ContainerFactory
    {
        protected override void SetupSpecificRegistrations(IUnityContainer container)
        {
            container.RegisterType<ICommandChannelClientFactory, CommandChannelClientFactory>();
        }
    }
}