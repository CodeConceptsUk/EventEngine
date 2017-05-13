namespace CodeConcepts.EventEngine.IsaPolicy.Contracts.Interfaces.DataAccess
{
    public interface ISequencingRepository
    {
        string Get(string type);
    }
}