using EventEngine.Application.Interfaces.Events;

namespace EventEngine.Application.Interfaces.Factories
{
    public interface IEventPlayerFactory
    {
        IEventPlayer Create(params IEventEvaluator[] eventEvaluators);
    }
}