using CodeConcepts.EventEngine.Application.Interfaces.Repositories;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;
using CodeConcepts.EventEngine.IsaPolicy.DataAccess.InMemory;
using CodeConcepts.EventEngine.IsaPolicy.DataAccess.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.DataAccess.Sql;
using CodeConcepts.EventEngine.IsaPolicy.Views.DataAccess.InMemory;
using CodeConcepts.EventEngine.IsaPolicy.Views.Views.PolicyTransactionsView.Domain;
using CodeConcepts.EventEngine.IsaPolicy.Views.Views.PolicyView.Domain;
using CodeConcepts.EventEngine.Services;
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
            container.RegisterType<ISnapshotStore<PolicyView>, SinglePolicySnapshotMemoryStore>();
            container.RegisterType<ISnapshotStore<PolicyTransactionView>, SinglePolicyTransactionSnapshotMemoryStore>();
            container.RegisterType<IEventStoreRepository<IsaPolicyEvent>, IsaPolicyEventsInSqlStore>();
            container.RegisterType<IIsaPolicyEventStoreRepository, IsaPolicyEventsInSqlStore>();

            RegisterNamedTypes<ICommand>(typeof(IsaPolicyEvent).Assembly, container);
        }
    }
}