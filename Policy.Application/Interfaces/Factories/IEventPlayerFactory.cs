﻿namespace Policy.Application.Interfaces.Factories
{
    public interface IEventPlayerFactory
    {
        IEventPlayer<TEventBase> Create<TEventBase>() where TEventBase : class, IEvent;
    }
}