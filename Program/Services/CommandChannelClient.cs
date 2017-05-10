using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.Practices.Unity;
using Policy.Contracts.Services;

namespace Program.Services
{
    public class CommandChannelClientFactory : ICommandChannelClientFactory, IDisposable
    {
        private readonly IUnityContainer _container;
        private readonly Uri _uri = new Uri("http://localhost/Policy/RemoteClient");
        private ChannelFactory<IRemoteClientService> _channelFactory;

        public CommandChannelClientFactory(IUnityContainer container)
        {
            _container = container;
            _channelFactory = new ChannelFactory<IRemoteClientService>(new BasicHttpBinding(), new EndpointAddress(_uri));
        }

        public IRemoteClientService Create()
        {
            return CreateChannelFactory();
        }

        private IRemoteClientService CreateChannelFactory()
        {
            foreach (var operation in _channelFactory.Endpoint.Contract.Operations)
            {
                operation.Behaviors.Find<DataContractSerializerOperationBehavior>().DataContractResolver = new CommandContractResolver(_container);
            }

            return _channelFactory.CreateChannel();
        }

        public void Dispose()
        {
            _channelFactory.Close();
            _channelFactory = null;
        }
    }
}