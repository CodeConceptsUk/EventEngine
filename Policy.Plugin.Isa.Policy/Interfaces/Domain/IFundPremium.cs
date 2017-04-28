namespace Policy.Plugin.Isa.Policy.Interfaces.Domain
{
    public interface IFundPremium
    {
        string FundId { get; set; }

        decimal Premium { get; set; }
    }
}