using System;
using EventEngine.Attributes;
using EventEngine.Interfaces.Events;
using EventEngine.Interfaces.Services;
using EventEngine.Services;
using NUnit.Framework;

namespace EventEngine.UnitTests.Services
{
    [TestFixture]
    public class EventEvaluatorAttributeServiceUnitTests
    {
        private IEventEvaluatorAttributeService _target;

        [SetUp]
        public void SetUp()
        {
            _target = new EventEvaluatorAttributeService();
        }

        [Test]
        public void WhenIGetEventAttributesWithOnlyEventNameItIsReturned()
        {
            var result = _target.Get(typeof(Test1));

            Assert.AreEqual("Test1", result.EventName);
            Assert.AreEqual(new Version(0, 0, 0, 0), result.MinimumVersion);
            Assert.IsNull(result.MaximumVersion);
        }

        [Test]
        public void WhenIGetEventAttributesWithEventNameAndMinimumVersionItIsReturned()
        {
            var result = _target.Get(typeof(Test2));

            Assert.AreEqual("Test2", result.EventName);
            Assert.AreEqual(new Version(1, 2, 3, 4), result.MinimumVersion);
            Assert.IsNull(result.MaximumVersion);
        }

        [Test]
        public void WhenIGetEventAttributesWithAllAttibutesItIsReturned()
        {
            var result = _target.Get(typeof(Test3));

            Assert.AreEqual("Test3", result.EventName);
            Assert.AreEqual(new Version(1, 2, 3, 4), result.MinimumVersion);
            Assert.AreEqual(new Version(2, 3, 4, 5), result.MaximumVersion);
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