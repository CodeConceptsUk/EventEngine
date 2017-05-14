using System.Collections.Generic;
using System.Reflection;
using CodeConcepts.CliConsole;
using CodeConcepts.CliConsole.Convertors;
using CodeConcepts.CliConsole.Interfaces;
using CodeConcepts.CliConsole.Interfaces.Convertors;
using CodeConcepts.CliConsole.Interfaces.Factories;
using CodeConcepts.EventEngine.Api.Contracts;
using CodeConcepts.EventEngine.ClientLibrary;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using SimpleInjector;

namespace CodeConcepts.EventEngine.ConsoleClient.Factories
{
    public class CliClientContainerFactory : ClientContainerFactory
    {
        protected override void SetupSpecificRegistrations(Container container)
        {
            var assemblies = new List<Assembly>
            {
                Assembly.Load($"CodeConcepts.EventEngine.IsaPolicy.{nameof(IsaPolicy.Contracts)}"),
                Assembly.Load($"CodeConcepts.EventEngine.IsaPolicy.Views.{nameof(IsaPolicy.Views.Contracts)}"),
                GetType().Assembly
            };

            base.SetupSpecificRegistrations(container);
            
            container.Register<IConsoleDispatcher, ConsoleDispatcher>();
            container.Register<ICommandInstanceFactory, ContainerCommandInstanceFactory>();
            container.Register<IConsoleParser, ConsoleParser>();
            container.Register<IConsoleProxy, ConsoleProxy>();
            container.Register<IValueConvertor, ValueConvertor>();
            container.Register<ICliLoop, CliLoop>();
            container.RegisterCollection<IView>(assemblies);
            container.RegisterCollection<ICliCommand>(assemblies);
        }
    }
}