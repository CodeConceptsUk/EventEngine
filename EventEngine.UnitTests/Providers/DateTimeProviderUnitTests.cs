using System;
using EventEngine.Interfaces.Providers;
using EventEngine.Providers;
using NUnit.Framework;

namespace EventEngine.UnitTests.Providers
{
    [TestFixture]
    public class DateTimeProviderUnitTests
    {
        private IDateTimeProvider _target;

        [SetUp]
        public void SetUp()
        {
            _target = new DateTimeProvider();
        }

        [Test]
        public void WhenIGetTheLocalDateTimeItIsReturned()
        {
            var expectedDateTime = DateTime.Now.ToLocalTime();

            var result = _target.GetLocalTime();

            Assert.That(result, Is.EqualTo(expectedDateTime).Within(TimeSpan.FromSeconds(1)));
        }

        [Test]
        public void WhenIGetTheUtcDateTimeItIsReturned()
        {
            var expectedDateTime = DateTime.Now.ToUniversalTime();

            var result = _target.GetUtcTime();

            Assert.That(result, Is.EqualTo(expectedDateTime).Within(TimeSpan.FromSeconds(1)));
        }
    }
}