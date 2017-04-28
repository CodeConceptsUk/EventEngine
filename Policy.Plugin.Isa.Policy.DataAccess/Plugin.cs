using Microsoft.Practices.Unity;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Interfaces.DataAccess;

namespace Policy.Plugin.Isa.Policy.DataAccess
{
    public class Plugin : IContainer
    {
        public void Setup(IUnityContainer container)
        {
            container.RegisterType<ISequencingRepository, SequencingRepository>();
            container.RegisterType<IUnitPricingRepository, UnitPricingRepository>();
        }
    }
}