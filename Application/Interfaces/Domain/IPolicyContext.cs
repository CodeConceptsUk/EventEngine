using System.Collections.Generic;

namespace Application.Interfaces.Domain
{
    public interface IPolicyContext : IContext
    {
        string PolicyNumber { get; }

        int CustomerId { get; }

        IEnumerable<IFund> Funds { get; }
    }
}