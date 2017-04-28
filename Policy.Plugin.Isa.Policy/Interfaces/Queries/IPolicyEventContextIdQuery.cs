using Policy.Plugin.Isa.Policy.Views;

namespace Policy.Plugin.Isa.Policy.Interfaces.Queries
{
    public interface IPolicyEventContextIdQuery
    {
        PolicyContextView Read(string policyNumber);
    }
}