using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace OnTrial
{
    /// <summary>
    /// Provides the ability to log to the DatabaseLogger
    /// </summary>
    public class DatabaseLoggerProvider : ILoggerProvider
    {
        #region Protected Members

        /// <summary>
        /// The path to log to
        /// </summary>
        protected string mDataSource;

        /// <summary>
        /// The configuration to use when creating loggers
        /// </summary>
        protected readonly DatabaseLoggerConfiguration mConfiguration;

        /// <summary>
        /// Keeps track of the loggers already created
        /// </summary>
        protected readonly ConcurrentDictionary<string, DatabaseLogger> mLoggers = new ConcurrentDictionary<string, DatabaseLogger>();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="pPath">The path to log to</param>
        /// <param name="pConfiguration">The configuration to use</param>
        public DatabaseLoggerProvider(string pDataSource, DatabaseLoggerConfiguration pConfiguration)
        {
            // Set the configuration
            mConfiguration = pConfiguration;

            // Set the path
            mDataSource = pDataSource;
        }

        #endregion

        #region ILoggerProvider Implementation

        /// <summary>
        /// Creates a DatabaseLogger logger based on the category name
        /// </summary>
        /// <param name="pCategoryName">The category name of this logger</param>
        /// <returns></returns>
        public ILogger CreateLogger(string pCategoryName)
        {
            // Get or create the logger for this category
            return mLoggers.GetOrAdd(pCategoryName, name => new DatabaseLogger(name, mDataSource, mConfiguration));
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose() => mLoggers.Clear();

        #endregion
    }
}