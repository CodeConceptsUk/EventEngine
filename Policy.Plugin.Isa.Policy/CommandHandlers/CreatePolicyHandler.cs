using System.Collections.Generic;
using Policy.Application.Interfaces;
using Policy.Application.Interfaces.Repositories;
using Policy.Plugin.Isa.Policy.Commands;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Interfaces.DataAccess;
using Policy.Plugin.Isa.Policy.Interfaces.Domain;

namespace Policy.Plugin.Isa.Policy.CommandHandlers
{
    public class CreatePolicyHandler : ICommandHandler<CreatePolicyCommand, IPolicyContext>
    {
        private readonly ISequencingRepository _sequencingRepository;

        public CreatePolicyHandler(ISequencingRepository sequencingRepository)
        {
            _sequencingRepository = sequencingRepository;
        }

        public IEnumerable<IEvent<IPolicyContext>> Execute(CreatePolicyCommand command)
        {
            var policyNumber = _sequencingRepository.Get("IsaPolicy");
            var @event = new PolicyCreatedEvent(policyNumber, command.CustomerId);
            return new[] { @event };
        }
    }
}