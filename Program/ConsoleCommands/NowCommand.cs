using System;
using System.Globalization;
using CliConsole;
using CliConsole.Interfaces;

namespace Program.ConsoleCommands
{
    public class NowCommand : InlineConsoleCommand
    {
        private readonly IConsoleProxy _console;

        public NowCommand(IConsoleProxy console)
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