using System;

namespace EventEngine.Interfaces.Services
{
    public interface IEventEvaluatorAttributeService
    {
        (string EventName, Version MinimumVersion, Version MaximumVersion) Get(Type eventEvaluatorType);
    }
}