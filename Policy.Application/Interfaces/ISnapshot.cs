namespace Policy.Application.Interfaces
{
    public interface ISnapshot<TView, TContext>
        where TContext : class, IContext
        where TView : class, IView<TContext>
    {
        IEvent<TContext> Event { get; set; }

        TView View { get; set; }
    }
}