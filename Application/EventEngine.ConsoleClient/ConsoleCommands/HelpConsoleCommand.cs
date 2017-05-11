using CliConsole;
using CliConsole.Interfaces;
using FrameworkExtensions.LinqExtensions;
using Microsoft.Practices.Unity;
using Program.Extensions;

namespace Program.ConsoleCommands
{
    public class HelpConsoleCommand : InlineConsoleCommand
    {
        private readonly IUnityContainer _container;
        private readonly IConsoleProxy _console;

        public HelpConsoleCommand(IUnityContainer container, IConsoleProxy console)
            : base("Help", "Displays help")
        {
            _container = container;
            _console = console;
        }

        protected override void Execute()
        {
            _console.WriteLine(new string('-', 80));
            _console.WriteLine($"{"Command name".ToFixedWidth(30)}Description");
            _console.WriteLine(new string('-', 80));
            var consoleCommands = _container.GetConsoleCommands();
            consoleCommands.ForEach(command =>
            {
                _console.WriteLine($"{command.CommandName.ToFixedWidth(30)}{command.Description}");
            });
            _console.WriteLine(new string('-', 80));
        }
    }
}