using Engine.Commands;
using Engine.Interfaces;

namespace Engine.Bus
{
    public class CommandBus : ICommandBus
    {
        private readonly IEventStoreRepository _repository;

        public CommandBus(IEventStoreRepository repository)
        {
            _repository = repository;
        }

        public void Publish<TCommand>(TCommand command) where TCommand : ICommand
        {

            repository.Add(command);
        }
    }
}