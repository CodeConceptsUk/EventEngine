using System;
using System.Linq;
using CodeConcepts.CliConsole.Interfaces;
using CodeConcepts.EventEngine.ConsoleClient.Extensions;
using CodeConcepts.EventEngine.ConsoleClient.Factories;
using SimpleInjector;

namespace CodeConcepts.EventEngine.ConsoleClient
{
    public class Program
    {
        public static void Main()
        {
            var container = new CliClientContainerFactory().Create();
            var consoleCommands = container.GetConsoleCommands().ToList();
            var dispatcher = container.Resolve<IConsoleDispatcher>();
            var console = container.Resolve<IConsoleProxy>();

            WelcomeMessage(console);
            console.Write($"Cli> ");

            string command;
            while ((command = console.ReadLine()) != "exit")
            {

                try
                {
                    var args = command.ParseArguments().ToArray();
                    dispatcher.DispatchCommand(consoleCommands, args);
                }
                catch (Exception exception)
                {
                    console.WriteLine(string.Empty);
                    console.WriteLine(exception.ToString());
                    console.WriteLine(string.Empty);
                }
                console.Write($"Cli> ");
            }
        }

        private static void WelcomeMessage(IConsoleProxy console)
        {
            console.WriteLine($"Cli integration for EventEngine.");
            console.WriteLine($"Client version {typeof(Program).Assembly.GetName().Version}.\nType 'Help' for commands list.");
            console.WriteLine();
        }
    }
}
