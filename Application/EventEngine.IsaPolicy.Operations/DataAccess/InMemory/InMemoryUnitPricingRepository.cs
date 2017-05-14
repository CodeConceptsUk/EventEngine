using System;
using CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.DataAccess;

namespace CodeConcepts.EventEngine.IsaPolicy.Operations.DataAccess.InMemory
{
    public class InMemoryUnitPricingRepository : IUnitPricingRepository
    {
        private static readonly Random Random = new Random(13245532);
        
        public decimal Get(string fundId, DateTime dateOfAllocation, decimal value)
        {
            // TODO: PRODUCT RULE
            var fractionalValue = Math.Round(value / 1000m, 6);
            return fractionalValue;
        }
    }
}