using System;
using System.Collections.Generic;
using Application.Commands;
using Application.Interfaces;
using Application.Interfaces.Domain;

namespace Application.CommandHandlers
{
    public class AddPremiumHandler : ICommandHandler<AddPremiumCommand, IPolicyContext>
    {
        public IEnumerable<IEvent<IPolicyContext>> Execute(AddPremiumCommand command)
        {
            throw new NotImplementedException();
        }
    }
}