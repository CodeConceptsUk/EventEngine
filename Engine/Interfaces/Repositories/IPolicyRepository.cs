namespace Engine.Interfaces.Repositories
{
    public interface IPolicyRepository
    {
        IPolicy Get(string policyNumber);
    }
}