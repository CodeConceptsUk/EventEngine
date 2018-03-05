using EventEngine.Interfaces.Repositories;
using StructureMap;

namespace EventEngine.ExampleApplication
{
    public class ExampleRegistry : Registry
    {
        public ExampleRegistry()
        {
            Scan(cfg =>
            {
                cfg.TheCallingAssembly();
            });
        }
    }

    public class ContainerFactory
    {
        public IContainer Create()
        {
            var container = new Container(cfg =>
                cfg.For<IEventStore>().Use<InMemoryEventStore>()
                
            );
            return container;
        }
    }
}