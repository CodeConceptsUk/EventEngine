using System;
using EventEngine.Interfaces.Events;
using EventEngine.PropertyBags;
using Xunit;

namespace EventEngine.UnitTests.PropertyBags
{
    public class EventTypeUnitTests
    {
        private IEventType _target;

        [Fact]
        public void WhenTheEventTypeIsConstructedItIsPopulatedAsExpected()
        {
            var expectedVersion = new Version(4, 2, 1, 2);
            var expectedType = Guid.NewGuid().ToString();

            _target = new EventType(expectedType, expectedVersion);

            Assert.Same(expectedVersion, _target.Version);
            Assert.Equal(expectedType, _target.Name);
        }
    }
}