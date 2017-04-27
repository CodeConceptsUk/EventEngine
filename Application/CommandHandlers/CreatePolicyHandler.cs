using System.Collections.Generic;
using Application.Commands;
using Application.Events;
using Application.Interfaces;
using Application.Interfaces.Domain;
using Application.Interfaces.Repositories;

namespace Application.CommandHandlers
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