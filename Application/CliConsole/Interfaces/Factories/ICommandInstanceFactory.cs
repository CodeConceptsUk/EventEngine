using System;

namespace CliConsole.Interfaces.Factories
{
    public interface ICommandInstanceFactory
    {
        ICommand Create(Type type);
    }
}