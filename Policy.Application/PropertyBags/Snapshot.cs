using Policy.Application.Interfaces;

namespace Policy.Application.PropertyBags
{
    public class Snapshot<TView, TContext> : ISnapshot<TView, TContext>
        where TContext : class, IContext
        where TView : class, IView<TContext>
    {
        public Snapshot(IEvent<TContext> @event, TView view)
        {
            Event = @event;
            View = view;
        }

        public IEvent<TContext> Event { get; set; }

        public TView View { get; set; }
    }
}