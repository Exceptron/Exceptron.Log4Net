﻿using System;
using Exceptron.Client;
using Exceptron.Client.Configuration;
using log4net.Core;

namespace Exceptron.Log4Net
{
    /// <summary>
    /// <see cref="log4net"/> appender for exceptron. Allows you to automatically report all
    /// exceptions logged to log4net/>
    /// </summary>
    public class ExceptronAppender : log4net.Appender.AppenderSkeleton
    {
        /// <summary>
        /// <see cref="ExceptronClient"/> instance that log4net appender uses to report the exceptions.
        /// </summary>
        public IExceptronClient ExceptronClient { get; internal set; }

        protected override bool PreAppendCheck()
        {
            var config = new ExceptronConfiguration()
            {
                ApiKey = ApiKey,
                ThrowExceptions = ThrowExceptions
            };
            ExceptronClient = new ExceptronClient(config, ApplicationVersion);
            return true;
        }

        /// <summary>
        /// exceptron API Key
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// exceptron API Key
        /// </summary>
        public Version ApplicationVersion { get; set; }

        /// <summary>
        /// If the appender should also throw exceptions
        /// </summary>
        public bool ThrowExceptions { get; set; }

        /// <summary>
        /// String that identifies the active user
        /// </summary>
        public string UserId { get; set; }

        protected override void Append(log4net.Core.LoggingEvent loggingEvent)
        {
            if (loggingEvent == null || loggingEvent.ExceptionObject == null) return;

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

            ExceptronClient.SubmitException(exceptionData);

        }
    }
}