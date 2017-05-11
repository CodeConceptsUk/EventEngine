using CodeConcepts.CliConsole;
using CodeConcepts.CliConsole.Convertors;
using CodeConcepts.CliConsole.Interfaces;
using CodeConcepts.CliConsole.Interfaces.Convertors;
using CodeConcepts.CliConsole.Interfaces.Factories;
using CodeConcepts.EventEngine.ClientLibrary;
using Microsoft.Practices.Unity;

namespace CodeConcepts.EventEngine.ConsoleClient.Factories
{
    public class CliClientContainerFactory : ClientContainerFactory
    {
        protected override void SetupSpecificRegistrations(IUnityContainer container)
        {
            base.SetupSpecificRegistrations(container);
            
            container.RegisterType<IConsoleDispatcher, ConsoleDispatcher>();
            container.RegisterType<ICommandInstanceFactory, ContainerCommandInstanceFactory>();
            container.RegisterType<IConsoleParser, ConsoleParser>();
            container.RegisterType<IConsoleProxy, ConsoleProxy>();
            container.RegisterType<IValueConvertor, ValueConvertor>();

            RegisterNamedTypes<ICommand>(container);
        }
    }
}