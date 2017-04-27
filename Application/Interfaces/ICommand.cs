namespace Application.Interfaces
{
    public interface ICommand <TContext>
        where TContext : class, IContext
    {
        
    }
}