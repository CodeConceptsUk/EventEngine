using System.Collections.Generic;

namespace CliConsole.Interfaces
{
    public interface ICommand
    {
        string CommandName { get; }
        string Description { get; }
        IEnumerable<CommandArgument> Arguments { get; }

        void Run();
    }
}