using System;

namespace EventEngine.Interfaces.Providers
{
    public interface IDateTimeProvider
    {
        DateTime GetLocalTime();

        DateTime GetUtcTime();
    }
}