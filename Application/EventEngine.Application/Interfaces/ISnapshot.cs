using CodeConcepts.EventEngine.Contracts.Interfaces;

namespace CodeConcepts.EventEngine.Application.Interfaces
{
    public interface ISnapshot<TView>
        where TView : class, IView
    {
        IEvent Event { get; set; }

        TView View { get; set; }
    }
}