using System.Linq;
using System.Reflection;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace CodeConcepts.EventEngine.Shared.Runtime
{
    public abstract class ContainerFactory
    {
        public IUnityContainer Create()
        {
            var container = new UnityContainer();

            container.RegisterInstance(container);
            
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

        protected void RegisterAllInterfacesForNamedTypes<TType>(Assembly assembly, IUnityContainer container)
        {
            var types = assembly.GetTypes().Where(t => t.IsAbstract == false && typeof(TType).IsAssignableFrom(t)).ToList();
            
            types.ForEach(t =>
            {
                t.GetInterfaces().ForEach(i =>
                {
                    if (i != typeof(TType))
                    {
                        container.RegisterType(i, t, new ContainerControlledLifetimeManager());
                    }
                });
            });
        }
    }
}