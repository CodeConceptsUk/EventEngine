using System;
using System.Linq;
using System.Text;
using CliConsole.Interfaces;
using Program.Extensions;
using Microsoft.Practices.Unity;
using Program.Factories;

namespace ConsoleClient
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

                var args = command.ParseArguments().ToArray();
                dispatcher.DispatchCommand(consoleCommands, args);
                Console.Write($"Cli> ");
            }
        }
    }
}
