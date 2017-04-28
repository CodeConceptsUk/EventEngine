using DbUp.Engine.Output;
using log4net;

namespace DemoDB
{
    public class UpgradeLogger : IUpgradeLog
    {
        private readonly ILog _logger;

        public UpgradeLogger(ILog logger)
        {
            _logger = logger;
        }

        public void WriteInformation(string format, params object[] args)
        {
            _logger.InfoFormat(format, args);
        }

        public void WriteError(string format, params object[] args)
        {
            _logger.ErrorFormat(format, args);
        }

        public void WriteWarning(string format, params object[] args)
        {
            _logger.WarnFormat(format, args);
        }
    }
}