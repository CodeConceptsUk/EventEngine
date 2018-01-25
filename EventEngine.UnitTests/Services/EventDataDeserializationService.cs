using System;
using EventEngine.Interfaces.Events;
using EventEngine.Interfaces.Services;
using EventEngine.Services;
using NUnit.Framework;

namespace EventEngine.UnitTests.Services
{

    [TestFixture]
    public class EventDataSerializationServiceUnitTests
    {
        private IEventDataSerializationService _target;

        [SetUp]
        public void SetUp()
        {
            _target = new EventDataSerializationService();
        }

        [Test]
        public void WhenISerializeTheEventDataObjectIsSerialized()
        {
            var testObject = new TestObject { Name = Guid.NewGuid().ToString() };
            var expectedSerializedObject = "{\r\n  \"Name\": \"" + testObject.Name + "\"\r\n}";

            var result = _target.Serialize(testObject);

            Assert.AreEqual(expectedSerializedObject, result);
        }

        private class TestObject : IEventData
        {
            public string Name { get; set; }
        }
    }

    [TestFixture]
    public class EventDataDeserializationServiceUnitTests
    {
        private IEventDataDeserializationService _target;

        [SetUp]
        public void SetUp()
        {
            _target = new EventDataDeserializationService();
        }

        [Test]
        public void WhenIDeserializeTheEventDataObjectIsReturned()
        {
            var expectedName = Guid.NewGuid().ToString();
            var serializedObject = "{\r\n  \"Name\": \"" + expectedName + "\"\r\n}";

            var result = (TestObject)_target.Deserialize(typeof(TestObject), serializedObject);

            Assert.AreEqual(expectedName, result.Name);
        }

        [Test]
        public void WhenIDeserializeTheEventDataThatDoesNotMatchAnObjectIsReturned()
        {
            var expectedName = Guid.NewGuid().ToString();
            var serializedObject = "{\r\n  \"Name\": \"" + expectedName + "\"\r\n}";

            var result = (TestObject2)_target.Deserialize(typeof(TestObject2), serializedObject);

            Assert.AreEqual(expectedName, result.Name);
            Assert.AreEqual(Guid.Empty, result.Id);
            Assert.AreEqual(default(DateTime), result.When);
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