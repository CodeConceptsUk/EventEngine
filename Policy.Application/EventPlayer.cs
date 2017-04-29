using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Policy.Application.Extensions;
using Policy.Application.Interfaces;
using Policy.Application.Interfaces.Repositories;

namespace Policy.Application
{
    public class EventPlayer : IEventPlayer
    {
        private readonly IUnityContainer _container;
        private readonly IList<IEventEvaluator> _handlers = new List<IEventEvaluator>();

        public EventPlayer(IUnityContainer container)
        {
            _container = container;
            var handlers = container.ResolveAll(typeof(IEventEvaluator));
            handlers.ForEach(handler => _handlers.Add((IEventEvaluator)handler));
        }

        public TView Handle<TContext, TView>(IEnumerable<IEvent<TContext>> events)
            where TContext : class, IContext
            where TView : class, IView<TContext>, new()
        {
            var enumerable = events as IList<IEvent<TContext>> ?? events.ToList();
            if (!enumerable.Any())
            {
                return null;
            }

            var snapshotRepository = _container.Resolve<ISnapshotRepository<TContext, TView>>();

            var view = snapshotRepository.GetViewAtOrBefore(Guid.Empty, DateTime.Now) ?? new TView();

            var lastCalculatedEventAt = view.LastCalculatedEventAt;

            enumerable.Where(@event => @event.EventDateTime > lastCalculatedEventAt).ForEach(@event => EvaluateEvent(@event, view));

            snapshotRepository.SaveView(view, Guid.Empty, enumerable.Last().EventDateTime);

            return view;
        }

        private void EvaluateEvent<TContext, TView>(IEvent<TContext> @event, TView view) where TContext : class, IContext
            where TView : class, IView<TContext>, new()
        {
            var evaluators = GetEvaluator(@event.GetType(), typeof(TContext), typeof(TView));
            evaluators.ForEach(evaluator => { evaluator.AsDynamic().Evaluate(view, @event.AsDynamic()); });
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