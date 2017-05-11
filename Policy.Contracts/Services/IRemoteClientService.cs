using System.ServiceModel;
using Policy.Application.Interfaces;

namespace Policy.Contracts.Services
{
    [ServiceContract]
    public interface IRemoteClientService
    {
        [OperationContract]
        void Dispatch(ICommand command);
    }
}