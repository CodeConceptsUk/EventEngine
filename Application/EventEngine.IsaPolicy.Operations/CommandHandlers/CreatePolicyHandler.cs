using System.Collections.Generic;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Commands;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CommandHandlers
{
    public class CreatePolicyHandler : ICommandHandler<CreatePolicyCommand, IsaPolicyEvent>
    {
        public IEnumerable<IsaPolicyEvent> Execute(CreatePolicyCommand command)
        {
            var @event = new PolicyCreatedEvent(command.ContextId, command.PolicyNumber, command.CustomerId);
            return new[] { @event };
        }
    }
}