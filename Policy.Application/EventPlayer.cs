using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FrameworkExtensions.LinqExtensions;
using FrameworkExtensions.ObjectExtensions;
using log4net;
using Microsoft.Practices.Unity;
using Policy.Application.Interfaces;

namespace Policy.Application
{
    public class EventPlayer : IEventPlayer
    {
        private readonly IList<IEventEvaluator> _handlers = new List<IEventEvaluator>();
        private ILog _logger;

        public EventPlayer(IUnityContainer container)
        {
            _logger = LogManager.GetLogger(typeof(EventPlayer));
            var handlers = container.ResolveAll(typeof(IEventEvaluator));
            handlers.ForEach(handler => _handlers.Add((IEventEvaluator)handler));
        }

        public TView Handle<TContext, TView>(IEnumerable<IEvent<TContext>> events, TView view)
            where TContext : class, IContext
            where TView : class, IView<TContext>
        {
            _logger.Debug($"Evaluating {events.Count()} events against {view.GetType().Name}");
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            events.ForEach(@event =>
            {
                var evaluators = GetEvaluator(@event.GetType(), typeof(TContext), typeof(TView));
                evaluators.ForEach(evaluator =>
                {
                    evaluator.AsDynamic().Evaluate(view, @event.AsDynamic());
                });
            });
            stopwatch.Stop();
            _logger.Debug($"Evaluation completed in {stopwatch.Elapsed}");

            return view;
        }

        private IEnumerable<IEventEvaluator> GetEvaluator(Type @event, Type context, Type view)
        {
            return _handlers
                .Where(t => t.GetType()
                .GetInterfaces()
                .Any(i => IsCorrectHandler(i, @event, context, view)));
        }

        private static bool IsCorrectHandler(Type handlerInterface, Type eventType, Type contextType, Type viewType)
        {
            if (!handlerInterface.IsGenericType || handlerInterface.GetGenericArguments().Length != 3)
                return false;

            var args = handlerInterface.GetGenericArguments();
            return args[0] == eventType &&
                   args[1] == contextType &&
                   args[2] == viewType;
        }
    }
}