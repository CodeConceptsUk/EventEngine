﻿using System.Collections.Generic;
using Policy.Plugin.Isa.Policy.Events;
using Policy.Plugin.Isa.Policy.Operations.BaseTypes;
using Policy.Plugin.Isa.Policy.Operations.Commands;

namespace Policy.Plugin.Isa.Policy.Operations.CommandHandlers
{
    public class AddPremiumAsReceivedHandler : IsaPolicyCommandHandler<AddPremiumAsReceivedCommand>
    {
        private readonly AddPremiumHandler _addPremiumHandler;
        private readonly SetPremiumAsReceivedHandler _setPremiumAsReceivedHandler;

        public AddPremiumAsReceivedHandler(AddPremiumHandler addPremiumHandler, SetPremiumAsReceivedHandler setPremiumAsReceivedHandler)
        {
            _addPremiumHandler = addPremiumHandler;
            _setPremiumAsReceivedHandler = setPremiumAsReceivedHandler;
        }

        public override IEnumerable<IsaPolicyEvent> Execute(AddPremiumAsReceivedCommand command)
        {
            var events = new List<IsaPolicyEvent>();
            AddPremium(command, events);
            SetPremiumAsReceived(command, events);
            return events;
        }

        private void SetPremiumAsReceived(AddPremiumAsReceivedCommand command, List<IsaPolicyEvent> events)
        {
            var setPremiumAsReceived = new SetPremiumAsReceivedCommand(command.PolicyNumber, command.PremiumId, command.PremiumDateTime);
            events.AddRange(_setPremiumAsReceivedHandler.Execute(setPremiumAsReceived));
        }

        private void AddPremium(AddPremiumAsReceivedCommand command, List<IsaPolicyEvent> events)
        {
            var addPremiumCommand = new AddPremiumCommand(command.PolicyNumber, command.PremiumId, command.PremiumDateTime, command.FundPremiumDetail);
            events.AddRange(_addPremiumHandler.Execute(addPremiumCommand));
        }
    }
}