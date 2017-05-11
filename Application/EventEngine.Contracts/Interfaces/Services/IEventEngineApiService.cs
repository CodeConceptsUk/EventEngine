using System.ServiceModel;

namespace CodeConcepts.EventEngine.Contracts.Interfaces.Services
{
    [ServiceContract]
    public interface IEventEngineApiService
    {
        [OperationContract]
        void DispatchCommand(ICommand command);
    }
}