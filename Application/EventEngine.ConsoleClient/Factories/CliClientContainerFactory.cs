using System.Reflection;
using CodeConcepts.CliConsole;
using CodeConcepts.CliConsole.Convertors;
using CodeConcepts.CliConsole.Interfaces;
using CodeConcepts.CliConsole.Interfaces.Convertors;
using CodeConcepts.CliConsole.Interfaces.Factories;
using CodeConcepts.EventEngine.Api.Contracts;
using CodeConcepts.EventEngine.ClientLibrary;
using Microsoft.Practices.Unity;
using ICommand = CodeConcepts.CliConsole.Interfaces.ICommand;

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
            RegisterNamedTypes<IView>(Assembly.Load($"CodeConcepts.EventEngine.IsaPolicy.{nameof(IsaPolicy.Contracts)}"), container);
            RegisterNamedTypes<IView>(Assembly.Load($"CodeConcepts.EventEngine.IsaPolicy.Views.{nameof(IsaPolicy.Views.Contracts)}"), container);
            
            RegisterNamedTypes<ICommand>(container);
        }
    }
}