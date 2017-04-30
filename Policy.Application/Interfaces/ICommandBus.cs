namespace Policy.Application.Interfaces
{
    public interface ICommandBus
    {
        void Apply(ICommand command);
    }
}