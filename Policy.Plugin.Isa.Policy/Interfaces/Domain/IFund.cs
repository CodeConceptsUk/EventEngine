namespace Policy.Plugin.Isa.Policy.Interfaces.Domain
{
    public interface IFund
    {
        string FundId { get; set; }

        decimal UnallocatedPremiums { get; set; }

        decimal Units { get; set; }
    }
}