using System;
using EventEngine.Interfaces;

namespace EventEngine.ExampleApplication.Interfaces.Queries
{
    public interface IEventQuery<out TView>
        where TView : IView
    {
        TView Get(Guid contextId, DateTime? from = null);
    }
}