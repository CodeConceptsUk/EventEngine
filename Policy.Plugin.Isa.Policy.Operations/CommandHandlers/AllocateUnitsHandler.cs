using System.Collections.Generic;
using System.Linq;
using FrameworkExtensions.LinqExtensions;
using Policy.Application.Exceptions;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Interfaces.DataAccess;
using Policy.Plugin.Isa.Policy.Operations.BaseTypes;
using Policy.Plugin.Isa.Policy.Operations.Commands;
using Policy.Plugin.Isa.Policy.Views.Queries;

// ReSharper disable SuspiciousTypeConversion.Global

namespace Policy.Plugin.Isa.Policy.Operations.CommandHandlers
{
    public class AllocateUnitsHandler : IsaPolicyCommandHandler<AllocateUnitsCommand>
    {
        private readonly IPolicyEventContextIdQuery _policyEventContextIdQuery;
        private readonly ISinglePolicyQuery _singlePolicyQuery;
        private readonly IUnitPricingRepository _unitPricingRepository;

        public AllocateUnitsHandler(IPolicyEventContextIdQuery policyEventContextIdQuery, ISinglePolicyQuery singlePolicyQuery, IUnitPricingRepository unitPricingRepository)
        {
            _policyEventContextIdQuery = policyEventContextIdQuery;
            _singlePolicyQuery = singlePolicyQuery;
            _unitPricingRepository = unitPricingRepository;
        }

        public override IEnumerable<IsaPolicyEvent> Execute(AllocateUnitsCommand command)
        {
            var eventContextId = _policyEventContextIdQuery.GeteventContextId(command.PolicyNumber);
            if (!eventContextId.HasValue)
                throw new QueryException($"The policy {command.PolicyNumber} does not exist!");
            var policy = _singlePolicyQuery.Build(eventContextId.Value);

            var events = new List<IsaPolicyEvent>();
            policy.Premiums.Where(p => !p.IsAllocated && p.IsReceived).ForEach(p =>
            {
                p.Partitions.ForEach(part =>
                {
                    var units = _unitPricingRepository.Get(part.FundId, command.DateOfAllocation, part.Amount);
                    events.Add(new UnitsAllocatedEvent(eventContextId.Value, p.PremiumId, part.PortionId, part.FundId, units, command.DateOfAllocation));
                });
            });
            return events;
        }
    }
}