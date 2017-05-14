using CodeConcepts.EventEngine.Api.Contracts;

namespace CodeConcepts.EventEngine.Contracts.Interfaces
{
    public interface ICommandDispatcher
    {
        void Apply(ICommand command);
    }
}