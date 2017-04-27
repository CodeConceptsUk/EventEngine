namespace Application.PropertyBags
{
    public class FundPremiumDetails
    {
        public FundPremiumDetails(string fundId, decimal premium)
        {
            FundId = fundId;
            Premium = premium;
        }

        public string FundId { get; }

        public decimal Premium { get; }
    }
}