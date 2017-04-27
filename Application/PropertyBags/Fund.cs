using Application.Interfaces.Domain;

namespace Application.PropertyBags
{
    public class Fund : IFund
    {
        public Fund(string fundId)
        {
            FundId = fundId;
            UnallocatedPremiums = 0;
            Units = 0;
        }

        public string FundId { get; set; }

        public decimal UnallocatedPremiums { get; set; }

        public decimal Units { get; set; }
    }
}