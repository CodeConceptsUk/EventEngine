namespace Application.PropertyBags
{
    public class FundPremiumDetails
    {
        public FundPremiumDetails(string fundId, string premium)
        {
            FundId = fundId;
            Premium = premium;
        }

        public string FundId { get; }

        public string Premium { get; }
    }
}