using System.Collections.Generic;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Interfaces.DataAccess;
using Policy.Plugin.Isa.Policy.Operations.BaseTypes;
using Policy.Plugin.Isa.Policy.Operations.Commands;

namespace Policy.Plugin.Isa.Policy.Operations.CommandHandlers
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