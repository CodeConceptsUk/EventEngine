using System;
using System.Reflection;
using CodeConcepts.EventEngine.Application.Interfaces.Factories;
using log4net;

namespace CodeConcepts.EventEngine.Application.Factories
{
    public class LogFactory : ILogFactory
    {
        public ILog GetLogger(Type type)
        {
            return LogManager.GetLogger(Assembly.GetCallingAssembly(), type);
        }
    }
}