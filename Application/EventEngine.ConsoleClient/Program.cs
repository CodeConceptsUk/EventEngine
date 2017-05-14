using System;
using System.Collections.Generic;
using System.Linq;
using CodeConcepts.CliConsole.Interfaces;
using CodeConcepts.EventEngine.ConsoleClient.Extensions;
using CodeConcepts.EventEngine.ConsoleClient.Factories;

namespace CodeConcepts.EventEngine.ConsoleClient
{
    public class Program
    {
        public static void Main()
        {
            var container = new CliClientContainerFactory().Create();
            var cliLoop = container.GetInstance<ICliLoop>();
            cliLoop.Run(WelcomeMessage);
        }

        private static string WelcomeMessage => $"Cli integration for EventEngine.{Environment.NewLine}" +
                                                $"Client version {typeof(Program).Assembly.GetName().Version}.{Environment.NewLine}" +
                                                "Type 'Help' for commands list.";
    }

    //TODO move these into CliLibrary

    public interface ICliLoop
    {
        void Run(string initialMessage);
    }

    public class CliLoop : ICliLoop
    {
        private readonly IEnumerable<ICliCommand> _commands;
        private readonly IConsoleDispatcher _consoleDispatcher;
        private readonly IConsoleProxy _consoleProxy;

        public CliLoop(IEnumerable<ICliCommand> commands, IConsoleDispatcher consoleDispatcher, IConsoleProxy consoleProxy)
        {
            _commands = commands;
            _consoleDispatcher = consoleDispatcher;
            _consoleProxy = consoleProxy;
        }

        public void Run(string initialMessage)
        {
            _consoleProxy.WriteLine(initialMessage);
            _consoleProxy.WriteLine();

            string command;
            _consoleProxy.Write($"Cli> ");
            while ((command = _consoleProxy.ReadLine()) != "exit")
            {

                try
                {
                    var args = command.ParseArguments().ToArray();
                    _consoleDispatcher.DispatchCommand(_commands, args);
                }
                catch (Exception exception)
                {
                    _consoleProxy.WriteLine();
                    _consoleProxy.WriteLine(exception);
                    _consoleProxy.WriteLine();
                }
                _consoleProxy.Write($"Cli> ");
            }
        }
    }
}
