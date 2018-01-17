using System;

namespace EventEngine.Application.Interfaces.Services
{
    public interface IEventEvaluatorAttributeService
    {
        (string EventName, Version MinimumVersion, Version MaximumVersion) Get(Type eventEvaluatorType);
    }
}