using System;
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
            var evaluator1 = Substitute.For<IEventEvaluator<IView, IEventData>>();
            var evaluator2 = Substitute.For<IEventEvaluator<IView, IEventData>>();
            var evaluator3 = Substitute.For<IEventEvaluator<IView, IEventData>>();
            var eventData1 = Substitute.For<IEventData>();
            var eventData2 = Substitute.For<IEventData>();
            var eventData3 = Substitute.For<IEventData>();

            var events = new[]
            {
                CreateEvent(Guid.NewGuid().ToString()),
                CreateEvent(Guid.NewGuid().ToString()),
                CreateEvent(Guid.NewGuid().ToString()),
            };

            _eventDataDeserializationService.Deserialize(typeof(IEventData), events[0].EventData).Returns(eventData1);
            _eventDataDeserializationService.Deserialize(typeof(IEventData), events[1].EventData).Returns(eventData2);
            _eventDataDeserializationService.Deserialize(typeof(IEventData), events[2].EventData).Returns(eventData3);
            _eventEvaluatorFilteringService.Filter<IView>(events[0].EventType).Returns(new IEventEvaluator[] {evaluator1});
            _eventEvaluatorFilteringService.Filter<IView>(events[1].EventType).Returns(new IEventEvaluator[] {evaluator2});
            _eventEvaluatorFilteringService.Filter<IView>(events[2].EventType).Returns(new IEventEvaluator[] {evaluator3});

            var view = Substitute.For<IView>();

            _target.Play(events, view);

            evaluator1.Received().Evaluate(view, events[0], eventData1);
            evaluator2.Received().Evaluate(view, events[1], eventData2);
            evaluator3.Received().Evaluate(view, events[2], eventData3);
        }

        private static IEvent CreateEvent(string eventData)
        {
            var @event = Substitute.For<IEvent>();
            var eventType = Substitute.For<IEventType>();
            @event.EventData.Returns(eventData);
            @event.EventType.Returns(eventType);
            return @event;
        }
    }
}