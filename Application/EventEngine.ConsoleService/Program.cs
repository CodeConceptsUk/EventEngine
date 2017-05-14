using System;
using System.ServiceProcess;
using CodeConcepts.EventEngine.ConsoleService.Console;
using CodeConcepts.EventEngine.ConsoleService.Service;

[assembly: log4net.Config.XmlConfigurator]
namespace CodeConcepts.EventEngine.ConsoleService
{
    internal class Program
    {
        private static void Main()
        {
            if (Environment.UserInteractive)
            {
                // Startup Console
                StartupConsole();
            }
            else
            {
                // Startup Service
                StartupService();
            }
        }

        private static void StartupService()
        {
            var servicesToRun = new ServiceBase[]
            {
                new EventEngineService()
            };
            ServiceBase.Run(servicesToRun);
        }

        private static void StartupConsole()
        {
            var consoleService = new EventEngineConsole();
            consoleService.Run();
        }
    }
}
