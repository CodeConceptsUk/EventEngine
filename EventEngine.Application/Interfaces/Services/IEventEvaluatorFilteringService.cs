using System;
using EventEngine.Application.Interfaces.Events;

namespace EventEngine.Application.Interfaces.Services
{
    public interface IEventEvaluatorFilteringService
    {
        IEventEvaluator[] Filter<TView>(IEventEvaluator[] eventEvaluators, IEventType eventType)
            where TView : class, IView;
    }
}