using CodeConcepts.EventEngine.Contracts.Interfaces;

namespace CodeConcepts.EventEngine.Application.Interfaces
{
    public interface ICommandDispatcher <in TCommand>
        where TCommand : class, ICommand
    {
        void Apply(TCommand command);
    }
}