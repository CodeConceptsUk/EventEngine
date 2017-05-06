using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CliConsole.Interfaces;
using CliConsole.Interfaces.Factories;

namespace CliConsole
{
    public class ConsoleDispatcher : IConsoleDispatcher
    {
        private readonly ICommandInstanceFactory _instanceFactory;
        private readonly IConsoleParser _parser;
        private readonly IConsoleProxy _console;

        public ConsoleDispatcher(ICommandInstanceFactory instanceFactory, IConsoleParser parser, IConsoleProxy console)
        {
            _instanceFactory = instanceFactory;
            _parser = parser;
            _console = console;
        }

        public void DispatchCommand(IEnumerable<ICommand> commands, string[] args)
        {
            if (args == null || args.Length == 0)
                return;

            var matchingCommands = commands.Where(t => string.Equals(t.CommandName, args[0], StringComparison.InvariantCultureIgnoreCase));
            foreach (var command in matchingCommands)
            {
                ParseCommandAndExecute(args, _console, command);
            }
        }

        private void ParseCommandAndExecute(string[] args, IConsoleProxy console, ICommand command)
        {
            var instance = _instanceFactory.Create(command.GetType());
            
            if (!_parser.Parse(instance, args))
                return;

            WriteExecutionInformationToConsole(console, command, instance);
        }

        private static void WriteExecutionInformationToConsole(IConsoleProxy console, ICommand command, ICommand instance)
        {
            var timer = new Stopwatch();
            timer.Start();
            instance.Run();
            timer.Stop();
            console.WriteLine($"Executed {command.CommandName} in {timer.Elapsed.TotalSeconds:0.000000} seconds");
            console.WriteLine(string.Empty);
        }
    }
}