using System;

namespace EventEngine.Application.Interfaces.Events
{
    public interface IEventEvaluator
    {
    }

    public interface IEventEvaluator<in TView> : IEventEvaluator
        where TView : class, IView
    {
        void EvaluateGenericEvent(TView view, IEvent @event);
    }
}