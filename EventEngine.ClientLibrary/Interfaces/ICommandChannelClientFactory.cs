using Policy.Contracts.Services;

namespace Program.Services
{
    public interface ICommandChannelClientFactory
    {
        IEventEngineApiService Create();
    }
}