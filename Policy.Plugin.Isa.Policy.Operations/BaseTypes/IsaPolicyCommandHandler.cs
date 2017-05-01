using System.Collections.Generic;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Events;

namespace Policy.Plugin.Isa.Policy.Operations.BaseTypes
{
    public abstract class IsaPolicyCommandHandler<TCommand> : ICommandHandler<TCommand, IsaPolicyEvent>
        where TCommand : class, ICommand
    {
        public abstract IEnumerable<IsaPolicyEvent> Execute(TCommand command);
    }
}