using System;
using log4net;

namespace CodeConcepts.EventEngine.Application.Interfaces.Factories
{
    public interface ILogFactory
    {
        ILog GetLogger(Type type);
    }
}