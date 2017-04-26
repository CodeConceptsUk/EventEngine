using System.Collections.Generic;

namespace Engine.Handlers.Events
{
    public interface ICalcEngine
    {
        Dictionary<string, decimal> Calculate(string values);
    }
}