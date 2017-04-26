using System.Windows.Input;

namespace Application.Interfaces
{
    public interface ICommandBus
    {
        void Apply<TContext>(ICommand<TContext> command)
            where TContext : class, IContext;
    }
}