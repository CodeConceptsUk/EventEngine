using System;
using System.Linq;
using System.Text;
using CodeConcepts.CliConsole.Interfaces;
using CodeConcepts.EventEngine.ConsoleClient.Extensions;
using CodeConcepts.EventEngine.ConsoleClient.Factories;
using Microsoft.Practices.Unity;

namespace CodeConcepts.EventEngine.ConsoleClient
{
    public class Program
    {
        public static void Main()
        {
            var container = new CliClientContainerFactory().Create();
            
            var consoleCommands = container.GetConsoleCommands().ToList();
            var dispatcher = container.Resolve<IConsoleDispatcher>();
            
            Console.OutputEncoding = Encoding.UTF8;
            Console.Write($"Cli> ");

            string command;
            while ((command = Console.ReadLine()) != "exit")
            {

                try
                {
                    var args = command.ParseArguments().ToArray();
                    dispatcher.DispatchCommand(consoleCommands, args);
                }
                catch (Exception exception)
                {
                    Console.WriteLine();
                    Console.WriteLine(exception);
                    Console.WriteLine();
                }
                Console.Write($"Cli> ");
            }
        }
    }
}
