using System;
using System.Collections.Generic;
using System.Linq;
using Application.Extensions;
using Application.Interfaces;
using Microsoft.Practices.Unity;

namespace Application
{
    public class EventPlayer : IEventPlayer
    {
        private readonly IList<IEventEvaluator> _handlers = new List<IEventEvaluator>();

        public EventPlayer(IUnityContainer container)
        {
            var handlers = container.ResolveAll(typeof(IEventEvaluator));
            handlers.ForEach(handler => _handlers.Add((IEventEvaluator)handler));
        }

        public TView Handle<TContext, TView>(IEnumerable<IEvent<TContext>> events)
            where TContext : class, IContext
            where TView : class, IView<TContext>, new()
        {
            var view = new TView();
            
            events.ForEach(@event =>
            {
                var evaluators = GetEvaluator(@event.GetType(), typeof(TContext), typeof(TView));
                evaluators.ForEach(evaluator =>
                {
                    evaluator.AsDynamic().Evaluate(view, @event.AsDynamic());
                });
            });

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