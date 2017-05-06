using System;
using CliConsole.Interfaces;
using CliConsole.Interfaces.Factories;

namespace CliConsole.Factories
{
    public class CommandInstanceFactory : ICommandInstanceFactory
    {
        public ICommand Create(Type type)
        {
            return Activator.CreateInstance(type) as ICommand;
        }
    }
}