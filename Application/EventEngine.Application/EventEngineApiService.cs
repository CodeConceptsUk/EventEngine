using System;
using System.ServiceModel;
using CodeConcepts.EventEngine.Api.Contracts;
using CodeConcepts.EventEngine.Api.Contracts.Services;
using CodeConcepts.EventEngine.Application.Interfaces.Factories;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using log4net;

namespace CodeConcepts.EventEngine.Application
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class EventEngineApiService : IEventEngineApiService
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ILog _log;

        public EventEngineApiService(ILogFactory logFactory, 
            ICommandDispatcher commandDispatcher,
            IQueryDispatcher queryDispatcher)
        {
            _log = logFactory.GetLogger(GetType());
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        public void DispatchCommand(ICommand request)
        {
            try
            {
                _log.Info($"Executing Api Command: {request.GetType()}");
                _commandDispatcher.Apply(request);
            }
            catch (Exception exception)
            {
                _log.Error($"Failed to execute {request?.GetType()}", exception);
                throw new FaultException(new FaultReason(exception.ToString()));
            }
        }

        public IView DispatchQuery(IQuery request)
        {
            try
            {
                _log.Info($"Executing Api Query: {request.GetType()}");
                return _queryDispatcher.Read(request);
            }
            catch (Exception exception)
            {
                _log.Error($"Failed to execute {request?.GetType()}", exception);
                throw new FaultException(new FaultReason(exception.ToString()));
            }
        }
    }
}