using System;
using System.Collections.Generic;
using FrameworkExtensions.Interfaces.Factories;
using FrameworkExtensions.Interfaces.Utilities;
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
        private IStopwatchFactory _stopwatchFactory;

        [SetUp]
        public void Setup()
        {
            _log = Substitute.For<ILog>();
            _logFactory = Substitute.For<ILogFactory>();
            _logFactory.GetLogger(typeof(EventPlayer<IEvent>)).Returns(_log);

            _stopwatchFactory = Substitute.For<IStopwatchFactory>();

            _container = Substitute.For<IUnityContainer>();
        }

        public class Event1 : IEvent
        {
            public Guid EventContextId { get; }
            public Guid EventId { get; }
            public DateTime EventDateTime { get; }
        }
        public class Event2 : IEvent
        {
            public Guid EventContextId { get; }
            public Guid EventId { get; }
            public DateTime EventDateTime { get; }
        }
        public class Event3 : IEvent
        {
            public Guid EventContextId { get; }
            public Guid EventId { get; }
            public DateTime EventDateTime { get; }
        }

        [Test]
        public void WhenIEvaluateASeriesOfEventsTheyAreEvaluatedWithTheCorrectEvaluatorsAndInOrder()
        {
            var view = Substitute.For<IView>();

            var expectedElapsed = TimeSpan.FromSeconds(123);
            var expectedDebugLogEntry1 = $"Evaluating 3 events against {view.GetType().Name}";
            var expectedDebugLogEntry2 = $"Evaluation completed in {expectedElapsed}";

            var event1 = new Event1();
            var event2 = new Event2();
            var event3 = new Event3();

            var events = new List<IEvent>
            {
                event1,
                event2,
                event3
            };

            var evaluator1 = Substitute.For<IEventEvaluator<Event1, IView>>();
            var evaluator2 = Substitute.For<IEventEvaluator<Event2, IView>>();
            var evaluator3 = Substitute.For<IEventEvaluator<Event3, IView>>();
            var evaluator4 = Substitute.For<IEventEvaluator<Event3, IView>>();
            var evaluator5 = Substitute.For<IEventEvaluator<Event3, IView>>();

            var evaluators = new List<IEventEvaluator>()
            {
                evaluator1,
                evaluator2,
                evaluator3,
                evaluator4,
                evaluator5
            };

            var stopwatch = Substitute.For<IStopwatch>();

            stopwatch.Elapsed.Returns(expectedElapsed);

            _stopwatchFactory.Create().Returns(stopwatch);

            _container.ResolveAll(typeof(IEventEvaluator)).Returns(evaluators);

            _target = new EventPlayer<IEvent>(_container, _logFactory, _stopwatchFactory);
            _target.Handle(events, view);

            Received.InOrder(() =>
            {
                _log.Debug(expectedDebugLogEntry1);
                stopwatch.Start();
                evaluator1.Evaluate(view, event1);
                evaluator2.Evaluate(view, event2);
                evaluator3.Evaluate(view, event3);
                evaluator4.Evaluate(view, event3);
                evaluator5.Evaluate(view, event3);
                stopwatch.Stop();
                _log.Debug(expectedDebugLogEntry2);
            });
        }
    }
}