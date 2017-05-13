﻿using System;
using CodeConcepts.EventEngine.Services.Hosting;
using log4net;
using Microsoft.Practices.Unity;

[assembly: log4net.Config.XmlConfigurator]
namespace CodeConcepts.EventEngine.ConsoleService
{
    internal class Program
    {
        private static void Main()
        {
            LogManager.GetLogger(typeof(Program)).Info("Starting Console Service");
            var serviceContainerFactory = new ConsoleServiceContainerFactory();
            var container = serviceContainerFactory.Create();
            using (var service = container.Resolve<IServiceHosting>())
            {
                service.Start();
                Console.ReadLine();
            }
        }
    }
}
