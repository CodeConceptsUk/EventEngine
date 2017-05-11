using ManyConsole;
using System;
using System.Collections.Generic;
using log4net;


[assembly: log4net.Config.XmlConfigurator()]

namespace DemoDB
{
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                var commands = GetCommands();
                return ConsoleCommandDispatcher.DispatchCommand(commands, args, Console.Out);
            }
            catch (Exception e)
            {
                LogManager.GetLogger(nameof(Program)).Error(e);
            }
            return 1;
        }

        public static IEnumerable<ConsoleCommand> GetCommands()
        {
            return ConsoleCommandDispatcher.FindCommandsInSameAssemblyAs(typeof(Program));
        }
    }
}
