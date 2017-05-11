using System;
using Microsoft.Practices.Unity;
using Program.Services;

[assembly: log4net.Config.XmlConfigurator]
namespace Program
{
    internal class Program
    {
        private static void Main()
        {
            var serviceContainerFactory = new ServiceContainerFactory();
            var container = serviceContainerFactory.Create();
            using (var service = container.Resolve<IServiceHosting>())
            {
                service.Start();
                Console.ReadLine();
            }
        }
    }
}
