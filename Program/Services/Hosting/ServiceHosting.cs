using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.Practices.Unity;
using Policy.Contracts.Services;

namespace Program.Services
{
    public class ServiceHosting : IServiceHosting
    {
        private ServiceHost _serviceHost;

        public ServiceHosting(IUnityContainer container)
        {
            var serviceInstance = container.Resolve<IRemoteClientService>();
            _serviceHost = new ServiceHost(serviceInstance, new Uri("http://localhost/Policy/RemoteClient"));
            
            var smb = _serviceHost.Description.Behaviors.Find<ServiceMetadataBehavior>();

            if (smb == null)
            {
                smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                _serviceHost.Description.Behaviors.Add(smb);
            }

            var endpoint = new ServiceEndpoint(
                ContractDescription.GetContract(typeof(IRemoteClientService)),
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