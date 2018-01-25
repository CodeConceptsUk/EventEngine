using System;
using EventEngine.Attributes;
using Xunit;

namespace EventEngine.UnitTests.Attributes
{
    public class MaximumVersionAttributeUnitTests
    {
        [Fact]
        public void AssertThatTheAttributeTargetsCorrectly()
        {
            var attributeUsage = (AttributeUsageAttribute) typeof(MaximumVersionAttribute).GetCustomAttributes(typeof(AttributeUsageAttribute), true)[0];

            Assert.Equal(AttributeTargets.Class, attributeUsage.ValidOn);
            Assert.True(attributeUsage.Inherited);
            Assert.False(attributeUsage.AllowMultiple);
        }

        [Fact]
        public void WhenCreateAMaximumVersionAttributeItIsCreated()
        {
            var expectedVersion = new Version(1, 2, 3, 4);

            var target = new MaximumVersionAttribute(expectedVersion.Major, expectedVersion.Minor,
                expectedVersion.Build, expectedVersion.Revision);

            Assert.Equal(expectedVersion, target.Version);
        }
    }
}