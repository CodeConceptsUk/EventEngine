using System;
using System.Collections.Generic;
using System.Linq;
using CodeConcepts.EventEngine.Api.Contracts;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Commands;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.CoreQueries;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.CoreQueryHandlers;
using CodeConcepts.FrameworkExtensions.Interfaces.Providers;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CommandHandlers
{
    public class ProcessChargesHandler : IsaPolicyNumberHandler, IAggregateCommandHandler<ProcessChargesCommand, IsaPolicyEvent>
    {
        private readonly IPolicyFundUnitBalanceQueryHandler _policyFundUnitBalanceQueryHandler;
        private readonly IDateTimeProvider _dateTimeProvider;

        public ProcessChargesHandler(IGetEventContextIdForPolicyNumberQueryHandler getEventContextIdForPolicyNumberQueryHandler,
            IPolicyFundUnitBalanceQueryHandler policyFundUnitBalanceQueryHandler,
            IDateTimeProvider dateTimeProvider)
            : base(getEventContextIdForPolicyNumberQueryHandler)
        {
            _policyFundUnitBalanceQueryHandler = policyFundUnitBalanceQueryHandler;
            _dateTimeProvider = dateTimeProvider;
        }

        public IEnumerable<ICommand> Execute(ProcessChargesCommand command)
        {
            var eventContextId = GetEventContextId(command.PolicyNumber);
            var policyFundUnitBalanceView = _policyFundUnitBalanceQueryHandler.Read(new GetPolicyFundUnitBalanceQuery(eventContextId));
            var commands = new List<ICommand>();

            var fundsRequiringDailyCharges = policyFundUnitBalanceView.FundAllocations.Where(t => t.UnitBalance > 0 && ShouldApplyChargeForDate(t.ChargesUpToDateOn));
            foreach (var fund in fundsRequiringDailyCharges)
            {
                var chargeDate = fund.ChargesUpToDateOn.Date;
                while (ShouldApplyChargeForDate(chargeDate))
                {
                    chargeDate = chargeDate.AddDays(1);
                    commands.Add(new CreateChargeCommand(command.PolicyNumber, fund.FundId, fund.PortionId, chargeDate));
                }
            }

            return commands;
        }

        private bool ShouldApplyChargeForDate(DateTime lastChargeDate)
        {
            var currentDate = _dateTimeProvider.GetDate();
            return lastChargeDate.Date < currentDate.Date;
        }
    }
}