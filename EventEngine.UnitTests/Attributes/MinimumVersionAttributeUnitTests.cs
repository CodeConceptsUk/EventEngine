using System;
using EventEngine.Application.Attributes;
using EventEngine.Application.Dispatchers;
using NUnit.Framework;

namespace EventEngine.UnitTests.Attributes
{
    [TestFixture]
    public class MinimumVersionAttributeUnitTests
    {
        [Test]
        public void WhenCreateAMinimumVersionAttributeItIsCreated()
        {
            var expectedVersion = new Version(1, 2, 3, 4);
            
            var target = new MinimumVersionAttribute(expectedVersion.Major, expectedVersion.Minor,
                expectedVersion.Build, expectedVersion.Revision);

            Assert.AreEqual(expectedVersion, target.Version);
        }

        [Test]
        public void AssertThatTheAttributeTargetsCorrectly()
        {
            var attributeUsage = (AttributeUsageAttribute)typeof(MinimumVersionAttribute).GetCustomAttributes(typeof(AttributeUsageAttribute), true)[0];

            Assert.AreEqual( AttributeTargets.Class, attributeUsage.ValidOn);
            Assert.IsTrue(attributeUsage.Inherited);
            Assert.IsFalse(attributeUsage.AllowMultiple);
        }
    }
}