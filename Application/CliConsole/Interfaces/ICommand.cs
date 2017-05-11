using System.Collections.Generic;

namespace CodeConcepts.CliConsole.Interfaces
{
    public interface ICommand
    {
        string CommandName { get; }
        string Description { get; }
        IEnumerable<CommandArgument> Arguments { get; }

        void Run();
    }
}