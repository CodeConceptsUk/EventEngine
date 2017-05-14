using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using CodeConcepts.EventEngine.Api.Contracts.Resolvers;
using CodeConcepts.EventEngine.Api.Contracts.Services;
using CodeConcepts.EventEngine.Application.Interfaces.Factories;
using log4net;
using SimpleInjector;

namespace CodeConcepts.EventEngine.Application.Hosting
{
    public class ServiceHosting : IServiceHosting
    {
        private ServiceHost _serviceHost;
        private readonly ILog _logger;

        public ServiceHosting(Container container, IEventEngineApiService serviceInstance, ILogFactory logFactory)
        {
            _logger = logFactory.GetLogger(typeof(ServiceHosting));

            var hostUrl = "http://localhost/Policy/RemoteClient";
            
            _logger.Info($"Configuring ServiceHost to listen at {hostUrl}");
            _serviceHost = new ServiceHost(serviceInstance, new Uri(hostUrl));
            
            var smb = _serviceHost.Description.Behaviors.Find<ServiceMetadataBehavior>();

            if (smb == null)
            {
                smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                _serviceHost.Description.Behaviors.Add(smb);
            }

            var endpoint = new ServiceEndpoint(
                ContractDescription.GetContract(typeof(IEventEngineApiService)),
                new WSHttpBinding(),
                new EndpointAddress(new Uri(hostUrl)));
            foreach (var operation in endpoint.Contract.Operations)
            {
                var behaviour = operation.Behaviors.Find<DataContractSerializerOperationBehavior>();
                behaviour.DataContractResolver = new EventEngineContractResolver(container);
            }
            _serviceHost.AddServiceEndpoint(endpoint);
            _serviceHost.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexHttpBinding(), "mex");
        }

        public void Start()
        {
            _serviceHost.Open();
            _logger.Info($"ServiceHost started at {_serviceHost.Description.Endpoints.First().Address}");
        }

        public void Dispose()
        {
            _logger.Info($"ServiceHost stopping!");
            if (_serviceHost.State == CommunicationState.Opened)
                _serviceHost.Close();
            _serviceHost = null;
        }
    }
}