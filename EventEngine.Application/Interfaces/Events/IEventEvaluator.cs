namespace EventEngine.Application.Interfaces.Events
{
    public interface IEventEvaluator
    {
    }

    public interface IEventEvaluator<in TEvent, in TView> : IEventEvaluator
        where TEvent : class, IEvent
        where TView : class, IView
    {
        void Evaluate(TView view, TEvent @event);
    }
}