using System.Collections.Generic;
using CodeConcepts.EventEngine.Contracts.Exceptions;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Commands;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueries;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.DataAccess;
using CodeConcepts.FrameworkExtensions.LinqExtensions;

// ReSharper disable SuspiciousTypeConversion.Global

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CommandHandlers
{
    //TODO create sub command that only allocates for single premium
    public class AllocateUnitsHandler : ICommandHandler<AllocateUnitsCommand, IsaPolicyEvent>
    {
        private readonly IGetEventContextIdForPolicyNumberQuery _getEventContextIdForPolicyNumberQuery;
        private readonly IUnallocatedReceivedPremiumsQuery _unallocatedReceivedPremiumsQuery;
        private readonly IUnitPricingRepository _unitPricingRepository;

        public AllocateUnitsHandler(IGetEventContextIdForPolicyNumberQuery getEventContextIdForPolicyNumberQuery, IUnallocatedReceivedPremiumsQuery unallocatedReceivedPremiumsQuery, IUnitPricingRepository unitPricingRepository)
        {
            _getEventContextIdForPolicyNumberQuery = getEventContextIdForPolicyNumberQuery;
            _unallocatedReceivedPremiumsQuery = unallocatedReceivedPremiumsQuery;
            _unitPricingRepository = unitPricingRepository;
        }

        public IEnumerable<IsaPolicyEvent> Execute(AllocateUnitsCommand command)
        {
            var eventContextId = _getEventContextIdForPolicyNumberQuery.GetEventContextId(command.PolicyNumber);
            if (!eventContextId.HasValue)
                throw new QueryException($"The policy {command.PolicyNumber} does not exist!");
            var policy = _unallocatedReceivedPremiumsQuery.Read(eventContextId.Value);

            var events = new List<IsaPolicyEvent>();
            policy.ReceivedPartitions.ForEach(part =>
            {
                var units = _unitPricingRepository.Get(part.FundId, command.DateOfAllocation, part.Amount);
                events.Add(new UnitsAllocatedEvent(eventContextId.Value, part.PremiumId, part.PortionId, part.FundId, units, command.DateOfAllocation));
            });
            return events;
        }
    }
}