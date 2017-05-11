using CliConsole;
using CliConsole.Convertors;
using CliConsole.Interfaces;
using CliConsole.Interfaces.Convertors;
using CliConsole.Interfaces.Factories;
using Microsoft.Practices.Unity;
using Program.Services;

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