using System;
using Exceptron.Client;
using Exceptron.Client.Configuration;
using log4net.Core;

namespace Exceptron.Log4Net
{
    public class ExceptronAppender : log4net.Appender.AppenderSkeleton
    {
        private ExceptionClient _exceptionClient;

        protected override bool PreAppendCheck()
        {
            var config = new ExceptronConfiguration()
            {
                ApiKey = ApiKey,
                ThrowExceptions = ThrowExceptions
            };
            _exceptionClient = new ExceptionClient(config);
            return true;
        }

        /// <summary>
        /// Exceptron API Key
        /// </summary>
        public string ApiKey { get; set;  }

        /// <summary>
        /// If the Appender should also throw exceptions
        /// </summary>
        public bool ThrowExceptions { get; set; }

        /// <summary>
        /// String that identifies the active user
        /// </summary>
        public string UserId { get; set; }

        protected override void Append(log4net.Core.LoggingEvent loggingEvent)
        {
            if (loggingEvent == null || loggingEvent.ExceptionObject == null) return;


            try
            {
                var exceptionData = new ExceptionData
                                        {
                                            Exception = loggingEvent.ExceptionObject,
                                            Component = loggingEvent.LoggerName,
                                            Message = loggingEvent.RenderedMessage,
                                            UserId = UserId                                            
                                        };
                if (loggingEvent.Level <= Level.Info)
                {
                    exceptionData.Severity = ExceptionSeverity.None;
                }
                else if (loggingEvent.Level <= Level.Warn)
                {
                    exceptionData.Severity = ExceptionSeverity.Warning;
                }
                else if (loggingEvent.Level <= Level.Error)
                {
                    exceptionData.Severity = ExceptionSeverity.Error;
                }
                else if (loggingEvent.Level <= Level.Fatal)
                {
                    exceptionData.Severity = ExceptionSeverity.Fatal;
                }

                _exceptionClient.SubmitException(exceptionData);
            }
            catch (Exception e)
            {
                //base.Append(new LoggingEvent(new LoggingEventData() { Domain= loggingEvent.Domain, ExceptionString = e.ToString(), Identity = loggingEvent.Identity, Level = Level.Error, Message = "Unable to report exception." }));
            }
        }
    }
}