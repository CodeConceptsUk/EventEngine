namespace EventEngine.Interfaces.Events
{
    public interface IEventEvaluator
    {
    }

    public interface IEventEvaluator<in TView, in TEventData> : IEventEvaluator
        where TView : class, IView
        where TEventData : IEventData
    {
        void Evaluate(TView view, IEvent @event, TEventData eventData);
    }
}