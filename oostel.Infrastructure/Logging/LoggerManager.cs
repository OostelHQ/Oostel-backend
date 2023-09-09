using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oostel.Common.Interfaces;

namespace Oostel.Infrastructure.Logging
{
    public class LoggerManager : ILoggerManager
    {
        public void LogError(string logEvent, object logData)
        {
            Log.Error($"Error :: {DateTime.UtcNow}, LogEvent : {logEvent}, LogData : {logData}");
        }

        public void LogInfo(string logEvent, object logData)
        {
            Log.Information($"INFO :: {DateTime.UtcNow}, LogEvent : {logEvent}, LogData : {logData}");
        }
    }
}
