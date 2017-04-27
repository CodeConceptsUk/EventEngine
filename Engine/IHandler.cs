namespace Engine
{
    public interface IHandler<in IEvent>
        where IEvent : class 
    {
        void Handle(IEvent @event);
    }
}