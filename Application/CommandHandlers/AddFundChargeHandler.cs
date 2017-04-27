using System;
using System.Collections.Generic;
using Application.Commands;
using Application.Interfaces;
using Application.Interfaces.Domain;

namespace Application.CommandHandlers
{
    public class AddFundChargeHandler : ICommandHandler<AddFundChargeCommand, IPolicyContext>
    {
        public IEnumerable<IEvent<IPolicyContext>> Execute(AddFundChargeCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
