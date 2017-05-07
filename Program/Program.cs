using System;
using System.Linq;
using System.Text;
using CliConsole.Interfaces;
using Microsoft.Practices.Unity;
using Program.Extensions;
using Program.Factories;

[assembly: log4net.Config.XmlConfigurator()]

namespace Program
{
    internal class Program
    {

        //TODO: things we should do:
        // 1. split PolicyView into several views - because it has too much detailed information ?
        // 2. no sql stores

        private static void Main()
        {
            string command;
            var container = new ContainerFactory().Create();
            var consoleCommands = container.GetConsoleCommands().ToList();
            var dispatcher = container.Resolve<IConsoleDispatcher>();

            Console.OutputEncoding = Encoding.UTF8;
            Console.Write($"Cli> ");
            while ((command = Console.ReadLine()) != "exit")
            {

                var args = command.ParseArguments().ToArray();
                dispatcher.DispatchCommand(consoleCommands, args);
                Console.Write($"Cli> ");
            }
        }
    }
}
