using EventEngine.Interfaces.Events;

namespace EventEngine.Interfaces.Services
{
    public interface IEventEvaluatorRegistry
    {
        IEventEvaluator[] Filter<TView>(IEventType eventType)
            where TView : class, IView;
    }
}