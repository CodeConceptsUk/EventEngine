using System.Collections.Generic;
using Policy.Application.Interfaces;

namespace Policy.Plugin.Isa.Policy.Commands.CommandHandlers
{
    public abstract class IsaPolicyCommandHandler <TCommand>: ICommandHandler<TCommand>
        where TCommand : class, ICommand
    {
        public abstract IEnumerable<IEvent> Execute(TCommand command);
    }
}