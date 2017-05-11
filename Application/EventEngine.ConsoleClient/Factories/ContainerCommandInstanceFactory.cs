using System;
using CodeConcepts.CliConsole.Interfaces;
using CodeConcepts.CliConsole.Interfaces.Factories;
using Microsoft.Practices.Unity;

namespace CodeConcepts.EventEngine.ConsoleClient.Factories
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