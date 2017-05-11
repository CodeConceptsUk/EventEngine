using System.Linq;
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

        private static void IsaSpecificStuff(UnityContainer container)
        {
            //container.RegisterType<ISequencingRepository, SequencingSqlStore>();
            //container.RegisterType<IUnitPricingRepository, UnitPricingInMemoryStore>();
            //container.RegisterType<ISnapshotStore<PolicyView>, SinglePolicySnapshotMemoryStore>();
            //container.RegisterType<ISnapshotStore<PolicyTransactionView>, SinglePolicyTransactionSnapshotMemoryStore>();
            //container.RegisterType<IEventStoreRepository<IsaPolicyEvent>, IsaPolicyEventsInSqlStore>();
            //container.RegisterType<IIsaPolicyEventStoreRepository, IsaPolicyEventsInSqlStore>();
        }

        protected virtual void RegisterNamedTypes<TType>(IUnityContainer container)
        {
            var types = GetType().Assembly.GetTypes().Where(t => t.IsAbstract == false && typeof(TType).IsAssignableFrom(t)).ToList();
            types.ForEach(t =>
            {
                container.RegisterType(typeof(TType), t, t.FullName, new ContainerControlledLifetimeManager());
            });
        }
    }
}