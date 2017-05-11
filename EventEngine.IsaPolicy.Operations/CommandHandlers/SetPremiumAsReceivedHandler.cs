using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using Policy.Application.Exceptions;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Operations.BaseTypes;
using Policy.Plugin.Isa.Policy.Operations.Commands;
using Policy.Plugin.Isa.Policy.Views.Queries;

namespace Policy.Plugin.Isa.Policy.Operations.CommandHandlers
{
    public class SetPremiumAsReceivedHandler : IsaPolicyCommandHandler<SetPremiumAsReceivedCommand>
    {
        private readonly IPolicyEventContextIdQuery _policyEventContextIdQuery;
        private readonly ISinglePolicyQuery _singlePolicyQuery;

        public SetPremiumAsReceivedHandler(IPolicyEventContextIdQuery policyEventContextIdQuery, ISinglePolicyQuery singlePolicyQuery)
        {
            _policyEventContextIdQuery = policyEventContextIdQuery;
            _singlePolicyQuery = singlePolicyQuery;
        }

        public override IEnumerable<IsaPolicyEvent> Execute(SetPremiumAsReceivedCommand command)
        {
            var eventContextId = _policyEventContextIdQuery.GeteventContextId(command.PolicyNumber);
            if (!eventContextId.HasValue)
                throw new QueryException($"The policy {command.PolicyNumber} does not exist!");
            var policy = _singlePolicyQuery.Read(eventContextId.Value);

            var premium = policy.Premiums.Single(p => p.PremiumId == command.PremiumId);
            if (premium.IsAllocated)
                throw new PolicyException($"Premium has already been allocated");
            if (premium.IsReceived)
                throw new PolicyException($"Premium has already been received");

            return new[] { new PremiumReceivedEvent(eventContextId.Value, command.PremiumId, command.DateTimeReceived) };
        }
    }
}