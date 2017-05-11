using System.ServiceModel;
using Policy.Application.Interfaces;

namespace Policy.Contracts.Services
{
    [ServiceContract]
    public interface IEventEngineApiService
    {
        [OperationContract]
        void DispatchCommand(ICommand command);
    }
}