using System;

namespace EventEngine.Application.Interfaces.Providers
{
    public interface IDateTimeProvider
    {
        DateTime GetLocalTime();

        DateTime GetUtcTime();
    }
}