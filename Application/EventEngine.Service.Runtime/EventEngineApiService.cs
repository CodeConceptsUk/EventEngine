using System.Linq;
using System.Reflection;
using System.ServiceModel;
using CodeConcepts.EventEngine.Application.Interfaces;
using CodeConcepts.EventEngine.Application.Interfaces.Factories;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.Contracts.Interfaces.Services;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;
using CodeConcepts.FrameworkExtensions.ObjectExtensions;
using log4net;
using Microsoft.Practices.Unity;

namespace CodeConcepts.EventEngine.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class EventEngineApiService : IEventEngineApiService
    {
        private readonly ICommandDispatcherFactory _commandDispatcherFactory;
        private readonly ILog _log;

        public EventEngineApiService(ILogFactory logFactory, ICommandDispatcherFactory commandDispatcherFactory)
        {
            _log = logFactory.GetLogger(GetType());
            _commandDispatcherFactory = commandDispatcherFactory;
        }

        public void DispatchCommand(ICommand request)
        {
            var commandType = request.GetType();
            var commandBaseType = commandType.BaseType;

            //TODO cheating: (remove the reference to IsaPolicy namespace too when fixing this)
            var eventBaseType = typeof(IsaPolicyEvent);

            var commandDispatcherCreationMethodInfo = typeof(ICommandDispatcherFactory).GetMethods(BindingFlags.Public|BindingFlags.Instance).Single(m => m.Name == "Create");
            var commandDispatcherInstanceCreationMethodInfo = commandDispatcherCreationMethodInfo.MakeGenericMethod(commandBaseType, eventBaseType);

            _log.Info($"Executing Api Command: {request.GetType()}");

            var dispatcher = commandDispatcherInstanceCreationMethodInfo.Invoke(_commandDispatcherFactory, new object[]{}).AsDynamic();
            dispatcher.Apply(request.AsDynamic());
        }
    }
}