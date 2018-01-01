using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EventEngine.Application.Interfaces.Commands;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Factories;
using EventEngine.Application.Interfaces.Repositories;
using Unity;
using Unity.Lifetime;
using Unity.RegistrationByConvention;

namespace EventEngine.ExampleApplication
{
    public class ContainerFactory
    {
        public IUnityContainer Create()
        {
            var container = new UnityContainer();
            container.RegisterType<IEventStore, InMemoryEventStore>(new TransientLifetimeManager());
            RegisterByConvention(container);
            return container;
        }

        private static void RegisterByConvention(IUnityContainer container)
        {
            container.RegisterTypes(
                AllClasses.FromLoadedAssemblies(),
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.ContainerControlled);
        }

        public static IEnumerable<IEventEvaluator> GetAllEventEvaluators(IUnityContainer container)
        {
            var types = typeof(ContainerFactory).Assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IEventEvaluator)));
            foreach (var type in types)
            {
                yield return (IEventEvaluator)Activator.CreateInstance(type);
            }
        }

        public static IEnumerable<ICommandHandler> GetAllCommandHandlers(IUnityContainer container, IEventFactory eventFactory)
        {
            var types = typeof(ContainerFactory).Assembly.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(ICommandHandler)));
            foreach (var type in types)
            {
                yield return (ICommandHandler)Activator.CreateInstance(type, eventFactory);
            }
        }
    }
}