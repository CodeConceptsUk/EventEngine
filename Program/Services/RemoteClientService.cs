using System;
using System.ServiceModel;
using FrameworkExtensions.ObjectExtensions;
using Microsoft.Practices.Unity;
using Policy.Application.Interfaces;
using Policy.Contracts.Services;

namespace Program.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class RemoteClientService : IRemoteClientService
    {
        private readonly IUnityContainer _container;

        public RemoteClientService(IUnityContainer container)
        {
            _container = container;
        }

        public void Dispatch(ICommand request)
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