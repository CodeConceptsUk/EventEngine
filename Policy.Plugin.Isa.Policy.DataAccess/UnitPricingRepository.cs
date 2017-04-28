using System;
using Policy.Plugin.Isa.Policy.Interfaces.DataAccess;

namespace Policy.Plugin.Isa.Policy.DataAccess
{
    public class UnitPricingRepository : IUnitPricingRepository
    {
        private static readonly Random Random = new Random(13245532);

        public decimal Get(string fundId, DateTime dateOfAllocation, decimal value)
        {
            var random = (decimal)Random.Next(1, 100) / 1000;
            //Console.WriteLine($"Fund: {fundId}, DateOfAllocation: {dateOfAllocation:dd/MM/yyyy}, Units: {random} for {value:C}");
            return value; //random;
        }
    }
}