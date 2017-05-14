using System.Diagnostics;
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

            container.ResolveUnregisteredType += Container_ResolveUnregisteredType;

            return container;
        }

        private static void Container_ResolveUnregisteredType(object sender, UnregisteredTypeEventArgs e)
        {
            if(Debugger.IsAttached)
                Debugger.Break();
        }

        protected abstract void SetupSpecificRegistrations(Container container);
        
        protected virtual void RegisterAllImplementorsOfType<TType>(Container container)
        {
            container.RegisterCollection(typeof(TType), GetType().Assembly);
        }

        protected void RegisterAllImplementorsOfType<TType>(Container container, Assembly assembly)
        {
            container.RegisterCollection(typeof(TType), assembly);
        }

        protected void RegisterAllInterfacesForNamedTypes<TType>(Assembly assembly, Container container)
        {
            RegisterAllImplementorsOfType<TType>(container, assembly);
        }
    }
}