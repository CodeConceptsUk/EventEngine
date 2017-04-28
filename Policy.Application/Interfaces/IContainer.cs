using Microsoft.Practices.Unity;

namespace Policy.Application.Interfaces
{
    public interface IContainer
    {
        void Setup(IUnityContainer container);
    }
}