using EventEngine.Application.Interfaces;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Services;
using EventEngine.Application.Players;
using NSubstitute;
using NUnit.Framework;

namespace EventEngine.UnitTests.Players
{
    public class EventPlayerUnitTests
    {
        private IEventEvaluatorFilteringService _eventEvaluatorFilteringService;
        private IEventPlayer _target;
        private IEventDataDeserializationService _eventDataDeserializationService;

        [SetUp]
        public void SetUp()
        {
            _eventEvaluatorFilteringService = Substitute.For<IEventEvaluatorFilteringService>();
            _eventDataDeserializationService = Substitute.For<IEventDataDeserializationService>();
            _target = new EventPlayer(_eventEvaluatorFilteringService, _eventDataDeserializationService);
        }

        [Test]
        public void WhenIRunMultipleEventsOnTheEventPlayerTheyAreEvaluated()
        {
            var eventType1 = Substitute.For<IEventType>();
            var eventType2 = Substitute.For<IEventType>();
            var eventType3 = Substitute.For<IEventType>();

            var evaluator1 = Substitute.For<IEventEvaluator<IView, IEventData>>();
            var evaluator2 = Substitute.For<IEventEvaluator<IView, IEventData>>();
            var evaluator3 = Substitute.For<IEventEvaluator<IView, IEventData>>();

            var events = new[]
            {
                Substitute.For<IEvent>(),
                Substitute.For<IEvent>(),
                Substitute.For<IEvent>()
            };

            events[0].EventType.Returns(eventType1);
            events[1].EventType.Returns(eventType2);
            events[2].EventType.Returns(eventType3);

            _eventEvaluatorFilteringService.Filter<IView>(eventType1).Returns(new IEventEvaluator[] {evaluator1});
            _eventEvaluatorFilteringService.Filter<IView>(eventType2).Returns(new IEventEvaluator[] {evaluator2});
            _eventEvaluatorFilteringService.Filter<IView>(eventType3).Returns(new IEventEvaluator[] {evaluator3});

            var view = Substitute.For<IView>();

            _target.Play(events, view);

            evaluator1.Received().EvaluateGenericEvent(view, events[0]);
            evaluator2.Received().EvaluateGenericEvent(view, events[1]);
            evaluator3.Received().EvaluateGenericEvent(view, events[2]);
        }
    }
}