using CodeConcepts.EventEngine.Api.Contracts;

namespace CodeConcepts.EventEngine.Contracts.Interfaces
{
    public interface IQueryHandler
    {
    }

    public interface IQueryHandler<in TQuery, out TResult> : IQueryHandler
        where TResult : class, IView
        where TQuery : class, IQuery
    {
        TResult Read(TQuery query);
    }
}