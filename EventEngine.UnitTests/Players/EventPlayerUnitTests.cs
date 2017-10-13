using System;
using EventEngine.Application.Factories;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Factories;
using EventEngine.UnitTests.EventHandlers;
using EventEngine.UnitTests.Events;
using NUnit.Framework;

namespace EventEngine.UnitTests.Players
{
    public class EventPlayerUnitTests
    {
        private IEventPlayerFactory _factory;
        private IEventPlayer _target;

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
            var events = new[] { new SetNameEvent { Name = expectedName } };
            _target.Evaluate(events, state);

            Assert.AreEqual(expectedName, state.Name);
        }

        [Test]
        public void WhenIRunMultipleEventsOnTheEventPlayerTheyAreEvaluated()
        {
            var expectedName = Guid.NewGuid().ToString();
            var expectedDateOfBirth = DateTime.Now.Date;
            var state = new StateObject();
            var events = new IEvent[] { new SetNameEvent { Name = expectedName }, new SetDateOfBirthEvent { DateOfBirth = expectedDateOfBirth } };
            _target.Evaluate(events, state);

            Assert.AreEqual(expectedName, state.Name);
            Assert.AreEqual(expectedDateOfBirth, state.DateOfBirth);
        }
    }
}