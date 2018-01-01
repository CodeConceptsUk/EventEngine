using System.Collections.Generic;
using EventEngine.Application.Interfaces;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Services;

namespace EventEngine.Application.Players
{
    internal class EventPlayer : IEventPlayer
    {
        private readonly IEventEvaluatorFilteringService _eventEvaluatorFilteringService;
        private readonly IEventEvaluator[] _eventEvaluators;

        internal EventPlayer(IEventEvaluatorFilteringService eventEvaluatorFilteringService, IEventEvaluator[] eventEvaluators)
        {
            _eventEvaluatorFilteringService = eventEvaluatorFilteringService;
            _eventEvaluators = eventEvaluators;
        }

        public void Play<TView>(IEnumerable<IEvent> events, TView view)
            where TView : class, IView
        {
            foreach (var @event in events)
            {
                var eventEvaluators = _eventEvaluatorFilteringService.Filter<TView>(@event.EventType);
                foreach (var eventEvaluator in eventEvaluators)
                    ((IEventEvaluator<TView>) eventEvaluator).EvaluateGenericEvent(view, @event);
            }
        }
    }
}