﻿using System;
using System.Collections.Generic;

namespace Application.Interfaces
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
        TView Handle<TContext, TView>(IEnumerable<IEvent<TContext>> events)
            where TContext : class, IContext
            where TView : class, IView<TContext>, new();
    }
}