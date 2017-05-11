using System.Collections.Generic;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Commands;
using CodeConcepts.EventEngine.IsaPolicy.DataAccess.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Events.Events;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CommandHandlers
{
    public class CreatePolicyHandler : IsaPolicyCommandHandler<CreatePolicyCommand>
    {
        private readonly ISequencingRepository _sequencingRepository;

        public CreatePolicyHandler(ISequencingRepository sequencingRepository)
        {
            _sequencingRepository = sequencingRepository;
        }

        public override IEnumerable<IsaPolicyEvent> Execute(CreatePolicyCommand command)
        {
            var policyNumber = _sequencingRepository.Get("IsaPolicyPolicyNumber");
            var @event = new PolicyCreatedEvent(policyNumber, command.CustomerId);
            return new[] { @event };
        }
    }
}