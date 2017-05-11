using System;
using System.ServiceModel;
using CodeConcepts.EventEngine.Application.Interfaces;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.Contracts.Interfaces.Services;
using CodeConcepts.FrameworkExtensions.ObjectExtensions;
using Microsoft.Practices.Unity;

namespace CodeConcepts.EventEngine.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class EventEngineApiService : IEventEngineApiService
    {
        private readonly IUnityContainer _container;

        public EventEngineApiService(IUnityContainer container)
        {
            _container = container;
        }

        public void DispatchCommand(ICommand request)
        {
            var commandType = request.GetType();
            var baseCommandType = commandType.BaseType;

            var commandDispatcherType = typeof(ICommandDispatcher<>);
            var genericCommandDispatch = commandDispatcherType.MakeGenericType(baseCommandType);

            var dispatcher = _container.Resolve(genericCommandDispatch).AsDynamic();
            dispatcher.Apply(request.AsDynamic());

            Console.WriteLine("I got there, yay. Lying can you tell. FO!");
        }
    }
}