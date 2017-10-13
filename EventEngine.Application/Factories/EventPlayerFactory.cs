using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Factories;
using EventEngine.Application.Players;

namespace EventEngine.Application.Factories
{
    public class EventPlayerFactory : IEventPlayerFactory
    {
        public IEventPlayer Create(params IEventEvaluator[] eventEvaluators)
        {
            return new EventPlayer(eventEvaluators);
        }
    }
}