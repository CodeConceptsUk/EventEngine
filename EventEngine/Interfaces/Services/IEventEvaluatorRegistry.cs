using EventEngine.Application.Interfaces.Events;

namespace EventEngine.Application.Interfaces.Services
{
    public interface IEventEvaluatorRegistry
    {
        IEventEvaluator[] Filter<TView>(IEventType eventType)
            where TView : class, IView;

        void Register(params IEventEvaluator[] eventEvaluators );
    }
}