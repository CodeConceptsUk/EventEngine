using System;
using EventEngine.PropertyBags;

namespace EventEngine.Interfaces.Services
{
    public interface IEventEvaluatorAttributeService
    {
        EventValidityData Get(Type eventEvaluatorType);
    }
}