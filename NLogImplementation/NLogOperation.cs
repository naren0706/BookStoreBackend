using NLog;
using System;

namespace NlogImplementation
{
    public class NlogOperation
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        public void LogInfo(string message)
        {
            logger.Info(message);
        }
        public void LogWarn(string message)
        {
            logger.Warn(message);
        }
        public void LogError(string message)
        {
            logger.Error(message);
        }
    }
}
