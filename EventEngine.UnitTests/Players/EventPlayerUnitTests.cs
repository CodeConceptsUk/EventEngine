using System;
using EventEngine.Application.Factories;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Factories;
using EventEngine.Application.PropertyBags;
using EventEngine.UnitTests.EventHandlers;
using EventEngine.UnitTests.Events;
using NSubstitute;
using NUnit.Framework;

namespace EventEngine.UnitTests.Players
{
    public class EventPlayerUnitTests
    {
        private IEventPlayerFactory _factory;
        private IEventPlayer _target;
        private readonly Guid _contextId = Guid.NewGuid();

        [SetUp]
        public void SetUp()
        {
            _factory = new EventPlayerFactory();
            _target = _factory.Create(new SetNameEventHandler(), new SetDateOfBirthEventHandler());
        }

        [Test]
        public void WhenIRunASingleEventOnTheEventPlayerItIsEvaluated()
        {
            var expectedName = Guid.NewGuid().ToString();
            var state = new StateObject();
            var event1 = CreateEvent(new SetNameEvent { Name = expectedName });
            var events = new[] { event1 };

            _target.Evaluate(events, state);

            Assert.AreEqual(expectedName, state.Name);
        }

        [Test]
        public void WhenIRunMultipleEventsOnTheEventPlayerTheyAreEvaluated()
        {
            var expectedName = Guid.NewGuid().ToString();
            var expectedDateOfBirth = DateTime.Now.Date;
            var state = new StateObject();
            var event1 = CreateEvent(new SetNameEvent { Name = expectedName });
            var event2 = CreateEvent(new SetDateOfBirthEvent { DateOfBirth = expectedDateOfBirth });

            var events = new[] { event1, event2 };
            _target.Evaluate(events, state);

            Assert.AreEqual(expectedName, state.Name);
            Assert.AreEqual(expectedDateOfBirth, state.DateOfBirth);
        }

        private IEvent CreateEvent<TEventData>(TEventData eventData)
            where TEventData : IEventData
        {
            var @event = Substitute.For<IEvent>();
            @event.ContextId.Returns(_contextId);
            @event.EventDateTime.Returns(DateTime.Now);
            return @event;
        }
    }
}