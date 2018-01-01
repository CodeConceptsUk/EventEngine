using System;
using EventEngine.Application.Attributes;
using NUnit.Framework;

namespace EventEngine.UnitTests.Attributes
{
    [TestFixture]
    public class EventNameAttributeUnitTests
    {
        [Test]
        public void WhenCreateAEventNameAttributeItIsCreated()
        {
            var expectedName = Guid.NewGuid().ToString();

            var target = new EventNameAttribute(expectedName);

            Assert.AreEqual(expectedName, target.Name);
        }

        [Test]
        public void AssertThatTheAttributeTargetsCorrectly()
        {
            var attributeUsage = (AttributeUsageAttribute)typeof(EventNameAttribute).GetCustomAttributes(typeof(AttributeUsageAttribute), true)[0];

            Assert.AreEqual(AttributeTargets.Class, attributeUsage.ValidOn);
            Assert.IsTrue(attributeUsage.Inherited);
            Assert.IsFalse(attributeUsage.AllowMultiple);
        }
    }
}