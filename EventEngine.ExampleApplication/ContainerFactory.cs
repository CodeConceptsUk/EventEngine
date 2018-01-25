using System;
using System.Collections.Generic;
using System.Linq;
using EventEngine.Interfaces.Commands;
using EventEngine.Interfaces.Events;
using EventEngine.Interfaces.Factories;
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
        
        public static IEnumerable<IEventEvaluator> GetAllEventEvaluators(IContainer container)
        {
            var types = typeof(ContainerFactory).Assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IEventEvaluator)));
            foreach (var type in types)
            {
                yield return (IEventEvaluator)Activator.CreateInstance(type);
            }
        }

        public static IEnumerable<ICommandHandler> GetAllCommandHandlers(IContainer container, IEventFactory eventFactory)
        {
            var types = typeof(ContainerFactory).Assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(ICommandHandler)));
            foreach (var type in types)
            {
                yield return (ICommandHandler)Activator.CreateInstance(type, eventFactory);
            }
        }
    }
}