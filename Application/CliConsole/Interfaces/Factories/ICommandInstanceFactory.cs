using System;

namespace CodeConcepts.CliConsole.Interfaces.Factories
{
    public interface ICommandInstanceFactory
    {
        ICommand Create(Type type);
    }
}