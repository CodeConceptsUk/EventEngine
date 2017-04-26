using System.Collections.Generic;
using System.Linq;
using Engine.Interfaces;

namespace Engine.Bus
{
    public class EventStateMachine : IEventStateMachine
    {
        private IList<IHandler> Handlers;

        public void Apply<TEvent>(TEvent @event) 
            where TEvent : IEvent
        {
            When(state)
                .Then() 

            var handler = Handlers.First();
            handler.Handle(@event);
        }
    }
}