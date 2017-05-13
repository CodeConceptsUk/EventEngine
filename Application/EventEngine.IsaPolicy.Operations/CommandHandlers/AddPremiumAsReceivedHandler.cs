using System.Collections.Generic;
using CodeConcepts.EventEngine.Contracts.Interfaces;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.BaseTypes;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Commands;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.CommandHandlers
{
    public class AddPremiumAsReceivedHandler : ICommandHandler<AddPremiumAsReceivedCommand, IsaPolicyEvent>
    {
        private readonly AddPremiumHandler _addPremiumHandler;
        private readonly SetPremiumAsReceivedHandler _setPremiumAsReceivedHandler;

        public AddPremiumAsReceivedHandler(AddPremiumHandler addPremiumHandler, SetPremiumAsReceivedHandler setPremiumAsReceivedHandler)
        {
            _addPremiumHandler = addPremiumHandler;
            _setPremiumAsReceivedHandler = setPremiumAsReceivedHandler;
        }

        public IEnumerable<IsaPolicyEvent> Execute(AddPremiumAsReceivedCommand command)
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
