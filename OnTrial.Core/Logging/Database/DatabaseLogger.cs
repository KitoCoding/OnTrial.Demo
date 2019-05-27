using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Data.SqlClient;

namespace OnTrial
{
    /// <summary>
    /// A logger that writes the provided datasource
    /// </summary>
    public class DatabaseLogger : ILogger
    {
        #region Protected Members

        /// <summary>
        /// The category for this logger
        /// </summary>
        protected string mCategoryName;

        /// <summary>
        /// The datasource to write to
        /// </summary>
        protected string mDataSource;

        /// <summary>
        /// The configuration to use
        /// </summary>
        protected DatabaseLoggerConfiguration mConfiguration;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="pCategoryName">The category for this logger</param>
        /// <param name="pDataSource">The datasource to write to</param>
        /// <param name="pConfiguration">The configuration to use</param>
        public DatabaseLogger(string pCategoryName, string pDataSource, DatabaseLoggerConfiguration pConfiguration)
        {
            // Set members
            mCategoryName = pCategoryName;
            mDataSource = pDataSource;
            mConfiguration = pConfiguration;
        }

        #endregion

        /// <summary>
        /// DatabaseLogger loggers are not scoped so this is always null
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="pState"></param>
        /// <returns></returns>
        public IDisposable BeginScope<TState>(TState pState) => null;

        /// <summary>
        /// TODO: IMPLEMENT
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel logLevel) => true;

        /// <summary>
        /// Logs the message to the DatabaseLogger
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
                return;

            try
            {
                using (IDbConnection connection = new SqlConnection(mDataSource))
                {
                    connection.Query(@"Exec [dbo].[InsertLog] @machineName, @logged, @level, @message, @callsite, @exception",
                        new
                        {
                            machineName = OnTrial.Environment.MachineName,
                            logged = DateTime.UtcNow,
                            level = pLogLevel.ToString(),
                            message = pFormatter(pState, pException),
                            callsite = pException?.StackTrace,
                            exception = pException?.ToString()
                        });
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}