using Application.Interfaces.Domain;

namespace Domain
{
    public class FundPremium : IFundPremium
    {
        public FundPremium(string fundId, decimal premium)
        {
            FundId = fundId;
            Premium = premium;
        }

        public string FundId { get; set; }

        public decimal Premium { get; set; }
    }
}