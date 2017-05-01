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
    public class UnitAllocationHandler : IsaPolicyCommandHandler<UnitAllocationCommand>
    {
        private readonly IPolicyEventContextIdQuery _policyEventContextIdQuery;
        private readonly IPolicyQuery _policyQuery;
        private readonly IUnitPricingRepository _unitPricingRepository;

        public UnitAllocationHandler(IPolicyEventContextIdQuery policyEventContextIdQuery, IPolicyQuery policyQuery, IUnitPricingRepository unitPricingRepository)
        {
            _policyEventContextIdQuery = policyEventContextIdQuery;
            _policyQuery = policyQuery;
            _unitPricingRepository = unitPricingRepository;
        }

        public override IEnumerable<IsaPolicyEvent> Execute(UnitAllocationCommand command)
        {
            var policy = _policyQuery.Read(command.PolicyNumber);
            var eventContextId = _policyEventContextIdQuery.GeteventContextId(command.PolicyNumber);
            if (!eventContextId.HasValue)
                throw new QueryException($"The policy {command.PolicyNumber} does not exist!");

            var events = new List<IsaPolicyEvent>();
            policy.Premiums.Where(p => !p.IsAllocated).ForEach(p =>
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