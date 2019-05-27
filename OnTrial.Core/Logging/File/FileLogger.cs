using System;
using System.Collections.Concurrent;
using System.IO;
using Microsoft.Extensions.Logging;

namespace OnTrial
{
    /// <summary>
    /// A logger that writes the logs to file
    /// </summary>
    public class FileLogger : ILogger
    {
        #region Static Properties

        /// <summary>
        /// A list of file locks based on path
        /// </summary>
        protected static ConcurrentDictionary<string, object> FileLocks = new ConcurrentDictionary<string, object>();

        #endregion

        #region Protected Members

        /// <summary>
        /// The category for this logger
        /// </summary>
        protected string mCategoryName;

        /// <summary>
        /// The file path to write to
        /// </summary>
        protected string mFilePath;

        /// <summary>
        /// The configuration to use
        /// </summary>
        protected FileLoggerConfiguration mConfiguration;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="pCategoryName">The category for this logger</param>
        /// <param name="pFilePath">The file path to write to</param>
        /// <param name="pConfiguration">The configuration to use</param>
        public FileLogger(string pCategoryName, string pFilePath, FileLoggerConfiguration pConfiguration)
        {
            // Set members
            mCategoryName = pCategoryName;
            mFilePath = Path.GetFullPath(pFilePath);
            mConfiguration = pConfiguration;
        }

        #endregion

        /// <summary>
        /// File loggers are not scoped so this is always null
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="pState"></param>
        /// <returns></returns>
        public IDisposable BeginScope<TState>(TState pState) => null;

        /// <summary>
        /// Enabled if the log level is the same or greater than the configuration
        /// </summary>
        /// <param name="logLevel">The log level to check against</param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel pLogLevel)
        {
            // Enabled if the log level is greater or equal to what we want to log
            return pLogLevel >= mConfiguration.LogLevel;
        }

        /// <summary>
        /// Logs the message to file
        /// </summary>
        /// <typeparam name="TState">The type of the details of the message</typeparam>
        /// <param name="pLogLevel">The log level</param>
        /// <param name="pEventID">The ID</param>
        /// <param name="pState">The details of the message</param>
        /// <param name="pException">Any exception to log</param>
        /// <param name="pFormatter">The formatter for converting the state and exception to a message string</param>
        public void Log<TState>(LogLevel pLogLevel, EventId pEventID, TState pState, Exception pException, Func<TState, Exception, string> pFormatter)
        {
            // If we should not log...
            if (!IsEnabled(pLogLevel))
                // Return
                return;

            // Get current time
            var currentTime = DateTimeOffset.Now.ToString("yyyy-MM-dd hh:mm:ss");

            // Prepend the time to the log if desired
            var timeLogString = mConfiguration.LogTime ? $"[{currentTime}] " : "";

            // Get the formatted message string
            var message = pFormatter(pState, pException);

            // Write the message
            var output = $"{timeLogString}{message}{Environment.NewLine}";

            // Normalize path
            // TODO: Make use of configuration base path
            var normalizedPath = mFilePath.ToUpper();

            // Get the file lock based on the absolute path
            var fileLock = FileLocks.GetOrAdd(normalizedPath, path => new object());

            // Lock the file
            lock (fileLock)
            {
                // Write the message to the file
                File.AppendAllText(mFilePath, output);
            }
        }
    }
}