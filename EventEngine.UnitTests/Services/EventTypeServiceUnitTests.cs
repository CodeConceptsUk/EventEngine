using System;
using EventEngine.Application.Attributes;
using EventEngine.Application.Exceptions;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Services;
using EventEngine.Application.Services;
using NUnit.Framework;

namespace EventEngine.UnitTests.Services
{

    [TestFixture]
    public class EventTypeServiceUnitTests
    {

        private IEventTypeService _target;

        [SetUp]
        public void SetUp()
        {
            _target = new EventTypeService();
        }

        [Test]
        public void WhenIGetTheEventTypeForAnEventWhichHasNoVersionAttributeADefaultVersionIsReturned()
        {
            const string expectedType = nameof(Event1);
            var expectedVersion = new Version(0, 0, 0, 0);

            var result = _target.Get<Event1>();

            Assert.AreEqual(expectedType, result.Type);
            Assert.AreEqual(expectedVersion, result.Version);
        }

        [Test]
        public void WhenIGetTheEventTypeForAnEventWhichHasAVersionAttributeATheCorrectVersionIsReturned()
        {
            const string expectedType = nameof(Event2);
            var expectedVersion = new Version(1, 2, 3, 4);

            var result = _target.Get<Event2>();

            Assert.AreEqual(expectedType, result.Type);
            Assert.AreEqual(expectedVersion, result.Version);
        }

        [Test]
        public void WhenIGetTheEventTypeForAnEventWhichIsMissingTheEventNameAttributeIGetAEventDeclarationException()
        {
            var expectedMessage = $"Event '{typeof(NonEvent).Name}' is missing the EventName attribute or the attribute has no value!";
            try
            {
                _target.Get<NonEvent>();
                Assert.Fail("Should not reach!");
            }
            catch (EventDeclarationException eventDeclarationException)
            {
                Assert.AreEqual(expectedMessage, eventDeclarationException.Message);
            }
        }

        public class NonEvent : IEventData
        {
            
        }

        [EventName(nameof(Event1))]
        public class Event1 : IEventData
        {
            
        }

        [EventName(nameof(Event2))]
        [Version(1,2,3,4)]
        public class Event2 : IEventData
        {
            
        }

    }
}