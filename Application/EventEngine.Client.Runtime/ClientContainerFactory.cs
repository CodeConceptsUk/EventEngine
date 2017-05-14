using CodeConcepts.EventEngine.ClientLibrary.Interfaces;
using CodeConcepts.EventEngine.Shared.Runtime;
using SimpleInjector;

namespace CodeConcepts.EventEngine.ClientLibrary
{
    public class ClientContainerFactory : ContainerFactory
    {
        protected override void SetupSpecificRegistrations(Container container)
        {
            container.RegisterType<ICommandChannelClientFactory, CommandChannelClientFactory>();
        }
    }
}