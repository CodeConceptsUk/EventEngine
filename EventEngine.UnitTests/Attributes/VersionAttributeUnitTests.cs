using System;
using EventEngine.Attributes;
using Xunit;

namespace EventEngine.UnitTests.Attributes
{
    public class VersionAttributeUnitTests
    {
        [Fact]
        public void AssertThatTheAttributeTargetsCorrectly()
        {
            var attributeUsage = (AttributeUsageAttribute) typeof(VersionAttribute)
                .GetCustomAttributes(typeof(AttributeUsageAttribute), true)[0];

            Assert.Equal(AttributeTargets.Class, attributeUsage.ValidOn);
            Assert.True(attributeUsage.Inherited);
            Assert.False(attributeUsage.AllowMultiple);
        }

        [Fact]
        public void WhenCreateAVersionAttributeItIsCreated()
        {
            var expectedVersion = new Version(1, 2, 3, 4);

            var target = new VersionAttribute(expectedVersion.Major, expectedVersion.Minor,
                expectedVersion.Build, expectedVersion.Revision);

            Assert.Equal(expectedVersion, target.Version);
        }
    }
}