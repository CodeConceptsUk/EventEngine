using System.Collections.Generic;

namespace CliConsole.Interfaces
{
    public interface IConsoleDispatcher
    {
        void DispatchCommand(IEnumerable<ICommand> commands, string[] args);
    }
}