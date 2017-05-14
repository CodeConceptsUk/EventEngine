using System;
using CodeConcepts.FrameworkExtensions.Interfaces.Providers;

namespace CodeConcepts.FrameworkExtensions.Providers
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetDate()
        {
            return DateTime.Now;
        }
    }
}