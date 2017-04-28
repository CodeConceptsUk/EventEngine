namespace Policy.Application.Interfaces
{
    public interface IQuery<out TView, in TContext>
        where TView : class
        where TContext : class
    {
    }
}