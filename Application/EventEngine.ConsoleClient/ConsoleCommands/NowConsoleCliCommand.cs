using System;
using System.Globalization;
using CodeConcepts.CliConsole;
using CodeConcepts.CliConsole.Interfaces;

namespace CodeConcepts.EventEngine.ConsoleClient.ConsoleCommands
{
    public class NowConsoleCliCommand : InlineConsoleCliCommand
    {
        private readonly IConsoleProxy _console;

        public NowConsoleCliCommand(IConsoleProxy console)
            : base("Now", "Gets the current date and time")
        {
            _console = console;
        }

        protected override void Execute()
        {
            _console.WriteLine(DateTime.Now.ToString(CultureInfo.InvariantCulture));
        }
    }
}