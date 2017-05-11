using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using CodeConcepts.EventEngine.Contracts.Interfaces.Services;
using CodeConcepts.EventEngine.Contracts.Resolvers;
using Microsoft.Practices.Unity;

namespace CodeConcepts.EventEngine.Services.Hosting
{
    public class ServiceHosting : IServiceHosting
    {
        private ServiceHost _serviceHost;

        public ServiceHosting(IUnityContainer container)
        {
            var serviceInstance = container.Resolve<IEventEngineApiService>();
            _serviceHost = new ServiceHost(serviceInstance, new Uri("http://localhost/Policy/RemoteClient"));
            
            var smb = _serviceHost.Description.Behaviors.Find<ServiceMetadataBehavior>();

            if (smb == null)
            {
                smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                _serviceHost.Description.Behaviors.Add(smb);
            }

            var endpoint = new ServiceEndpoint(
                ContractDescription.GetContract(typeof(IEventEngineApiService)),
                new BasicHttpBinding(),
                new EndpointAddress(new Uri("http://localhost/Policy/RemoteClient")));
            foreach (var operation in endpoint.Contract.Operations)
            {
                var behaviour = operation.Behaviors.Find<DataContractSerializerOperationBehavior>();
                behaviour.DataContractResolver = new CommandContractResolver(container);
            }
            _serviceHost.AddServiceEndpoint(endpoint);
            _serviceHost.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexHttpBinding(), "mex");
        }

        public void Start()
        {
            _serviceHost.Open();
            Console.WriteLine($"Service started at {_serviceHost.Description.Endpoints.First().Address}");
        }

        public void Dispose()
        {
            if (_serviceHost.State == CommunicationState.Opened)
                _serviceHost.Close();
            _serviceHost = null;
        }
    }
}