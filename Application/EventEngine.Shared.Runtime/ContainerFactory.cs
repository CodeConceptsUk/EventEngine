using System.Reflection;
using SimpleInjector;

namespace CodeConcepts.EventEngine.Shared.Runtime
{
    public abstract class ContainerFactory
    {
        public Container Create()
        {
            var container = new Container();
            
            
            SetupSpecificRegistrations(container);
            
            return container;
        }

        protected abstract void SetupSpecificRegistrations(Container container);
        
        protected virtual void RegisterNamedTypes<TType>(Container container)
        {
            container.Register(typeof(TType), new []{GetType().Assembly} );
        }

        protected void RegisterNamedTypes<TType>(Assembly assembly, Container container)
        {
            container.Register(typeof(TType), new[] { assembly });
        }

        protected void RegisterAllInterfacesForNamedTypes<TType>(Assembly assembly, Container container)
        {
            RegisterNamedTypes<TType>(assembly, container);
        }
    }
}