using System;

namespace CodeConcepts.CliConsole.Interfaces.Factories
{
    public interface ICommandInstanceFactory
    {
        ICliCommand Create(Type type);
    }
}