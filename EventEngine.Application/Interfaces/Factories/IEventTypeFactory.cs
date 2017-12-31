using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Repositories;

namespace EventEngine.Application.Interfaces.Factories
{
    public interface IEventTypeFactory
    {
        IEventType Get(IEventData eventData);
    }
}