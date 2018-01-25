using System;
using EventEngine.Interfaces.Providers;

namespace EventEngine.Providers
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