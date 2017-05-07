using System.Collections.Generic;
using log4net;
using Microsoft.Practices.Unity;
using NSubstitute;
using NUnit.Framework;
using Policy.Application.Interfaces;
using Policy.Application.Interfaces.Factories;

namespace Policy.Application.UnitTests
{
    [TestFixture]
    public class EventPlayerUnitTests
    {
        private IEventPlayer<IEvent> _target;
        private IUnityContainer _container;
        private ILogFactory _logFactory;
        private ILog _log;

        [SetUp]
        public void Setup()
        {
            _log = Substitute.For<ILog>();
            _logFactory = Substitute.For<ILogFactory>();
            _logFactory.GetLogger(typeof(EventPlayer<IEvent>)).Returns(_log);

            _container = Substitute.For<IUnityContainer>();
            _target = new EventPlayer<IEvent>(_container, _logFactory);
        }

        [Test]
        public void Test1()
        {
            var view = Substitute.For<IView>();

            var expectedDebugLogEntry1 = $"Evaluating 3 events against {view.GetType().Name}";

            var event1 = Substitute.For<IEvent>();
            var event2 = Substitute.For<IEvent>();
            var event3 = Substitute.For<IEvent>();

            var events = new List<IEvent>
            {
                event1,
                event2,
                event3
            };

            var evaluator1 = Substitute.For<IEventEvaluator<IEvent, IView>>();
            var evaluator2 = Substitute.For<IEventEvaluator<IEvent, IView>>();
            var evaluator3 = Substitute.For<IEventEvaluator<IEvent, IView>>();

            var evaluators = new List<IEventEvaluator>()
            {
                evaluator1,
                evaluator2,
                evaluator3
            };

            _container.ResolveAll(typeof(IEventEvaluator)).Returns(evaluators);

            _target.Handle(events, view);

            _log.Received().Debug(expectedDebugLogEntry1);

            evaluator1.Received().Evaluate(view, event1);
            evaluator2.Received().Evaluate(view, event1);
            evaluator3.Received().Evaluate(view, event1);

            evaluator1.Received().Evaluate(view, event2);
            evaluator2.Received().Evaluate(view, event2);
            evaluator3.Received().Evaluate(view, event2);

            evaluator1.Received().Evaluate(view, event3);
            evaluator2.Received().Evaluate(view, event3);
            evaluator3.Received().Evaluate(view, event3);

            _log.Received().Debug($"Some String");
        }
    }
}