using System;
using EventEngine.Application.Attributes;
using NUnit.Framework;

namespace EventEngine.UnitTests.Attributes
{
    [TestFixture]
    public class VersionAttributeUnitTests
    {
        [Test]
        public void WhenCreateAVersionAttributeItIsCreated()
        {
            var expectedVersion = new Version(1, 2, 3, 4);

            var target = new VersionAttribute(expectedVersion.Major, expectedVersion.Minor,
                expectedVersion.Build, expectedVersion.Revision);

            Assert.AreEqual(expectedVersion, target.Version);
        }

        [Test]
        public void AssertThatTheAttributeTargetsCorrectly()
        {
            var attributeUsage = (AttributeUsageAttribute)typeof(VersionAttribute)
                .GetCustomAttributes(typeof(AttributeUsageAttribute), true)[0];

            Assert.AreEqual(AttributeTargets.Class, attributeUsage.ValidOn);
            Assert.IsTrue(attributeUsage.Inherited);
            Assert.IsFalse(attributeUsage.AllowMultiple);
        }
    }
}