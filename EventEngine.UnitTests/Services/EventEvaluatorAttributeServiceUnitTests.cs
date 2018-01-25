using System;
using EventEngine.Attributes;
using EventEngine.Interfaces.Events;
using EventEngine.Interfaces.Services;
using EventEngine.Services;
using Xunit;

namespace EventEngine.UnitTests.Services
{
    public class EventEvaluatorAttributeServiceUnitTests
    {
        private IEventEvaluatorAttributeService _target;

        public EventEvaluatorAttributeServiceUnitTests()
        {
            _target = new EventEvaluatorAttributeService();
        }

        [Fact]
        public void WhenIGetEventAttributesWithOnlyEventNameItIsReturned()
        {
            var result = _target.Get(typeof(Test1));

            Assert.Equal("Test1", result.EventName);
            Assert.Equal(new Version(0, 0, 0, 0), result.MinimumVersion);
            Assert.Null(result.MaximumVersion);
        }

        [Fact]
        public void WhenIGetEventAttributesWithEventNameAndMinimumVersionItIsReturned()
        {
            var result = _target.Get(typeof(Test2));

            Assert.Equal("Test2", result.EventName);
            Assert.Equal(new Version(1, 2, 3, 4), result.MinimumVersion);
            Assert.Null(result.MaximumVersion);
        }

        [Fact]
        public void WhenIGetEventAttributesWithAllAttibutesItIsReturned()
        {
            var result = _target.Get(typeof(Test3));

            Assert.Equal("Test3", result.EventName);
            Assert.Equal(new Version(1, 2, 3, 4), result.MinimumVersion);
            Assert.Equal(new Version(2, 3, 4, 5), result.MaximumVersion);
        }

        [EventName("Test1")]
        public class Test1 : IEventEvaluator
        {
        }

        [EventName("Test2")]
        [MinimumVersion(1, 2, 3, 4)]
        public class Test2 : IEventEvaluator
        {
        }

        [EventName("Test3")]
        [MinimumVersion(1, 2, 3, 4)]
        [MaximumVersion(2, 3, 4, 5)]
        public class Test3 : IEventEvaluator
        {
        }
    }
}