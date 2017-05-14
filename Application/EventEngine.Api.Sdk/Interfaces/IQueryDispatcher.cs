using CodeConcepts.EventEngine.Api.Contracts;

namespace CodeConcepts.EventEngine.Contracts.Interfaces
{
    public interface IQueryDispatcher
    {
        IView Read(IQuery query);
    }
}