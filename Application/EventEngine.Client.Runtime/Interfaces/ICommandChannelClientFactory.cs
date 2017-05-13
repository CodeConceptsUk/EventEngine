using CodeConcepts.EventEngine.Api.Contracts.Services;

namespace CodeConcepts.EventEngine.ClientLibrary.Interfaces
{
    public interface ICommandChannelClientFactory
    {
        IEventEngineApiService Create();
    }
}