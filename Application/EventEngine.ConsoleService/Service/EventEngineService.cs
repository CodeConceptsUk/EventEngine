using System;
using System.ServiceProcess;
using CodeConcepts.EventEngine.Application.Hosting;
using log4net;
using Microsoft.Practices.Unity;

namespace CodeConcepts.EventEngine.ConsoleService.Service
{
    partial class EventEngineService : ServiceBase
    {
        private readonly IUnityContainer _container;
        private readonly ILog _log;
        private IServiceHosting _service;

        public EventEngineService()
        {
            InitializeComponent();

            _log = LogManager.GetLogger(typeof(EventEngineService));
            var serviceContainerFactory = new ConsoleServiceContainerFactory();
            _container = serviceContainerFactory.Create();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                _log.Info($"Starting Windows Service {GetType()}");
                _service = _container.Resolve<IServiceHosting>();
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
