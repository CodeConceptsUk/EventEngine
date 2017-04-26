using System.Collections.Generic;

namespace Engine.Interfaces
{
    public interface IPolicy
    {
        string PolicyNumber { get; }

        IEnumerable<IFund> Funds { get; }

        void AddCharge(IFund fund, decimal units);

    }
}