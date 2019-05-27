using Microsoft.Extensions.Logging;
using System;
using System.Runtime.CompilerServices;

namespace OnTrial
{
    /// <summary>
    /// Extensions for <see cref="ILogger"/> loggers
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        /// Logs a critical message, including the source of the log
        /// </summary>
        /// <param name="pLogger">The logger</param>
        /// <param name="pMessage">The message</param>
        /// <param name="pEventID">The event ID</param>
        /// <param name="pException">The exception</param>
        /// <param name="pOrigin">The callers member/function name</param>
        /// <param name="pFilePath">The source code file path</param>
        /// <param name="pLineNumber">The line number in the code file of the caller</param>
        /// <param name="pArgs">The additional arguments</param>
        public static void Critical(this ILogger pLogger, string pMessage, EventId pEventID = new EventId(), Exception pException = null, [CallerMemberName] string pOrigin = "", [CallerFilePath] string pFilePath = "", [CallerLineNumber] int pLineNumber = 0, params object[] pArgs) =>
            pLogger?.Log(LogLevel.Critical, pEventID, pArgs.Prepend(pOrigin, pFilePath, pLineNumber, pMessage), pException, LoggerSourceFormatter.Format);

        /// <summary>
        /// Logs a verbose trace message, including the source of the log
        /// </summary>
        /// <param name="pLogger">The logger</param>
        /// <param name="pMessage">The message</param>
        /// <param name="pEventID">The event ID</param>
        /// <param name="pException">The exception</param>
        /// <param name="pOrigin">The callers member/function name</param>
        /// <param name="pFilePath">The source code file path</param>
        /// <param name="pLineNumber">The line number in the code file of the caller</param>
        /// <param name="pArgs">The additional arguments</param>
        public static void Trace(this ILogger pLogger, string pMessage, EventId pEventID = new EventId(), Exception pException = null, [CallerMemberName] string pOrigin = "", [CallerFilePath] string pFilePath = "", [CallerLineNumber] int pLineNumber = 0, params object[] pArgs) =>
            pLogger?.Log(LogLevel.Trace, pEventID, pArgs.Prepend(pOrigin, pFilePath, pLineNumber, pMessage), pException, LoggerSourceFormatter.Format);

        /// <summary>
        /// Logs a debug message, including the source of the log
        /// </summary>
        /// <param name="pLogger">The logger</param>
        /// <param name="pMessage">The message</param>
        /// <param name="pEventID">The event ID</param>
        /// <param name="pException">The exception</param>
        /// <param name="pOrigin">The callers member/function name</param>
        /// <param name="pFilePath">The source code file path</param>
        /// <param name="pLineNumber">The line number in the code file of the caller</param>
        /// <param name="pArgs">The additional arguments</param>
        public static void Debug(this ILogger pLogger, string pMessage, EventId pEventID = new EventId(), Exception pException = null, [CallerMemberName] string pOrigin = "", [CallerFilePath] string pFilePath = "", [CallerLineNumber] int pLineNumber = 0, params object[] pArgs) =>
            pLogger?.Log(LogLevel.Debug, pEventID, pArgs.Prepend(pOrigin, pFilePath, pLineNumber, pMessage), pException, LoggerSourceFormatter.Format);

        /// <summary>
        /// Logs an error message, including the source of the log
        /// </summary>
        /// <param name="pLogger">The logger</param>
        /// <param name="pMessage">The message</param>
        /// <param name="pEventID">The event ID</param>
        /// <param name="pException">The exception</param>
        /// <param name="pOrigin">The callers member/function name</param>
        /// <param name="pFilePath">The source code file path</param>
        /// <param name="pLineNumber">The line number in the code file of the caller</param>
        /// <param name="pArgs">The additional arguments</param>
        public static void Error(this ILogger pLogger, string pMessage, EventId pEventID = new EventId(), Exception pException = null, [CallerMemberName] string pOrigin = "", [CallerFilePath] string pFilePath = "", [CallerLineNumber] int pLineNumber = 0, params object[] pArgs) =>
            pLogger?.Log(LogLevel.Error, pEventID, pArgs.Prepend(pOrigin, pFilePath, pLineNumber, pMessage), pException, LoggerSourceFormatter.Format);

        /// <summary>
        /// Logs an informative message, including the source of the log
        /// </summary>
        /// <param name="pLogger">The logger</param>
        /// <param name="pMessage">The message</param>
        /// <param name="pEventID">The event ID</param>
        /// <param name="pException">The exception</param>
        /// <param name="pOrigin">The callers member/function name</param>
        /// <param name="pFilePath">The source code file path</param>
        /// <param name="pLineNumber">The line number in the code file of the caller</param>
        /// <param name="pArgs">The additional arguments</param>
        public static void Info(this ILogger pLogger, string pMessage, EventId pEventID = new EventId(), Exception pException = null, [CallerMemberName] string pOrigin = "", [CallerFilePath] string pFilePath = "", [CallerLineNumber] int pLineNumber = 0, params object[] pArgs) =>
            pLogger?.Log(LogLevel.Information, pEventID, pArgs.Prepend(pOrigin, pFilePath, pLineNumber, pMessage), pException, LoggerSourceFormatter.Format);

        /// <summary>
        /// Logs a warning message, including the source of the log
        /// </summary>
        /// <param name="pLogger">The logger</param>
        /// <param name="pMessage">The message</param>
        /// <param name="pEventID">The event ID</param>
        /// <param name="pException">The exception</param>
        /// <param name="pOrigin">The callers member/function name</param>
        /// <param name="pFilePath">The source code file path</param>
        /// <param name="pLineNumber">The line number in the code file of the caller</param>
        /// <param name="pArgs">The additional arguments</param>
        public static void Warn(this ILogger pLogger, string pMessage, EventId pEventID = new EventId(), Exception pException = null, [CallerMemberName] string pOrigin = "", [CallerFilePath] string pFilePath = "", [CallerLineNumber] int pLineNumber = 0, params object[] pArgs) =>
            pLogger?.Log(LogLevel.Warning, pEventID, pArgs.Prepend(pOrigin, pFilePath, pLineNumber, pMessage), pException, LoggerSourceFormatter.Format);
    }
}