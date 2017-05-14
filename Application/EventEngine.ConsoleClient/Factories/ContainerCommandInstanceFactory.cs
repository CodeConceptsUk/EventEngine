using System;
using CodeConcepts.CliConsole.Interfaces;
using CodeConcepts.CliConsole.Interfaces.Factories;
using SimpleInjector;

namespace CodeConcepts.EventEngine.ConsoleClient.Factories
{
    public class ContainerCommandInstanceFactory : ICommandInstanceFactory
    {
        private readonly Container _container;

        public ContainerCommandInstanceFactory(Container container)
        {
            _container = container;
        }

        public ICliCommand Create(Type type)
        {
            return null;
           // return _container.Resolve(type, type.Name) as ICliCommand;
        }
    }
}