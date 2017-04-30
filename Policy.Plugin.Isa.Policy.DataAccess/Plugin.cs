using Microsoft.Practices.Unity;
using Policy.Application.Interfaces;
using Policy.Application.Interfaces.Repositories;
using Policy.Plugin.Isa.Policy.DataAccess.InMemory;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Interfaces.DataAccess;
using Policy.Plugin.Isa.Policy.Views.PolicyView;
using Policy.Plugin.Isa.Policy.Views.PolicyView.Domain;

namespace Policy.Plugin.Isa.Policy.DataAccess
{
    public class Plugin : IContainer
    {
        public void Setup(IUnityContainer container)
        {
            container.RegisterType<ISequencingRepository, SequencingInMemoryStore>();
            container.RegisterType<IUnitPricingRepository, UnitPricingInMemoryStore>();
            container.RegisterType<ISnapshotStore<PolicyView>, SinglePolicySnapshotMemoryStore>();
            container.RegisterType<IEventStoreRepository<IsaPolicyEvent>, PolicyContextEventStoreInMemoryStore>();
        }
    }
}