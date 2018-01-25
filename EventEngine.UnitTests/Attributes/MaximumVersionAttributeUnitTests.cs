using System;
using EventEngine.Attributes;
using NUnit.Framework;

namespace EventEngine.UnitTests.Attributes
{
    [TestFixture]
    public class MaximumVersionAttributeUnitTests
    {
        [Test]
        public void AssertThatTheAttributeTargetsCorrectly()
        {
            var attributeUsage = (AttributeUsageAttribute) typeof(MaximumVersionAttribute).GetCustomAttributes(typeof(AttributeUsageAttribute), true)[0];

            Assert.AreEqual(AttributeTargets.Class, attributeUsage.ValidOn);
            Assert.IsTrue(attributeUsage.Inherited);
            Assert.IsFalse(attributeUsage.AllowMultiple);
        }

        [Test]
        public void WhenCreateAMaximumVersionAttributeItIsCreated()
        {
            var expectedVersion = new Version(1, 2, 3, 4);

            var target = new MaximumVersionAttribute(expectedVersion.Major, expectedVersion.Minor,
                expectedVersion.Build, expectedVersion.Revision);

            Assert.AreEqual(expectedVersion, target.Version);
        }
    }
}