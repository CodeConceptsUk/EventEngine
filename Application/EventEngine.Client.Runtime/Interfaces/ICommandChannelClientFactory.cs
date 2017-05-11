using CodeConcepts.EventEngine.Contracts.Interfaces.Services;

namespace CodeConcepts.EventEngine.ClientLibrary.Interfaces
{
    public interface ICommandChannelClientFactory
    {
        IEventEngineApiService Create();
    }
}