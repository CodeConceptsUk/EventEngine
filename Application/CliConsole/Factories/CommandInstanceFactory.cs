using System;
using CodeConcepts.CliConsole.Interfaces;
using CodeConcepts.CliConsole.Interfaces.Factories;

namespace CodeConcepts.CliConsole.Factories
{
    public class CommandInstanceFactory : ICommandInstanceFactory
    {
        public ICliCommand Create(Type type)
        {
            return Activator.CreateInstance(type) as ICliCommand;
        }
    }
}