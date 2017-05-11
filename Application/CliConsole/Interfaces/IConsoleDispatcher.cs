using System.Collections.Generic;

namespace CodeConcepts.CliConsole.Interfaces
{
    public interface IConsoleDispatcher
    {
        void DispatchCommand(IEnumerable<ICommand> commands, string[] args);
    }
}