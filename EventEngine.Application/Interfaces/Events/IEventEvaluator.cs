using System;

namespace EventEngine.Application.Interfaces.Events
{
    public interface IEventEvaluator
    {
        string Name { get; }

        Version MinimumVersion { get; }

        Version MaximumVersion { get; }
    }
        
    public interface IEventEvaluator<in TView> : IEventEvaluator
     where TView : class, IView
    {
        void EvaluateGenericEvent(TView view, IEvent @event);
    }
}