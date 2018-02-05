using System;
using EventEngine.Interfaces.Events;
using EventEngine.Interfaces.Services;
using EventEngine.Services;
using Xunit;

namespace EventEngine.UnitTests.Services
{

    public class EventDataSerializationServiceUnitTests
    {
        private IEventDataSerializationService _target;

        public EventDataSerializationServiceUnitTests()
        {
            _target = new EventDataSerializationService();
        }

        [Fact]
        public void WhenISerializeTheEventDataObjectIsSerialized()
        {
            var testObject = new TestObject { Name = Guid.NewGuid().ToString() };
            var expectedSerializedObject = "{\r\n  \"$type\": \"EventEngine.UnitTests.Services.EventDataSerializationServiceUnitTests+TestObject, EventEngine.UnitTests\",\r\n  \"Name\": \"" + testObject.Name + "\"\r\n}";

            var result = _target.Serialize(testObject);

            Assert.Equal(expectedSerializedObject, result);
        }

        private class TestObject : IEventData
        {
            public string Name { get; set; }
        }
    }
}