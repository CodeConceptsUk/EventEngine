using System.Collections.Generic;
using System.Linq;
using CodeConcepts.CliConsole.Interfaces;
using Microsoft.Practices.Unity;

namespace CodeConcepts.EventEngine.ConsoleClient.Extensions
{
    internal static class ContainerExtensions
    {
        internal static IEnumerable<ICommand> GetConsoleCommands(this IUnityContainer container)
        {
            return container
                .Registrations
                .Where(t => t.RegisteredType == typeof(ICommand))
                .Select(x => (ICommand)container.Resolve(x.RegisteredType, x.Name));
        }
    }
}