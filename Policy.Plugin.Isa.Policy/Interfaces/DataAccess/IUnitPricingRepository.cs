using System;

namespace Policy.Plugin.Isa.Policy.Interfaces.DataAccess
{
    public interface IUnitPricingRepository
    {
        decimal Get(string fundId, DateTime dateOfAllocation, decimal value);
    }
}