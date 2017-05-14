using CodeConcepts.EventEngine.Api.Contracts;

namespace CodeConcepts.EventEngine.Contracts.Interfaces
{
    public interface ISnapshot<TView>
        where TView : class, IView
    {
        IEvent Event { get; set; }

        TView View { get; set; }
    }
}