using System;
using System.Reflection;
using log4net;
using Policy.Application.Interfaces.Factories;

namespace Policy.Application.Factories
{
    public class LogFactory : ILogFactory
    {
        public ILog GetLogger(Type type)
        {
            return LogManager.GetLogger(Assembly.GetCallingAssembly(), type);
        }
    }
}