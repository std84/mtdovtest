using Microsoft.Extensions.Logging;
using NLog;  
using System;  
using System.Collections.Generic; 
namespace missiontest.REPOSITORY
{
    public class LogNLog: ILog  
    {  
        private static NLog.ILogger logger = LogManager.GetCurrentClassLogger();  
   
        public LogNLog()  
        {  
        }  
   
        public void Debug(string message)  
        {  
            Logger logger = LogManager.GetLogger("fileLogger");
            //Logger logger = LogManager.GetLogger("EventLogTarget");  
            var logEventInfo = new LogEventInfo(NLog.LogLevel.Error, "EventLogMessage", $"{message}, generated at {DateTime.UtcNow}.");  
            logger.Log(logEventInfo);  
            //LogManager.Shutdown();  
        }  
   
        public void Error(string message)  
        {   
            Logger logger = LogManager.GetLogger("fileLogger");
  var logEventInfo = new LogEventInfo(NLog.LogLevel.Error, "EventLogMessage", $"{message}, generated at {DateTime.UtcNow}.");  
          
            logger.Error(message);  
        }  
   
        public void Information(string message)  
        {  
             Logger logger = LogManager.GetLogger("fileLogger");
               var logEventInfo = new LogEventInfo(NLog.LogLevel.Error, "EventLogMessage", $"{message}, generated at {DateTime.UtcNow}.");  
          
            logger.Info(message);  
        }  
   
        public void Warning(string message)  
        {  
             Logger logger = LogManager.GetLogger("fileLogger");
               var logEventInfo = new LogEventInfo(NLog.LogLevel.Error, "EventLogMessage", $"{message}, generated at {DateTime.UtcNow}.");  
          
            logger.Warn(message);  
        }  
    }  
}