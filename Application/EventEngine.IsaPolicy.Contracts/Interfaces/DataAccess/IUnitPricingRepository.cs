using System;

namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.DataAccess
{
    public interface IUnitPricingRepository
    {
        decimal Get(string fundId, DateTime dateOfAllocation, decimal value);
    }
}