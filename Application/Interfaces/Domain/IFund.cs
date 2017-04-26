namespace Application.Interfaces.Domain
{
    public interface IFund
    {
        string FundId { get; }

        decimal Units { get; }
    }
}