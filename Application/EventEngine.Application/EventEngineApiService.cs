using System;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using CodeConcepts.EventEngine.Api.Contracts;
using CodeConcepts.EventEngine.Api.Contracts.Services;
using CodeConcepts.EventEngine.Application.Interfaces.Factories;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;
using CodeConcepts.FrameworkExtensions.ObjectExtensions;
using log4net;

namespace CodeConcepts.EventEngine.Application
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class EventEngineApiService : IEventEngineApiService
    {
        private readonly ICommandDispatcherFactory _commandDispatcherFactory;
        private readonly IQueryDispatcherFactory _queryDispatcherFactory;
        private readonly ILog _log;

        public EventEngineApiService(ILogFactory logFactory, 
            ICommandDispatcherFactory commandDispatcherFactory,
            IQueryDispatcherFactory queryDispatcherFactory)
        {
            _log = logFactory.GetLogger(GetType());
            _commandDispatcherFactory = commandDispatcherFactory;
            _queryDispatcherFactory = queryDispatcherFactory;
        }

        public void DispatchCommand(ICommand request)
        {
            try
            {
                var commandType = request.GetType();
                var commandBaseType = commandType.BaseType;

                //TODO cheating: (remove the reference to IsaPolicy namespace too when fixing this - using plugin to fix)
                var eventBaseType = typeof(IsaPolicyEvent);

                var commandDispatcherCreationMethodInfo = typeof(ICommandDispatcherFactory).GetMethods(BindingFlags.Public | BindingFlags.Instance).Single(m => m.Name == "Create");
                var commandDispatcherInstanceCreationMethodInfo = commandDispatcherCreationMethodInfo.MakeGenericMethod(commandBaseType, eventBaseType);

                _log.Info($"Executing Api Command: {request.GetType()}");

                var dispatcher = commandDispatcherInstanceCreationMethodInfo.Invoke(_commandDispatcherFactory, new object[] {}).AsDynamic();
                dispatcher.Apply(request.AsDynamic());
            }
            catch (Exception exception)
            {
                _log.Error($"Failed to execute {request?.GetType()}", exception);
                throw new FaultException(new FaultReason(exception.ToString()));
            }
        }

        public IView DispatchQuery(IQuery request)
        {
            var queryDispatcherCreationMethodInfo = typeof(IQueryDispatcherFactory).GetMethods(BindingFlags.Public | BindingFlags.Instance).Single(m => m.Name == "Create");
            var queryDispatcherInstanceCreationMethodInfo = queryDispatcherCreationMethodInfo.MakeGenericMethod(request.GetType());

            var dispatcher = queryDispatcherInstanceCreationMethodInfo.Invoke(_queryDispatcherFactory, new object[] { }).AsDynamic();
            return dispatcher.Read(request.AsDynamic());
        }
    }
}