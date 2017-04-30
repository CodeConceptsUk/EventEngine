using System.Collections.Generic;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Commands.Commands;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Interfaces.DataAccess;

namespace Policy.Plugin.Isa.Policy.Commands.CommandHandlers
{
    public class CreatePolicyHandler : ICommandHandler<CreatePolicyCommand>
    {
        private readonly ISequencingRepository _sequencingRepository;

        public CreatePolicyHandler(ISequencingRepository sequencingRepository)
        {
            _sequencingRepository = sequencingRepository;
        }

        public IEnumerable<IEvent> Execute(CreatePolicyCommand command)
        {
            var policyNumber = _sequencingRepository.Get("IsaPolicy");
            var @event = new PolicyCreatedEvent(policyNumber, command.CustomerId);
            return new[] { @event };
        }
    }
}