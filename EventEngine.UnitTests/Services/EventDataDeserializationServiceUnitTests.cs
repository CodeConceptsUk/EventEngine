using System;
using EventEngine.Interfaces.Events;
using EventEngine.Interfaces.Services;
using EventEngine.Services;
using Xunit;

namespace EventEngine.UnitTests.Services
{
    public class EventDataDeserializationServiceUnitTests
    {
        private IEventDataDeserializationService _target;

        public EventDataDeserializationServiceUnitTests()
        {
            _target = new EventDataDeserializationService();
        }

        [Fact]
        public void WhenIDeserializeTheEventDataObjectIsReturned()
        {
            var expectedName = Guid.NewGuid().ToString();
            var serializedObject = "{\r\n  \"$type\": \"EventEngine.UnitTests.Services.EventDataDeserializationServiceUnitTests+TestObject, EventEngine.UnitTests\",\r\n    \"Name\": \"" + expectedName + "\"\r\n}";

            var result = (TestObject)_target.Deserialize(typeof(TestObject), serializedObject);

            Assert.Equal(expectedName, result.Name);
        }

        [Fact(Skip="Failing - breaks event versioning - solve using mapping system instead of allowing json to map")]
        public void WhenIDeserializeTheEventDataThatDoesNotMatchAnObjectIsReturned()
        {
            var expectedName = Guid.NewGuid().ToString();
            var serializedObject = "{\r\n  \"$type\": \"EventEngine.UnitTests.Services.EventDataDeserializationServiceUnitTests+TestObject, EventEngine.UnitTests\",\r\n    \"Name\": \"" + expectedName + "\"\r\n}";

            var result = (TestObject2)_target.Deserialize(typeof(TestObject2), serializedObject);

            Assert.Equal(expectedName, result.Name);
            Assert.Equal(Guid.Empty, result.Id);
            Assert.Equal(default(DateTime), result.When);
        }

        private class TestObject : IEventData
        {
            public string Name { get; set; }
        }

        private class TestObject2 : IEventData
        {
            public string Name { get; set; }

            public Guid Id { get; set; }

            public DateTime When { get; set; }
        }
    }
}