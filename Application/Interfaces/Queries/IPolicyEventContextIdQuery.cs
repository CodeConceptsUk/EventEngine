using Application.Views;

namespace Application.Interfaces.Queries
{
    public interface IPolicyEventContextIdQuery
    {
        PolicyContextView Read(string policyNumber);
    }
}