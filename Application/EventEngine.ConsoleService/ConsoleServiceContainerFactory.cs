using System.Reflection;
using CodeConcepts.EventEngine.Api.Contracts;
using CodeConcepts.EventEngine.Application;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.Contracts.Interfaces.Repositories;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PolicyFundUnitBalance;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.PremiumsStatus;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreViews.UnallocatedReceivedPremiums;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueryHandlers;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.DataAccess;
using CodeConcepts.EventEngine.IsaPolicy.Operations.CoreQueryHandlers;
using CodeConcepts.EventEngine.IsaPolicy.Operations.DataAccess.InMemory;
using CodeConcepts.EventEngine.IsaPolicy.Operations.DataAccess.InMemory.CoreViewSnapshots;
using CodeConcepts.EventEngine.IsaPolicy.Operations.DataAccess.Sql;
using CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Views.PolicyTransactionsView;
using CodeConcepts.EventEngine.IsaPolicy.Views.Contracts.Views.PolicyView;
using CodeConcepts.EventEngine.IsaPolicy.Views.DataAccess.InMemory;
using SimpleInjector;

namespace CodeConcepts.EventEngine.ConsoleService
{
    public class ConsoleServiceContainerFactory : ServiceContainerFactory
    {
        //TODO - this is less than ideal - get plugin system
        protected override void SetupSpecificRegistrations(Container container)
        {
            base.SetupSpecificRegistrations(container);
            container.Register<ISequencingRepository, SequencingSqlStore>();
            container.Register<IUnitPricingRepository, UnitPricingInMemoryStore>();
            container.Register<IEventPlayer<IsaPolicyEvent>, EventPlayer<IsaPolicyEvent>>();
            
            container.Register<IEventStoreRepository<IsaPolicyEvent>, IsaPolicyEventsInSqlStore>();
            container.Register<IIsaPolicyEventStoreRepository, IsaPolicyEventsInSqlStore>();
            container.Register<ISnapshotStore<PolicyView>, SinglePolicySnapshotMemoryStore>();
            container.Register<ISnapshotStore<PolicyFundUnitBalanceView>, PolicyFundBalanceSnapshotMemoryStore>();
            container.Register<ISnapshotStore<PremiumsStatusView>, PremiumsStatusSnapshotMemoryStore>();
            container.Register<ISnapshotStore<UnallocatedReceivedPremiumsView>, UnallocatedReceivedPremiumsSnapshotMemoryStore>();
            container.RegisterType<ISnapshotStore<PolicyTransactionView>, PolicyTransactionSnapshotMemoryStore>();
            //TODO snapshots need to be application lifetime

            RegisterNamedTypes<ICommand>(typeof(IsaPolicyEvent).Assembly, container);

            RegisterNamedTypes<ICommandHandler>(Assembly.Load($"CodeConcepts.EventEngine.IsaPolicy.{nameof(IsaPolicy.Operations)}"), container);

            RegisterNamedTypes<IQuery>(Assembly.Load($"CodeConcepts.EventEngine.IsaPolicy.{nameof(IsaPolicy.Contracts)}"), container);
            RegisterNamedTypes<IQuery>(Assembly.Load($"CodeConcepts.EventEngine.IsaPolicy.Views.{nameof(IsaPolicy.Views.Contracts)}"), container);

            RegisterNamedTypes<IQueryHandler>(Assembly.Load($"CodeConcepts.EventEngine.IsaPolicy.{nameof(IsaPolicy.Contracts)}"), container);
            RegisterNamedTypes<IQueryHandler>(Assembly.Load($"CodeConcepts.EventEngine.IsaPolicy.{nameof(IsaPolicy.Operations)}"), container);
            RegisterNamedTypes<IQueryHandler>(Assembly.Load($"CodeConcepts.EventEngine.IsaPolicy.{nameof(IsaPolicy.Views)}"), container);

            RegisterAllInterfacesForNamedTypes<IQuery>(Assembly.Load("CodeConcepts.EventEngine.IsaPolicy.Views.Contracts"), container);
            RegisterAllInterfacesForNamedTypes<IQueryHandler>(Assembly.Load("CodeConcepts.EventEngine.IsaPolicy.Views"), container);

            RegisterNamedTypes<ISnapshotStore>(Assembly.Load($"CodeConcepts.EventEngine.IsaPolicy.{nameof(IsaPolicy.Operations)}"), container);
            RegisterNamedTypes<ISnapshotStore>(Assembly.Load($"CodeConcepts.EventEngine.IsaPolicy.{nameof(IsaPolicy.Views)}"), container);

            RegisterNamedTypes<IEventEvaluator>(Assembly.Load($"CodeConcepts.EventEngine.IsaPolicy.{nameof(IsaPolicy.Operations)}"), container);
            RegisterNamedTypes<IEventEvaluator>(Assembly.Load($"CodeConcepts.EventEngine.IsaPolicy.{nameof(IsaPolicy.Views)}"), container);

            container.RegisterType<IGetEventContextIdForPolicyNumberQueryHandler, GetEventContextIdForPolicyNumberQueryHandler>();
            

        }
    }
}