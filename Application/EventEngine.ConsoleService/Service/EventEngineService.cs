using System;
using System.ServiceProcess;
using CodeConcepts.EventEngine.Application.Hosting;
using log4net;
using SimpleInjector;

namespace CodeConcepts.EventEngine.ConsoleService.Service
{
    internal partial class EventEngineService : ServiceBase
    {
        private readonly ILog _log;
        private readonly IServiceHosting _service;
        private Container _container;

        public EventEngineService()
        {
            var serviceContainerFactory = new ConsoleServiceContainerFactory();
            _container = serviceContainerFactory.Create();
            _service = _container.GetInstance<IServiceHosting>();
            InitializeComponent();

            _log = LogManager.GetLogger(typeof(EventEngineService));
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                _log.Info($"Starting Windows Service {GetType()}");
                _service.Start();
            }
            catch (Exception e)
            {
                _log.Error(e);
                throw;
            }
        }

        protected override void OnStop()
        {
            _log.Info($"Stopping Windows Service {GetType()}");
            _service.Dispose();
        }
    }
}
