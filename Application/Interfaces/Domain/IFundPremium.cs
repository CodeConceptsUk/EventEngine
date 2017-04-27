namespace Application.Interfaces.Domain
{
    public interface IFundPremium
    {
        string FundId { get; set; }

        decimal Premium { get; set; }
    }
}