namespace Policy.Application.Interfaces
{
    public interface ISnapshot<TView>
        where TView : class, IView
    {
        IEvent Event { get; set; }

        TView View { get; set; }
    }
}