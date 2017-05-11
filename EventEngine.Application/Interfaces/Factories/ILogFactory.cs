using System;
using log4net;

namespace Policy.Application.Interfaces.Factories
{
    public interface ILogFactory
    {
        ILog GetLogger(Type type);
    }
}