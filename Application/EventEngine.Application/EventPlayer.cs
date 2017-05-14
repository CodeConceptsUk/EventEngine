using System;
using System.Collections.Generic;
using System.Linq;
using CodeConcepts.EventEngine.Api.Contracts;
using CodeConcepts.EventEngine.Application.Interfaces.Factories;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.FrameworkExtensions.Interfaces.Factories;
using CodeConcepts.FrameworkExtensions.LinqExtensions;
using CodeConcepts.FrameworkExtensions.ObjectExtensions;
using log4net;

namespace CodeConcepts.EventEngine.Application
{
    public class EventPlayer: IEventPlayer
    {
        private readonly IStopwatchFactory _stopwatchFactory;
        private readonly IEnumerable<IEventEvaluator> _handlers;
        private readonly ILog _logger;

        public EventPlayer(IEnumerable<IEventEvaluator> handlers, ILogFactory logFactory, IStopwatchFactory stopwatchFactory)
        {
            _handlers = handlers;
            _stopwatchFactory = stopwatchFactory;
            _logger = logFactory.GetLogger(typeof(EventPlayer));
        }

        public TView Handle<TView, TEvent>(IEnumerable<TEvent> events, TView view)
            where TView : class, IView
        where TEvent : class, IEvent
        {
            var eventArray = events.ToArray();
            _logger.Debug($"Evaluating {eventArray.Length} events against {view.GetType().Name}");
            var stopwatch = _stopwatchFactory.Create();
            stopwatch.Start();
            eventArray.ForEach(@event =>
            {
                var evaluators = GetEvaluator(@event.GetType(), typeof(TView));
                evaluators.ForEach(evaluator =>
                {
                    evaluator.AsDynamic().Evaluate(view, @event.AsDynamic());
                });
            });
            stopwatch.Stop();
            _logger.Debug($"Evaluation completed in {stopwatch.Elapsed}");

            return view;
        }

        private IEnumerable<IEventEvaluator> GetEvaluator(Type @event, Type view)
        {
            return _handlers
                .Where(t => t.GetType()
                    .GetInterfaces()
                    .Any(i => IsCorrectEvaluator(i, @event, view)));
        }

        private static bool IsCorrectEvaluator(Type evaluatorInterface, Type eventType, Type viewType)
        {
            if (!evaluatorInterface.IsGenericType || evaluatorInterface.GetGenericArguments().Length != 2)
                return false;

            var args = evaluatorInterface.GetGenericArguments();
            return args[0] == eventType &&
                   args[1] == viewType;
        }
    }
}