namespace Application.Interfaces
{
    public interface IEventEvaluator
    {
    }

    public interface IEventEvaluator<in TEvent, TContext, in TView> : IEventEvaluator
        where TEvent : class, IEvent<TContext>
        where TContext : class, IContext
        where TView : class, IView<TContext>
    {
        void Evaluate(TView view, TEvent @event);
    }
}