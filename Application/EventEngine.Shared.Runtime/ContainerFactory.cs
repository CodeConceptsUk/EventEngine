using System.Linq;
using System.Reflection;
using CodeConcepts.EventEngine.Application.Factories;
using CodeConcepts.EventEngine.Application.Interfaces;
using CodeConcepts.EventEngine.Application.Interfaces.Factories;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.FrameworkExtensions.Factories;
using CodeConcepts.FrameworkExtensions.Interfaces.Factories;
using Microsoft.Practices.Unity;

namespace CodeConcepts.EventEngine.Shared.Runtime
{
    public abstract class ContainerFactory
    {
        public IUnityContainer Create()
        {
            var container = new UnityContainer();

            container.RegisterInstance(container);
            container.RegisterType<ILogFactory, LogFactory>();
            container.RegisterType<IStopwatchFactory, StopwatchFactory>();
            container.RegisterType<ICommandDispatcherFactory, CommandDispatcherFactory>();
            container.RegisterType<IEventPlayerFactory, EventPlayerFactory>();
            
            RegisterNamedTypes<ICommandHandler>(container);
            RegisterNamedTypes<ICommand>(container);
            RegisterNamedTypes<IEventEvaluator>(container);
            
            SetupSpecificRegistrations(container);
            
            return container;
        }

        protected abstract void SetupSpecificRegistrations(IUnityContainer container);
        
        protected virtual void RegisterNamedTypes<TType>(IUnityContainer container)
        {
            RegisterNamedTypes<TType>(GetType().Assembly, container);
        }

        protected void RegisterNamedTypes<TType>(Assembly assembly, IUnityContainer container)
        {
            var types = assembly.GetTypes().Where(t => t.IsAbstract == false && typeof(TType).IsAssignableFrom(t)).ToList();
            types.ForEach(t =>
            {
                container.RegisterType(typeof(TType), t, t.FullName, new ContainerControlledLifetimeManager());
            });
        }
    }
}