using System;
using EventEngine.Application.Interfaces.Providers;

namespace EventEngine.Application.Providers
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetLocalTime()
        {
            return DateTime.Now.ToLocalTime();
        }

        public DateTime GetUtcTime()
        {
            return DateTime.Now.ToUniversalTime();
        }
    }
}