using System.Collections.Generic;
using CodeConcepts.CliConsole;
using CodeConcepts.CliConsole.Interfaces;
using CodeConcepts.EventEngine.ConsoleClient.Extensions;
using CodeConcepts.FrameworkExtensions.LinqExtensions;

namespace CodeConcepts.EventEngine.ConsoleClient.ConsoleCommands
{
    public class HelpConsoleCliCommand : InlineConsoleCliCommand
    {
        private readonly IEnumerable<ICliCommand> _commands;
        private readonly IConsoleProxy _console;

        public HelpConsoleCliCommand(IEnumerable<ICliCommand> commands, IConsoleProxy console)
            : base("Help", "Displays help")
        {
            _commands = commands;
            _console = console;
        }

        protected override void Execute()
        {
            _console.WriteLine(new string('-', 80));
            _console.WriteLine($"{"Command name".ToFixedWidth(30)}Description");
            _console.WriteLine(new string('-', 80));
            _commands.ForEach(command =>
            {
                _console.WriteLine($"{command.CommandName.ToFixedWidth(30)}{command.Description}");
            });
            _console.WriteLine(new string('-', 80));
        }
    }
}