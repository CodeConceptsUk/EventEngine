﻿using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Events;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CoreViewEventEvaluators.PolicyFundBalanceView
{
    public class PolicyCreatedEventEvaluator : IEventEvaluator<PolicyCreatedEvent, Domain.PolicyView>
    {
        public void Evaluate(Domain.PolicyView view, PolicyCreatedEvent @event)
        {
            view.PolicyNumber = @event.PolicyNumber;
            view.CustomerId = @event.CustomerId;
        }
    }
}