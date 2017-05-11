﻿using System.Collections.Generic;
using System.Linq;
using CliConsole.Interfaces;
using Microsoft.Practices.Unity;

namespace Program.Extensions
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