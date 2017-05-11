using System.Collections.Generic;
using CodeConcepts.EventEngine.Contracts.Interfaces;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes
{
    public abstract class IsaPolicyCommandHandler<TCommand> : ICommandHandler<TCommand, IsaPolicyEvent>
        where TCommand : IsaPolicyCommand
    {
        public abstract IEnumerable<IsaPolicyEvent> Execute(TCommand command);
    }
}