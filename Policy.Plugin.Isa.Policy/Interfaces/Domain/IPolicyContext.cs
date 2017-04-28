using System.Collections.Generic;
using Policy.Application.Interfaces;

namespace Policy.Plugin.Isa.Policy.Interfaces.Domain
{
    public interface IPolicyContext : IContext
    {
        string PolicyNumber { get; }

        int CustomerId { get; }

        IEnumerable<IFund> Funds { get; }
    }
}