using CodeConcepts.EventEngine.Contracts.Interfaces;

namespace CodeConcepts.EventEngine.Application.Interfaces.Factories
{
    public interface IEventPlayerFactory
    {
        IEventPlayer<TEventBase> Create<TEventBase>() where TEventBase : class, IEvent;
    }
}