using ManyConsole;
using System;
using System.Collections.Generic;


[assembly: log4net.Config.XmlConfigurator()]

namespace DemoDB
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var commands = GetCommands();
            return ConsoleCommandDispatcher.DispatchCommand(commands, args, Console.Out);
        }

        public static IEnumerable<ConsoleCommand> GetCommands()
        {
            return ConsoleCommandDispatcher.FindCommandsInSameAssemblyAs(typeof(Program));
        }
    }
}
