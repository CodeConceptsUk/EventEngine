using Microsoft.Practices.Unity;
using Policy.Application.Interfaces;
using Policy.Application.Interfaces.Repositories;
using Policy.Plugin.Isa.Policy.Interfaces.DataAccess;
using Policy.Plugin.Isa.Policy.Interfaces.Domain;
using Policy.Plugin.Isa.Policy.Views.PolicyView;

namespace Policy.Plugin.Isa.Policy.DataAccess
{
    public class Plugin : IContainer
    {
        public void Setup(IUnityContainer container)
        {
            container.RegisterType<ISequencingRepository, SequencingRepository>();
            container.RegisterType<IUnitPricingRepository, UnitPricingRepository>();
            container.RegisterType<ISnapshotStore<PolicyView, IPolicyContext>, SinglePolicySnapshotStore>();
        }
    }
}