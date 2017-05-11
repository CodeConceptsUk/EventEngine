using System;
using CliConsole.Interfaces;
using CliConsole.Interfaces.Factories;
using Microsoft.Practices.Unity;

namespace Program.Factories
{
    public class ContainerCommandInstanceFactory : ICommandInstanceFactory
    {
        private readonly IUnityContainer _container;

        public ContainerCommandInstanceFactory(IUnityContainer container)
        {
            _container = container;
        }

        public ICommand Create(Type type)
        {
            return _container.Resolve(type, type.Name) as ICommand;
        }
    }
}