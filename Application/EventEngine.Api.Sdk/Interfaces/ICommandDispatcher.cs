using CodeConcepts.EventEngine.Api.Contracts;

namespace CodeConcepts.EventEngine.Contracts.Interfaces
{
    public interface ICommandDispatcher <in TCommand>
        where TCommand : class, ICommand
    {
        void Apply(TCommand command);
    }
}