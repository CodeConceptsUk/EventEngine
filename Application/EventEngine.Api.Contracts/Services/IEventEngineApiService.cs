using System.ServiceModel;

namespace CodeConcepts.EventEngine.Api.Contracts.Services
{
    [ServiceContract]
    public interface IEventEngineApiService
    {
        [OperationContract]
        void DispatchCommand(ICommand command);
    }
}