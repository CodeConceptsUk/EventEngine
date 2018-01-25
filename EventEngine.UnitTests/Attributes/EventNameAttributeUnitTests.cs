using System;
using EventEngine.Attributes;
using Xunit;

namespace EventEngine.UnitTests.Attributes
{
    public class EventNameAttributeUnitTests
    {
        [Fact]
        public void AssertThatTheAttributeTargetsCorrectly()
        {
            var attributeUsage = (AttributeUsageAttribute) typeof(EventNameAttribute).GetCustomAttributes(typeof(AttributeUsageAttribute), true)[0];

            Assert.Equal(AttributeTargets.Class, attributeUsage.ValidOn);
            Assert.True(attributeUsage.Inherited);
            Assert.False(attributeUsage.AllowMultiple);
        }

        [Fact]
        public void WhenCreateAEventNameAttributeItIsCreated()
        {
            var expectedName = Guid.NewGuid().ToString();

            var target = new EventNameAttribute(expectedName);

            Assert.Equal(expectedName, target.Name);
        }
    }
}