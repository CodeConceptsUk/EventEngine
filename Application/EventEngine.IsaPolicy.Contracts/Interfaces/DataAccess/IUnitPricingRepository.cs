using System;

namespace CodeConcepts.EventEngine.IsaPolicy.DataAccess.Interfaces
{
    public interface IUnitPricingRepository
    {
        decimal Get(string fundId, DateTime dateOfAllocation, decimal value);
    }
}