using System;

namespace EventEngine.Application.Exceptions
{
    public class EventEngineException :Exception
    {
        public EventEngineException(string message)
            : base(message)
        {
        }
    }
}