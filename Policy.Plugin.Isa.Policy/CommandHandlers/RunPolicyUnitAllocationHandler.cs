using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ObjectBuilder2;
using Policy.Application.Exceptions;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Commands;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Interfaces.DataAccess;
using Policy.Plugin.Isa.Policy.Interfaces.Queries;
using Policy.Plugin.Isa.Policy.Views.PolicyView;
// ReSharper disable SuspiciousTypeConversion.Global

namespace Policy.Plugin.Isa.Policy.CommandHandlers
{
    public class UnitAllocationHandler : ICommandHandler<UnitAllocationCommand>
    {
        private readonly IPolicyeventContextIdQuery _policyeventContextIdQuery;
        private readonly IPolicyQuery _policyQuery;
        private readonly IUnitPricingRepository _unitPricingRepository;

        public UnitAllocationHandler(IPolicyeventContextIdQuery policyeventContextIdQuery, IPolicyQuery policyQuery, IUnitPricingRepository unitPricingRepository)
        {
            _policyeventContextIdQuery = policyeventContextIdQuery;
            _policyQuery = policyQuery;
            _unitPricingRepository = unitPricingRepository;
        }

        public IEnumerable<IEvent> Execute(UnitAllocationCommand command)
        {
            var policy = _policyQuery.Read(command.PolicyNumber);
            var eventContextId = _policyeventContextIdQuery.GeteventContextId(command.PolicyNumber);
            if (!eventContextId.HasValue)
                throw new QueryException($"The policy {command.PolicyNumber} does not exist!");

            return policy.Premiums.Where(p => !p.IsAllocated).Select(p =>
            {
                return p.Partitions.Select(part =>
                {
                    var units = _unitPricingRepository.Get(part.FundId, command.DateOfAllocation, part.Amount);
                    return new UnitsAllocatedEvent(eventContextId.Value, part.FundId, units, part.Amount, command.DateOfAllocation);
                });
            }).OfType<IEvent>();
        }
    }
}