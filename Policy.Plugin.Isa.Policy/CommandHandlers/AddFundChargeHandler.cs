using System;
using System.Collections.Generic;
using Policy.Application.Interfaces;
using Policy.Plugin.Isa.Policy.Commands;
using Policy.Plugin.Isa.Policy.Interfaces.Domain;

namespace Policy.Plugin.Isa.Policy.CommandHandlers
{
    public class AddFundChargeHandler : ICommandHandler<AddFundChargeCommand, IPolicyContext>
    {
        public IEnumerable<IEvent<IPolicyContext>> Execute(AddFundChargeCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
