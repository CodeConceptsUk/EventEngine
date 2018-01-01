using System;
using System.Security.Cryptography.X509Certificates;
using EventEngine.Application.Interfaces.Events;
using EventEngine.Application.Interfaces.Services;
using EventEngine.Application.Services;
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

            var result = _target.Deserialize<TestObject>(serializedObject);

            Assert.AreEqual(expectedName, result.Name);
        }

        private class TestObject : IEventData
        {
            public string Name { get; set; }
        }
    }
}