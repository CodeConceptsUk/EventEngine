using System.Linq;
using CliConsole;
using CliConsole.Convertors;
using CliConsole.Interfaces;
using CliConsole.Interfaces.Convertors;
using CliConsole.Interfaces.Factories;
using FrameworkExtensions.LinqExtensions;
using Microsoft.Practices.Unity;
using Policy.Application.Interfaces;
using Policy.Contracts.Services;
using Program.Factories;
using Program.Services;
using ICommand = CliConsole.Interfaces.ICommand;

namespace Program
{
    public class DependencyInjectionConfiguration : IContainer
    {
        public void Setup(IUnityContainer container)
        {
            container.RegisterType<IConsoleProxy, ConsoleProxy>();
            container.RegisterType<IConsoleDispatcher, ConsoleDispatcher>();
            container.RegisterType<IConsoleParser, ConsoleParser>();
            container.RegisterType<IValueConvertor, ValueConvertor>();
            container.RegisterType<ICommandInstanceFactory, ContainerCommandInstanceFactory>();
            container.RegisterType<IServiceHosting, ServiceHosting>();
            container.RegisterType<IRemoteClientService, RemoteClientService>();
            container.RegisterType<ICommandChannelClientFactory, CommandChannelClientFactory>();
            RegisterConsoleCommands(container);
        }

        private void RegisterConsoleCommands(IUnityContainer container)
        {
            var consoleCommands = GetType()
                .Assembly
                .GetTypes()
                .Where(t => t.IsPublic && !t.IsAbstract)
                .Where(t => typeof(ICommand).IsAssignableFrom(t));
            consoleCommands.ForEach(consoleCommand =>
            {
                container.RegisterType(typeof(ICommand), consoleCommand, consoleCommand.Name);
            });
        }
    }
}