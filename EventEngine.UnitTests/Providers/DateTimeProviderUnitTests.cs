using System;
using EventEngine.Interfaces.Providers;
using EventEngine.Providers;
using Xunit;

namespace EventEngine.UnitTests.Providers
{
    public class DateTimeProviderUnitTests
    {
        private readonly IDateTimeProvider _target;

        
        public DateTimeProviderUnitTests()
        {
            _target = new DateTimeProvider();
        }

        [Fact]
        public void WhenIGetTheLocalDateTimeItIsReturned()
        {
            var timeBefore = DateTime.Now;
            var result = _target.GetLocalTime();
            var timeAfter = DateTime.Now;

            Assert.InRange(result, timeBefore, timeAfter);
        }

        [Fact]
        public void WhenIGetTheUtcDateTimeItIsReturned()
        {
            var timeBefore = DateTime.UtcNow;
            var result = _target.GetUtcTime();
            var timeAfter = DateTime.UtcNow;

            Assert.InRange(result, timeBefore, timeAfter);
        }
    }
}