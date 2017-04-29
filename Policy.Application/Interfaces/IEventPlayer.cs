using System.Collections.Generic;

namespace Policy.Application.Interfaces
{
    public interface IContext
    {
    }

    public interface IView<TContext>
        where TContext : class, IContext
    {
        
    }

    public interface IEventPlayer
    {
        TView Handle<TContext, TView>(IEnumerable<IEvent<TContext>> events, TView view)
            where TContext : class, IContext
            where TView : class, IView<TContext>;
    }
}