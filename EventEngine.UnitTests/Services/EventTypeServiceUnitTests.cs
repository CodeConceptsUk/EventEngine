using System;
using EventEngine.Attributes;
using EventEngine.Exceptions;
using EventEngine.Interfaces.Events;
using EventEngine.Interfaces.Services;
using EventEngine.Services;
using Xunit;

namespace EventEngine.UnitTests.Services
{
    public class EventTypeServiceUnitTests
    {
        public EventTypeServiceUnitTests()
        {
            _target = new EventTypeService();
        }

        private IEventTypeService _target;

        public class NonEvent : IEventData
        {
        }

        [EventName(nameof(Event1))]
        public class Event1 : IEventData
        {
        }

        [EventName(nameof(Event2))]
        [Version(1, 2, 3, 4)]
        public class Event2 : IEventData
        {
        }

        [Fact]
        public void WhenIGetTheEventTypeForAnEventWhichHasAVersionAttributeATheCorrectVersionIsReturned()
        {
            const string expectedType = nameof(Event2);
            var expectedVersion = new Version(1, 2, 3, 4);

            var result = _target.Get<Event2>();

            Assert.Equal(expectedType, result.Name);
            Assert.Equal(expectedVersion, result.Version);
        }

        [Fact]
        public void WhenIGetTheEventTypeForAnEventWhichHasNoVersionAttributeADefaultVersionIsReturned()
        {
            const string expectedType = nameof(Event1);
            var expectedVersion = new Version(0, 0, 0, 0);

            var result = _target.Get<Event1>();

            Assert.Equal(expectedType, result.Name);
            Assert.Equal(expectedVersion, result.Version);
        }

        [Fact]
        public void WhenIGetTheEventTypeForAnEventWhichIsMissingTheEventNameAttributeIGetAEventDeclarationException()
        {
            var expectedMessage = $"Event '{typeof(NonEvent).Name}' is missing the EventName attribute or the attribute has no value!";
            try
            {
                _target.Get<NonEvent>();
                Assert.True(false, "Should not reach!");
            }
            catch (EventDeclarationException eventDeclarationException)
            {
                Assert.Equal(expectedMessage, eventDeclarationException.Message);
            }
        }
    }
}