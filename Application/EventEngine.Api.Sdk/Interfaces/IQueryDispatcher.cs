using CodeConcepts.EventEngine.Api.Contracts;

namespace CodeConcepts.EventEngine.Contracts.Interfaces
{
    public interface IQueryDispatcher<in TQuery>
        where TQuery : class, IQuery
    {
        IView Read(TQuery query);
    }
}