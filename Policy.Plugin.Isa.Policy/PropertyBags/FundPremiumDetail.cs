namespace Policy.Plugin.Isa.Policy.PropertyBags
{
    public class FundPremiumDetail
    {
        public FundPremiumDetail(string fundId, decimal amount)
        {
            FundId = fundId;
            Amount = amount;
        }

        public string FundId { get; }

        public decimal Amount { get; }
    }
}