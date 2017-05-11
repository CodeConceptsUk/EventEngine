using System;
using System.Linq;
using System.Text;
using CliConsole;
using CliConsole.Interfaces;
using Program.Extensions;
using Program.Services;
using Microsoft.Practices.Unity;

namespace ConsoleClient
{
    public class Program
    {
        public static void Main()
        {
            var container = new ClientContainerFactory().Create();

            container.RegisterType<IConsoleDispatcher, ConsoleDispatcher>();

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
