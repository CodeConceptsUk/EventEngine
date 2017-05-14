using System;
using CodeConcepts.EventEngine.Application.Hosting;
using log4net;
using Microsoft.Practices.Unity;

namespace CodeConcepts.EventEngine.ConsoleService.Console
{
    public class EventEngineConsole
    {
        private readonly ILog _log;

        public EventEngineConsole()
        {
            _log = LogManager.GetLogger(typeof(EventEngineConsole));
            _log.Info("Starting Console Service");
        }

        public void Run(params string[] args)
        {
            var serviceContainerFactory = new ConsoleServiceContainerFactory();
            var container = serviceContainerFactory.Create();
            using (var service = container.Resolve<IServiceHosting>())
            {
                service.Start();
                do
                {
                    System.Console.Write("Cli (Server)> ");
                } while (!string.Equals(GetConsoleReadLine(), "exit", StringComparison.InvariantCultureIgnoreCase));
            }
        }

        private static string GetConsoleReadLine()
        {
            return System.Console.ReadLine() ?? string.Empty;
        }
    }
}