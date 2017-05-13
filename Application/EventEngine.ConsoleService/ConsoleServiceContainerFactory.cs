using System.Reflection;
using CodeConcepts.EventEngine.Api.Contracts;
using CodeConcepts.EventEngine.Application;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.Contracts.Interfaces.Repositories;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.DataAccess;
using CodeConcepts.EventEngine.IsaPolicy.Operations.DataAccess.InMemory;
using CodeConcepts.EventEngine.IsaPolicy.Operations.DataAccess.Sql;
using Microsoft.Practices.Unity;

namespace CodeConcepts.EventEngine.ConsoleService
{
    public class ConsoleServiceContainerFactory : ServiceContainerFactory
    {
        //TODO - this is less than ideal - get plugin system
        protected override void SetupSpecificRegistrations(IUnityContainer container)
        {
            base.SetupSpecificRegistrations(container);
            container.RegisterType<ISequencingRepository, SequencingSqlStore>();
            container.RegisterType<IUnitPricingRepository, UnitPricingInMemoryStore>();
            container.RegisterType<IEventStoreRepository<IsaPolicyEvent>, IsaPolicyEventsInSqlStore>();
            container.RegisterType<IIsaPolicyEventStoreRepository, IsaPolicyEventsInSqlStore>();
            container.RegisterType<IEventPlayer<IsaPolicyEvent>, EventPlayer<IsaPolicyEvent>>();
            //TODO snapshots need to be application lifetime
            RegisterNamedTypes<ICommand>(typeof(IsaPolicyEvent).Assembly, container);
            RegisterNamedTypes<ICommandHandler>(Assembly.Load($"CodeConcepts.EventEngine.IsaPolicy.{nameof(IsaPolicy.Operations)}"), container);
            RegisterNamedTypes<ISnapshotStore>(Assembly.Load($"CodeConcepts.EventEngine.IsaPolicy.{nameof(IsaPolicy.Operations)}"), container);
            RegisterNamedTypes<IEventEvaluator>(Assembly.Load($"CodeConcepts.EventEngine.IsaPolicy.{nameof(IsaPolicy.Operations)}"), container);
            RegisterNamedTypes<IEventEvaluator>(Assembly.Load($"CodeConcepts.EventEngine.IsaPolicy.{nameof(IsaPolicy.Views)}"), container);
            RegisterNamedTypes<ISnapshotStore>(Assembly.Load($"CodeConcepts.EventEngine.IsaPolicy.{nameof(IsaPolicy.Views)}"), container);
            RegisterAllInterfacesForNamedTypes<IQuery>(Assembly.Load("CodeConcepts.EventEngine.IsaPolicy.Views"), container);
        }
    }
}