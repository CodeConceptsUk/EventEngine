using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FrameworkExtensions.Interfaces.Factories;
using FrameworkExtensions.LinqExtensions;
using FrameworkExtensions.ObjectExtensions;
using log4net;
using Microsoft.Practices.Unity;
using Policy.Application.Interfaces;
using Policy.Application.Interfaces.Factories;

namespace Policy.Application
{
    public class EventPlayer <TEvent>: IEventPlayer<TEvent>
        where TEvent : class, IEvent
    {
        private readonly IStopwatchFactory _stopwatchFactory;
        private readonly List<IEventEvaluator> _handlers = new List<IEventEvaluator>();
        private readonly ILog _logger;

        public EventPlayer(IUnityContainer container, ILogFactory logFactory, IStopwatchFactory stopwatchFactory)
        {
            _stopwatchFactory = stopwatchFactory;
            _logger = logFactory.GetLogger(typeof(EventPlayer<TEvent>));
            var handlers = container.ResolveAll(typeof(IEventEvaluator)).OfType<IEventEvaluator>().ToList();
            _handlers.AddRange(handlers);
        }

        public TView Handle<TView>(IEnumerable<TEvent> events, TView view)
            where TView : class, IView
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